using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class ReservedFromToDatesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ToDate",
                table: "OutfitsForCarts",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "OutfitsForCarts",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRangeEnd",
                table: "OutfitRenterDates",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRangeStart",
                table: "OutfitRenterDates",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRangeEnd",
                table: "OutfitRenterDates");

            migrationBuilder.DropColumn(
                name: "DateRangeStart",
                table: "OutfitRenterDates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ToDate",
                table: "OutfitsForCarts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "OutfitsForCarts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
