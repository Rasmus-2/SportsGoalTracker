using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsGoalApp.Migrations
{
    /// <inheritdoc />
    public partial class addeddurationunit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Goals_GoalId",
                table: "Practices");

            migrationBuilder.DropIndex(
                name: "IX_Practices_GoalId",
                table: "Practices");

            migrationBuilder.AddColumn<string>(
                name: "DurationUnit",
                table: "Practices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationUnit",
                table: "Practices");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_GoalId",
                table: "Practices",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Goals_GoalId",
                table: "Practices",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id");
        }
    }
}
