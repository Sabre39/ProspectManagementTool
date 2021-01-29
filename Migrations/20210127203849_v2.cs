using Microsoft.EntityFrameworkCore.Migrations;

namespace ProspectManagementTool.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProspectDraftedBy",
                schema: "PM",
                table: "Prospects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProspectDraftedBy",
                schema: "PM",
                table: "Prospects");
        }
    }
}
