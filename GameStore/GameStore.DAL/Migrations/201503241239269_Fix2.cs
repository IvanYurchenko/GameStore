using System.Data.Entity.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class Fix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "Genre_GenreId", "dbo.Genres");
            DropForeignKey("dbo.Comments", "Comment_CommentId", "dbo.Comments");
            DropIndex("dbo.Genres", new[] {"Genre_GenreId"});
            DropIndex("dbo.Comments", new[] {"Comment_CommentId"});
            AddColumn("dbo.Genres", "ParentGenreId", c => c.Int());
            AddColumn("dbo.Comments", "ParentCommentId", c => c.Int());
            AddForeignKey("dbo.Genres", "ParentGenreId", "dbo.Genres", "GenreId");
            AddForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments", "CommentId");
            CreateIndex("dbo.Genres", "ParentGenreId");
            CreateIndex("dbo.Comments", "ParentCommentId");
            DropColumn("dbo.Genres", "Genre_GenreId");
            DropColumn("dbo.Comments", "Comment_CommentId");
        }

        public override void Down()
        {
            AddColumn("dbo.Comments", "Comment_CommentId", c => c.Int());
            AddColumn("dbo.Genres", "Genre_GenreId", c => c.Int());
            DropIndex("dbo.Comments", new[] {"ParentCommentId"});
            DropIndex("dbo.Genres", new[] {"ParentGenreId"});
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Genres", "ParentGenreId", "dbo.Genres");
            DropColumn("dbo.Comments", "ParentCommentId");
            DropColumn("dbo.Genres", "ParentGenreId");
            CreateIndex("dbo.Comments", "Comment_CommentId");
            CreateIndex("dbo.Genres", "Genre_GenreId");
            AddForeignKey("dbo.Comments", "Comment_CommentId", "dbo.Comments", "CommentId");
            AddForeignKey("dbo.Genres", "Genre_GenreId", "dbo.Genres", "GenreId");
        }
    }
}