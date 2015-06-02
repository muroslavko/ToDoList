namespace test1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_subtask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subtasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Complete = c.Boolean(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subtasks", "TaskId", "dbo.Tasks");
            DropIndex("dbo.Subtasks", new[] { "TaskId" });
            DropTable("dbo.Subtasks");
        }
    }
}
