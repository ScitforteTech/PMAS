using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISB.Migrations
{
    /// <inheritdoc />
    public partial class oneplans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_oneplan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parent_Plan = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team = table.Column<int>(type: "int", nullable: false),
                    timeframe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_oneplan", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_oneplan_tbl_teams_team",
                        column: x => x.team,
                        principalTable: "tbl_teams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_oneplan_team",
                table: "tbl_oneplan",
                column: "team");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_oneplan");
        }
    }
}
