using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWDifficultiesTODifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WDifficulties_DifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WDifficulties",
                table: "WDifficulties");

            migrationBuilder.RenameTable(
                name: "WDifficulties",
                newName: "Difficulties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Difficulties_DifficultyId",
                table: "Walks",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Difficulties_DifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties");

            migrationBuilder.RenameTable(
                name: "Difficulties",
                newName: "WDifficulties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WDifficulties",
                table: "WDifficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WDifficulties_DifficultyId",
                table: "Walks",
                column: "DifficultyId",
                principalTable: "WDifficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
