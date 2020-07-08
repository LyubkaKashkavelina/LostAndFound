using Microsoft.EntityFrameworkCore.Migrations;

namespace LostAndFound.Migrations
{
    public partial class SeedRegions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionId", "RegionDescription", "RegionName" },
                values: new object[] { 1, null, "Blagoevgrad" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionId",
                keyValue: 1);
        }
    }
}
