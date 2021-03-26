namespace BlogManageentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "DateOfUpload", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blogs", "DateOfUpload", c => c.String());
        }
    }
}
