namespace ASLaba6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Headphones",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Price = c.Int(),
                        Color = c.String(),
                        Brand = c.String(),
                        Country = c.String()
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Headphones");
        }
    }
}
