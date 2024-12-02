using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsGoalApp.Migrations
{
    /// <inheritdoc />
    public partial class addedUserIdPracticelog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Practices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Practices");
        }
    }
}
