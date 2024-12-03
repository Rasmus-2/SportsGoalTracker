using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsGoalApp.Migrations
{
    /// <inheritdoc />
    public partial class fixingDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PracticeLog",
                table: "PracticeLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.RenameTable(
                name: "PracticeLog",
                newName: "Practices");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Practices",
                table: "Practices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Practices",
                table: "Practices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.RenameTable(
                name: "Practices",
                newName: "PracticeLog");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PracticeLog",
                table: "PracticeLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "Id");
        }
    }
}
