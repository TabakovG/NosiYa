using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class SizeFormatDefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Size",
                value: "-XS-");

            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Size",
                value: "-XS-");

            migrationBuilder.UpdateData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Size",
                value: "-XS-S-");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Size",
                value: "XS");

            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Size",
                value: "XS");

            migrationBuilder.UpdateData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Size",
                value: "XS");
        }
    }
}
