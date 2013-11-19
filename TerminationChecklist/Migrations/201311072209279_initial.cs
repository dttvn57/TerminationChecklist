namespace TerminationChecklist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HR_TerminationChecklist",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        date_personnel_notified = c.DateTime(nullable: false),
                        location = c.String(nullable: false),
                        last_work_day = c.DateTime(nullable: false),
                        classification = c.String(nullable: false),
                        delete_pin_day = c.DateTime(),
                        initials = c.String(),
                        specify_building_key = c.String(),
                        locker_num = c.String(),
                        notify_phone_coordinator = c.Boolean(nullable: false),
                        notify_network_coordinator = c.Boolean(nullable: false),
                        remove_user_folder = c.Boolean(nullable: false),
                        remove_infopac_password = c.Boolean(nullable: false),
                        notify_hr = c.Boolean(nullable: false),
                        secretarial_unit = c.Boolean(nullable: false),
                        remove_staff_status = c.Boolean(nullable: false),
                        collect_bldg_keys = c.String(),
                        special_keys = c.Boolean(nullable: false),
                        health_plan_binder_returned = c.Boolean(nullable: false),
                        name_badge = c.Boolean(nullable: false),
                        county_gas_card = c.Boolean(nullable: false),
                        conflict_of_interest_code_report = c.Boolean(nullable: false),
                        returned_to_itu = c.Boolean(nullable: false),
                        returned_to_accounting_manager = c.Boolean(nullable: false),
                        special_equipment = c.String(),
                        supervisor_signature = c.String(),
                        supervisor_signed_date = c.DateTime(),
                        created_date = c.DateTime(),
                        created_by_lname = c.String(),
                        created_by_fname = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HR_TerminationChecklist");
        }
    }
}
