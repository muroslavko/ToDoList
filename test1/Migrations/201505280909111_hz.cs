namespace test1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "NewCat", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "NewCat");
        }
    }
}
