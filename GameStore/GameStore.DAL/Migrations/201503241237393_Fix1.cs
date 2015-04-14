using System.Data.Entity.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class Fix1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "ParentGenre_GenreId", "dbo.Genres");
            DropForeignKey("dbo.Comments", "ParentComment_CommentId", "dbo.Comments");
            DropIndex("dbo.Genres", new[] {"ParentGenre_GenreId"});
            DropIndex("dbo.Comments", new[] {"ParentComment_CommentId"});
            AddColumn("dbo.Genres", "Genre_GenreId", c => c.Int());
            AddColumn("dbo.Comments", "Comment_CommentId", c => c.Int());
            AddForeignKey("dbo.Genres", "Genre_GenreId", "dbo.Genres", "GenreId");
            AddForeignKey("dbo.Comments", "Comment_CommentId", "dbo.Comments", "CommentId");
            CreateIndex("dbo.Genres", "Genre_GenreId");
            CreateIndex("dbo.Comments", "Comment_CommentId");
            DropColumn("dbo.Genres", "ParentGenreId");
            DropColumn("dbo.Genres", "ParentGenre_GenreId");
            DropColumn("dbo.Comments", "ParentCommentId");
            DropColumn("dbo.Comments", "ParentComment_CommentId");
        }

        public override void Down()
        {
            AddColumn("dbo.Comments", "ParentComment_CommentId", c => c.Int());
            AddColumn("dbo.Comments", "ParentCommentId", c => c.Int());
            AddColumn("dbo.Genres", "ParentGenre_GenreId", c => c.Int());
            AddColumn("dbo.Genres", "ParentGenreId", c => c.Int());
            DropIndex("dbo.Comments", new[] {"Comment_CommentId"});
            DropIndex("dbo.Genres", new[] {"Genre_GenreId"});
            DropForeignKey("dbo.Comments", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Genres", "Genre_GenreId", "dbo.Genres");
            DropColumn("dbo.Comments", "Comment_CommentId");
            DropColumn("dbo.Genres", "Genre_GenreId");
            CreateIndex("dbo.Comments", "ParentComment_CommentId");
            CreateIndex("dbo.Genres", "ParentGenre_GenreId");
            AddForeignKey("dbo.Comments", "ParentComment_CommentId", "dbo.Comments", "CommentId");
            AddForeignKey("dbo.Genres", "ParentGenre_GenreId", "dbo.Genres", "GenreId");
        }
    }
}