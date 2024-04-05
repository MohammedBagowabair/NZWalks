using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations.NZWalksAuthDb
{
    /// <inheritdoc />
    public partial class addAuthTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d07802db-5ef7-4624-823a-5e9158cb1a32", "d07802db-5ef7-4624-823a-5e9158cb1a32", "Reader", "READER" },
                    { "f238d417-deed-4eea-949f-0cd4791e8fb4", "f238d417-deed-4eea-949f-0cd4791e8fb4", "Writer", "WRITER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d07802db-5ef7-4624-823a-5e9158cb1a32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f238d417-deed-4eea-949f-0cd4791e8fb4");
        }
    }
}
