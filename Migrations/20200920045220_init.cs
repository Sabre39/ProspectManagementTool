using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProspectManagementTool.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PM");

            migrationBuilder.CreateTable(
                name: "Attributes",
                schema: "PM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttributeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "PM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(maxLength: 50, nullable: false),
                    TeamName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                schema: "PM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DraftName = table.Column<string>(maxLength: 70, nullable: false),
                    DraftPosition = table.Column<int>(nullable: false),
                    DraftOV = table.Column<byte>(nullable: false),
                    DraftPotential = table.Column<int>(nullable: false),
                    DraftAge = table.Column<byte>(nullable: false),
                    DraftInitialRating = table.Column<string>(maxLength: 6, nullable: false),
                    DraftSelected = table.Column<int>(nullable: true),
                    TeamID = table.Column<int>(nullable: true),
                    AttributeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Drafts_Attributes_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "PM",
                        principalTable: "Attributes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drafts_Teams_TeamID",
                        column: x => x.TeamID,
                        principalSchema: "PM",
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamProspectVM",
                schema: "PM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(maxLength: 50, nullable: false),
                    TeamName = table.Column<string>(maxLength: 50, nullable: false),
                    ProspectName = table.Column<string>(maxLength: 70, nullable: false),
                    ProspectPosition = table.Column<int>(nullable: false),
                    ProspectOV = table.Column<byte>(nullable: false),
                    ProspectPotential = table.Column<int>(nullable: false),
                    ProspectAge = table.Column<byte>(nullable: false),
                    ProspectInitialRating = table.Column<string>(maxLength: 6, nullable: false),
                    TeamID = table.Column<int>(nullable: false),
                    AttributeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamProspectVM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TeamProspectVM_Attributes_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "PM",
                        principalTable: "Attributes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamProspectVM_Teams_TeamID",
                        column: x => x.TeamID,
                        principalSchema: "PM",
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prospects",
                schema: "PM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProspectName = table.Column<string>(maxLength: 70, nullable: false),
                    ProspectPosition = table.Column<int>(nullable: false),
                    ProspectOV = table.Column<byte>(nullable: false),
                    ProspectPotential = table.Column<int>(nullable: false),
                    ProspectAge = table.Column<byte>(nullable: false),
                    ProspectRerollRating = table.Column<string>(maxLength: 4, nullable: true),
                    ProspectInitialRating = table.Column<string>(maxLength: 6, nullable: false),
                    TeamID = table.Column<int>(nullable: false),
                    AttributeID = table.Column<int>(nullable: false),
                    TeamProspectVMID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prospects_Attributes_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "PM",
                        principalTable: "Attributes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prospects_Teams_TeamID",
                        column: x => x.TeamID,
                        principalSchema: "PM",
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prospects_TeamProspectVM_TeamProspectVMID",
                        column: x => x.TeamProspectVMID,
                        principalSchema: "PM",
                        principalTable: "TeamProspectVM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_AttributeID",
                schema: "PM",
                table: "Drafts",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_TeamID",
                schema: "PM",
                table: "Drafts",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_AttributeID",
                schema: "PM",
                table: "Prospects",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_TeamID",
                schema: "PM",
                table: "Prospects",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_TeamProspectVMID",
                schema: "PM",
                table: "Prospects",
                column: "TeamProspectVMID");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_ProspectName_ProspectAge_ProspectPosition",
                schema: "PM",
                table: "Prospects",
                columns: new[] { "ProspectName", "ProspectAge", "ProspectPosition" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamProspectVM_AttributeID",
                schema: "PM",
                table: "TeamProspectVM",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamProspectVM_TeamID",
                schema: "PM",
                table: "TeamProspectVM",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drafts",
                schema: "PM");

            migrationBuilder.DropTable(
                name: "Prospects",
                schema: "PM");

            migrationBuilder.DropTable(
                name: "TeamProspectVM",
                schema: "PM");

            migrationBuilder.DropTable(
                name: "Attributes",
                schema: "PM");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "PM");
        }
    }
}
