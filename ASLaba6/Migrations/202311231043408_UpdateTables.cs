namespace ASLaba6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Headphones", "CountryId", c => c.Int());
            CreateIndex("dbo.Headphones", "CountryId");
            AddForeignKey("dbo.Headphones", "CountryId", "dbo.Countries", "ID");
            DropColumn("dbo.Headphones", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Headphones", "Country", c => c.String());
            DropForeignKey("dbo.Headphones", "CountryId", "dbo.Countries");
            DropIndex("dbo.Headphones", new[] { "CountryId" });
            DropColumn("dbo.Headphones", "CountryId");
            DropTable("dbo.Countries");
        }
    }
}
