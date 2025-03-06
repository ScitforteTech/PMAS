using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISB.Migrations
{
    /// <inheritdoc />
    public partial class risks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_riskmangament",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentifiedRisks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Risk_Mitigation_Plans = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likelihood = table.Column<int>(type: "int", nullable: false),
                    impact = table.Column<int>(type: "int", nullable: false),
                    Risk_Scoring = table.Column<int>(type: "int", nullable: false),
                    Stakeholder_id = table.Column<int>(type: "int", nullable: false),
                    Effectiveness_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_riskmangament", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_riskmangament_tbl_CommunicationEffectiveness_Effectiveness_id",
                        column: x => x.Effectiveness_id,
                        principalTable: "tbl_CommunicationEffectiveness",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_riskmangament_tbl_StakeholderEngagement_Stakeholder_id",
                        column: x => x.Stakeholder_id,
                        principalTable: "tbl_StakeholderEngagement",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_riskmangament_Effectiveness_id",
                table: "tbl_riskmangament",
                column: "Effectiveness_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_riskmangament_Stakeholder_id",
                table: "tbl_riskmangament",
                column: "Stakeholder_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_riskmangament");
        }
    }
}
