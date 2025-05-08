using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISB.Migrations
{
    /// <inheritdoc />
    public partial class plans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_plan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: false),
                    team_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_plan", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_plan_tbl_plantypes_type_id",
                        column: x => x.type_id,
                        principalTable: "tbl_plantypes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_plan_tbl_teams_team_id",
                        column: x => x.team_id,
                        principalTable: "tbl_teams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_plan_team_id",
                table: "tbl_plan",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_plan_type_id",
                table: "tbl_plan",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_plan");
        }
    }
}
