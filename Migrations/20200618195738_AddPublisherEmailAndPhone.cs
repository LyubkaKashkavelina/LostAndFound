using Microsoft.EntityFrameworkCore.Migrations;

namespace LostAndFound.Migrations
{
    public partial class AddPublisherEmailAndPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublisherEmail",
                table: "Ads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublisherPhone",
                table: "Ads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublisherEmail",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "PublisherPhone",
                table: "Ads");
        }
    }
}
