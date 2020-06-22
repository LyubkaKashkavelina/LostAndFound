using Microsoft.EntityFrameworkCore.Migrations;

namespace LostAndFound.Migrations
{
    public partial class AddGenderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderType",
                table: "Ads",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenderType",
                table: "Ads");
        }
    }
}
