namespace ASLaba6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Headphones", "Guarantee", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Headphones", "Guarantee");
        }
    }
}
