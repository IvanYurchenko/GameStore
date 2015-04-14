using System.Data.Entity.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class Fix4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "GenreId", "dbo.Genres");
            DropIndex("dbo.Games", new[] {"GenreId"});
            CreateTable(
                "dbo.GenreGame",
                c => new
                {
                    Genre_GenreId = c.Int(false),
                    Game_GameId = c.Int(false)
                })
                .PrimaryKey(t => new {t.Genre_GenreId, t.Game_GameId})
                .ForeignKey("dbo.Genres", t => t.Genre_GenreId, true)
                .ForeignKey("dbo.Games", t => t.Game_GameId, true)
                .Index(t => t.Genre_GenreId)
                .Index(t => t.Game_GameId);

            DropColumn("dbo.Games", "GenreId");
        }

        public override void Down()
        {
            AddColumn("dbo.Games", "GenreId", c => c.Int(false));
            DropIndex("dbo.GenreGame", new[] {"Game_GameId"});
            DropIndex("dbo.GenreGame", new[] {"Genre_GenreId"});
            DropForeignKey("dbo.GenreGame", "Game_GameId", "dbo.Games");
            DropForeignKey("dbo.GenreGame", "Genre_GenreId", "dbo.Genres");
            DropTable("dbo.GenreGame");
            CreateIndex("dbo.Games", "GenreId");
            AddForeignKey("dbo.Games", "GenreId", "dbo.Genres", "GenreId", true);
        }
    }
}