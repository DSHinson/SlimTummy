using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Generic.Database.Migrations.Default
{
    [Migration(20221016)]
    public class PageGeneration:Migration
    {
        public override void Up()
        {
            Create.Schema("Page");
            Create.Table("TM_PGE_Page").InSchema("Page")
                .WithColumn("pge_PageID").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("pge_PageName").AsString(150).NotNullable();

            Create.Table("TR_FRM_Forms").InSchema("Page")
                .WithColumn("frm_FormID").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("frm_pge_PageID").AsInt32().NotNullable()
                .ForeignKey(foreignKeyName: "FK_frm_pge_PageID_TM_PGE_Page", primaryTableSchema: "Page", "TM_PGE_Page", "pge_PageID")
                .WithColumn("frm_FormName").AsString(150).NotNullable()
                .WithColumn("frm_PostBackUrl").AsString(250).NotNullable();

            Create.Table("TR_CTL_Control").InSchema("Page")
                .WithColumn("ctl_ControlID").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("ctl_frm_FormID").AsInt32().NotNullable()
                .ForeignKey(foreignKeyName: "FK_ctl_frm_FormID_TR_FRM_Forms", primaryTableSchema: "Page", "TR_FRM_Forms", "frm_FormID")
                .WithColumn("ctl_FormKey").AsString(100).NotNullable()
                .WithColumn("ctl_FormLabel").AsString(100).NotNullable()
                .WithColumn("ctl_InputType").AsString(100).NotNullable()
                .WithColumn("ctl_DefaultValue").AsString(100).Nullable();

            Create.Table("TR_OPT_ControlOption").InSchema("Page")
                .WithColumn("itm_ItemID").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("itm_ctl_ControlID").AsInt32().NotNullable()
                .ForeignKey(foreignKeyName: "FK_ctl_ControlID_TR_CTL_Control", primaryTableSchema: "Page", "TR_CTL_Control", "ctl_ControlID")
                .WithColumn("itm_Key").AsString(150).NotNullable()
                .WithColumn("itm_Value").AsString(150).Nullable();
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_ctl_ControlID_TR_CTL_Control").OnTable("TR_OPT_ControlOption").InSchema("Page");
            Delete.ForeignKey("FK_ctl_frm_FormID_TR_FRM_Forms").OnTable("TR_CTL_Control").InSchema("Page");
            Delete.ForeignKey("FK_frm_pge_PageID_TM_PGE_Page").OnTable("TR_FRM_Forms").InSchema("Page");
           

            Delete.Table("TR_OPT_ControlOption").InSchema("Page");
            Delete.Table("TR_CTL_Control").InSchema("Page");
            Delete.Table("TR_FRM_Forms").InSchema("Page");

            Delete.Schema("Page");
        }
    }
}
