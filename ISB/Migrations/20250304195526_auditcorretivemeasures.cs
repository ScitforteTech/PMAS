using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISB.Migrations
{
    /// <inheritdoc />
    public partial class auditcorretivemeasures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_auditCorrectiveMeasures",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImprovementSuggestions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priority_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_auditCorrectiveMeasures", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_auditCorrectiveMeasures_tbl_measurePrority_priority_id",
                        column: x => x.priority_id,
                        principalTable: "tbl_measurePrority",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_auditCorrectiveMeasures_priority_id",
                table: "tbl_auditCorrectiveMeasures",
                column: "priority_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_auditCorrectiveMeasures");
        }
    }
}
