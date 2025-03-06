using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISB.Migrations
{
    /// <inheritdoc />
    public partial class auditconclusions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblaudit_Conclusions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rating_id = table.Column<int>(type: "int", nullable: false),
                    Summary_Findings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblaudit_Conclusions", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblaudit_Conclusions_tbl_auditRating_rating_id",
                        column: x => x.rating_id,
                        principalTable: "tbl_auditRating",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblaudit_Conclusions_rating_id",
                table: "tblaudit_Conclusions",
                column: "rating_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblaudit_Conclusions");
        }
    }
}
