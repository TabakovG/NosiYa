using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class OutfitPartOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "OutfitParts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[] { 4, 1, null, null, null, "Jeravna image" });

            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7"));

            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e"));

            migrationBuilder.UpdateData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Родопска детска носия за момче.\r\n                    Състои се от:\r\n                    - Риза\r\n                    - Елек\r\n                    - Панталон\r\n                    - Пояс\r\n\r\n                    Подходяща за момче между 7 и 9 години.\r\n                    ");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitParts_OwnerId",
                table: "OutfitParts",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitParts_AspNetUsers_OwnerId",
                table: "OutfitParts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutfitParts_AspNetUsers_OwnerId",
                table: "OutfitParts");

            migrationBuilder.DropIndex(
                name: "IX_OutfitParts_OwnerId",
                table: "OutfitParts");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "OutfitParts");

            migrationBuilder.UpdateData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Родопска детска носия за момче.Риза, елек, панталон и пояс,\r\n                    Състои се от:\r\n                    - Риза\r\n                    - Елек\r\n                    - Панталон\r\n                    - Пояс\r\n\r\n                    Подходяща за момче между 7 и 9 години.\r\n                    ");
        }
    }
}
