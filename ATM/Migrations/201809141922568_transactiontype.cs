namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactiontype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "TransactionType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "TransactionType");
        }
    }
}
