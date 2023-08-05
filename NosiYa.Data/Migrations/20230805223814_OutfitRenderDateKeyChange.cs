using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class OutfitRenderDateKeyChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutfitRenterDates",
                table: "OutfitRenterDates");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "OutfitRenterDates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutfitRenterDates",
                table: "OutfitRenterDates",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitRenterDates_OutfitId",
                table: "OutfitRenterDates",
                column: "OutfitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutfitRenterDates",
                table: "OutfitRenterDates");

            migrationBuilder.DropIndex(
                name: "IX_OutfitRenterDates_OutfitId",
                table: "OutfitRenterDates");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "OutfitRenterDates",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutfitRenterDates",
                table: "OutfitRenterDates",
                columns: new[] { "OutfitId", "Date" });
        }
    }
}
