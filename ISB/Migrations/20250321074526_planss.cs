using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISB.Migrations
{
    /// <inheritdoc />
    public partial class planss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "time_frame",
                table: "tbl_plan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time_frame",
                table: "tbl_plan");
        }
    }
}
