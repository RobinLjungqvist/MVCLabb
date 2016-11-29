namespace MVCLabb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryEntityModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        GalleryName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserEntityModels", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.PictureEntityModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Path = c.String(),
                        UserID = c.Int(nullable: false),
                        DatePosted = c.DateTime(),
                        DateEdited = c.DateTime(),
                        _public = c.Boolean(name: "public", nullable: false),
                        GalleryID = c.Int(nullable: false),
                        Users_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserEntityModels", t => t.Users_id)
                .ForeignKey("dbo.GalleryEntityModels", t => t.GalleryID, cascadeDelete: true)
                .Index(t => t.GalleryID)
                .Index(t => t.Users_id);
            
            CreateTable(
                "dbo.CommentEntityModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Comment = c.String(),
                        UserID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                        DatePosted = c.DateTime(),
                        DateEdited = c.DateTime(),
                        Pictures_id = c.Int(),
                        Users_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.PictureEntityModels", t => t.Pictures_id)
                .ForeignKey("dbo.UserEntityModels", t => t.Users_id)
                .Index(t => t.Pictures_id)
                .Index(t => t.Users_id);
            
            CreateTable(
                "dbo.UserEntityModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PictureEntityModels", "GalleryID", "dbo.GalleryEntityModels");
            DropForeignKey("dbo.PictureEntityModels", "Users_id", "dbo.UserEntityModels");
            DropForeignKey("dbo.GalleryEntityModels", "UserID", "dbo.UserEntityModels");
            DropForeignKey("dbo.CommentEntityModels", "Users_id", "dbo.UserEntityModels");
            DropForeignKey("dbo.CommentEntityModels", "Pictures_id", "dbo.PictureEntityModels");
            DropIndex("dbo.CommentEntityModels", new[] { "Users_id" });
            DropIndex("dbo.CommentEntityModels", new[] { "Pictures_id" });
            DropIndex("dbo.PictureEntityModels", new[] { "Users_id" });
            DropIndex("dbo.PictureEntityModels", new[] { "GalleryID" });
            DropIndex("dbo.GalleryEntityModels", new[] { "UserID" });
            DropTable("dbo.UserEntityModels");
            DropTable("dbo.CommentEntityModels");
            DropTable("dbo.PictureEntityModels");
            DropTable("dbo.GalleryEntityModels");
        }
    }
}
