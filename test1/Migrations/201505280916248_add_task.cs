namespace test1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_task : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(),
                        Complete = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            DropColumn("dbo.Categories", "NewCat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "NewCat", c => c.String());
            DropForeignKey("dbo.Tasks", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Tasks", new[] { "CategoryId" });
            DropTable("dbo.Tasks");
        }
    }
}
