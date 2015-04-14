using System.Data.Entity.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                {
                    GameId = c.Int(false, true),
                    Key = c.String(),
                    Name = c.String(),
                    Description = c.String(),
                    GenreId = c.Int(false)
                })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Genres", t => t.GenreId, true)
                .Index(t => t.GenreId);

            CreateTable(
                "dbo.Genres",
                c => new
                {
                    GenreId = c.Int(false, true),
                    Name = c.String(),
                    ParentGenreId = c.Int(),
                    ParentGenre_GenreId = c.Int()
                })
                .PrimaryKey(t => t.GenreId)
                .ForeignKey("dbo.Genres", t => t.ParentGenre_GenreId)
                .Index(t => t.ParentGenre_GenreId);

            CreateTable(
                "dbo.Comments",
                c => new
                {
                    CommentId = c.Int(false, true),
                    Name = c.String(),
                    Body = c.String(),
                    GameId = c.Int(false),
                    ParentCommentId = c.Int(),
                    ParentComment_CommentId = c.Int()
                })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Games", t => t.GameId, true)
                .ForeignKey("dbo.Comments", t => t.ParentComment_CommentId)
                .Index(t => t.GameId)
                .Index(t => t.ParentComment_CommentId);

            CreateTable(
                "dbo.PlatformTypes",
                c => new
                {
                    PlatformTypeId = c.Int(false, true),
                    Type = c.String()
                })
                .PrimaryKey(t => t.PlatformTypeId);

            CreateTable(
                "dbo.PlatformTypeGame",
                c => new
                {
                    PlatformType_PlatformTypeId = c.Int(false),
                    Game_GameId = c.Int(false)
                })
                .PrimaryKey(t => new {t.PlatformType_PlatformTypeId, t.Game_GameId})
                .ForeignKey("dbo.PlatformTypes", t => t.PlatformType_PlatformTypeId, true)
                .ForeignKey("dbo.Games", t => t.Game_GameId, true)
                .Index(t => t.PlatformType_PlatformTypeId)
                .Index(t => t.Game_GameId);
        }

        public override void Down()
        {
            DropIndex("dbo.PlatformTypeGame", new[] {"Game_GameId"});
            DropIndex("dbo.PlatformTypeGame", new[] {"PlatformType_PlatformTypeId"});
            DropIndex("dbo.Comments", new[] {"ParentComment_CommentId"});
            DropIndex("dbo.Comments", new[] {"GameId"});
            DropIndex("dbo.Genres", new[] {"ParentGenre_GenreId"});
            DropIndex("dbo.Games", new[] {"GenreId"});
            DropForeignKey("dbo.PlatformTypeGame", "Game_GameId", "dbo.Games");
            DropForeignKey("dbo.PlatformTypeGame", "PlatformType_PlatformTypeId", "dbo.PlatformTypes");
            DropForeignKey("dbo.Comments", "ParentComment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "GameId", "dbo.Games");
            DropForeignKey("dbo.Genres", "ParentGenre_GenreId", "dbo.Genres");
            DropForeignKey("dbo.Games", "GenreId", "dbo.Genres");
            DropTable("dbo.PlatformTypeGame");
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.Comments");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
        }
    }
}
