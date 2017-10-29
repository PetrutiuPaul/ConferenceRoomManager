namespace ConferenceRoomManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNotifyField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Notify", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Notify");
        }
    }
}
