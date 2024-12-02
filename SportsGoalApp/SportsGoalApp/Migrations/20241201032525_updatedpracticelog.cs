using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsGoalApp.Migrations
{
    /// <inheritdoc />
    public partial class updatedpracticelog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Goals_GoalId",
                table: "Practices");

            migrationBuilder.DropIndex(
                name: "IX_Practices_GoalId",
                table: "Practices");
        }
    }
}
