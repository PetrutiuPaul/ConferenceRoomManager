namespace ConferenceRoomManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeNotifyField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rooms", "Notify");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Notify", c => c.Boolean(nullable: false));
        }
    }
}
