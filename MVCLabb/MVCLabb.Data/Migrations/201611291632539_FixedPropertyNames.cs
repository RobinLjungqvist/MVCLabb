namespace MVCLabb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedPropertyNames : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentEntityModels", "Pictures_id", "dbo.PictureEntityModels");
            DropForeignKey("dbo.CommentEntityModels", "Users_id", "dbo.UserEntityModels");
            DropForeignKey("dbo.PictureEntityModels", "Users_id", "dbo.UserEntityModels");
            DropIndex("dbo.CommentEntityModels", new[] { "Pictures_id" });
            DropIndex("dbo.CommentEntityModels", new[] { "Users_id" });
            DropIndex("dbo.PictureEntityModels", new[] { "Users_id" });
            DropColumn("dbo.CommentEntityModels", "PictureID");
            DropColumn("dbo.CommentEntityModels", "UserID");
            DropColumn("dbo.PictureEntityModels", "UserID");
            RenameColumn(table: "dbo.CommentEntityModels", name: "Pictures_id", newName: "PictureID");
            RenameColumn(table: "dbo.CommentEntityModels", name: "Users_id", newName: "UserID");
            RenameColumn(table: "dbo.PictureEntityModels", name: "Users_id", newName: "UserID");
            AlterColumn("dbo.CommentEntityModels", "PictureID", c => c.Int(nullable: false));
            AlterColumn("dbo.CommentEntityModels", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.PictureEntityModels", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.CommentEntityModels", "UserID");
            CreateIndex("dbo.CommentEntityModels", "PictureID");
            CreateIndex("dbo.PictureEntityModels", "UserID");
            AddForeignKey("dbo.CommentEntityModels", "PictureID", "dbo.PictureEntityModels", "id", cascadeDelete: true);
            AddForeignKey("dbo.CommentEntityModels", "UserID", "dbo.UserEntityModels", "id", cascadeDelete: false);
            AddForeignKey("dbo.PictureEntityModels", "UserID", "dbo.UserEntityModels", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PictureEntityModels", "UserID", "dbo.UserEntityModels");
            DropForeignKey("dbo.CommentEntityModels", "UserID", "dbo.UserEntityModels");
            DropForeignKey("dbo.CommentEntityModels", "PictureID", "dbo.PictureEntityModels");
            DropIndex("dbo.PictureEntityModels", new[] { "UserID" });
            DropIndex("dbo.CommentEntityModels", new[] { "PictureID" });
            DropIndex("dbo.CommentEntityModels", new[] { "UserID" });
            AlterColumn("dbo.PictureEntityModels", "UserID", c => c.Int());
            AlterColumn("dbo.CommentEntityModels", "UserID", c => c.Int());
            AlterColumn("dbo.CommentEntityModels", "PictureID", c => c.Int());
            RenameColumn(table: "dbo.PictureEntityModels", name: "UserID", newName: "Users_id");
            RenameColumn(table: "dbo.CommentEntityModels", name: "UserID", newName: "Users_id");
            RenameColumn(table: "dbo.CommentEntityModels", name: "PictureID", newName: "Pictures_id");
            AddColumn("dbo.PictureEntityModels", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.CommentEntityModels", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.CommentEntityModels", "PictureID", c => c.Int(nullable: false));
            CreateIndex("dbo.PictureEntityModels", "Users_id");
            CreateIndex("dbo.CommentEntityModels", "Users_id");
            CreateIndex("dbo.CommentEntityModels", "Pictures_id");
            AddForeignKey("dbo.PictureEntityModels", "Users_id", "dbo.UserEntityModels", "id");
            AddForeignKey("dbo.CommentEntityModels", "Users_id", "dbo.UserEntityModels", "id");
            AddForeignKey("dbo.CommentEntityModels", "Pictures_id", "dbo.PictureEntityModels", "id");
        }
    }
}
