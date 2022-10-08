using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Generic.Database.Migrations.Default
{
    [Migration(2)]
    public class BaseTables:Migration
    {
        public override void Up()
        {
            Create.Schema("Action");
            Create.Table("TM_MET_Methods")
                .WithDescription("This table contains a list of functions accessible via the api")
                .InSchema("Action").WithColumn("met_MethodID").AsInt32().PrimaryKey().Identity(1, 1)
                .WithColumn("met_DisplayName").AsString(500).NotNullable()
                .WithColumn("met_Area").AsString(100).NotNullable()
                .WithColumn("met_Method").AsString(100).NotNullable()
                .WithColumn("met_Command").AsCustom("nvarchar(max)").NotNullable()
                .WithColumn("met_Version").AsInt32().NotNullable()
                .WithColumn("met_Enabled").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("TM_MET_Methods").InSchema("Action");
           Delete.Schema("Action");
        }
    }
}
