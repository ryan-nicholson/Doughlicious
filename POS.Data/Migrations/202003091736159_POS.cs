namespace POS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnythingElse : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "Id", newName: "EmployeeId");
            RenameIndex(table: "dbo.Orders", name: "IX_Id", newName: "IX_EmployeeId");
            AlterColumn("dbo.Pizzas", "EmployeeId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pizzas", "EmployeeId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Orders", name: "IX_EmployeeId", newName: "IX_Id");
            RenameColumn(table: "dbo.Orders", name: "EmployeeId", newName: "Id");
        }
    }
}
