using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilleterieParis2024.Data.Migrations
{
    public partial class AddImageAndAvailable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "TicketsOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "TicketsOffers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "TicketsOffers");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "TicketsOffers");
        }
    }
}
