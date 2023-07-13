using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class OutfitSetSeedTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_OutfitParts_Image",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_OutfitParts_OutfitSets_OutfitSetId",
                table: "OutfitParts");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Images",
                newName: "OutfitPartId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_Image",
                table: "Images",
                newName: "IX_Images_OutfitPartId");

            migrationBuilder.AlterColumn<int>(
                name: "OutfitSetId",
                table: "OutfitParts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "OutfitSets",
                columns: new[] { "Id", "Color", "Description", "IsAvailable", "Name", "PricePerDay", "RegionId", "RenterType", "Size" },
                values: new object[] { 1, "Кафяв", "Родопска детска носия за момче.Риза, елек, панталон и пояс,\r\n                    Състои се от:\r\n                    - Риза\r\n                    - Елек\r\n                    - Панталон\r\n                    - Пояс\r\n\r\n                    Подходяща за момче между 7 и 9 години.\r\n                    ", true, "Носия 17", 25m, 1, 3, "XS" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[] { 1, null, null, 1, null, "Set image" });

            migrationBuilder.InsertData(
                table: "OutfitParts",
                columns: new[] { "Id", "Color", "Description", "Name", "OutfitSetId", "OutfitType", "RenterType", "Size" },
                values: new object[] { 1, "бял", "Риза: \r\n                            Рамене - 31\r\n                            Гръдна обиколка - 73", "Детска Риза", 1, 4, 3, "XS" });

            migrationBuilder.InsertData(
                table: "OutfitParts",
                columns: new[] { "Id", "Color", "Description", "Name", "OutfitSetId", "OutfitType", "RenterType", "Size" },
                values: new object[] { 2, "кафяв", "Елек: \r\n                            Рамене - 32\r\n                            Дължина - 34\r\n                            Отвор за ръкав - 27", "Детски Елек", 1, 4, 3, "XS" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[] { 2, null, 1, null, null, "Set image" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[] { 3, null, 2, null, null, "Set image" });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_OutfitParts_OutfitPartId",
                table: "Images",
                column: "OutfitPartId",
                principalTable: "OutfitParts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitParts_OutfitSets_OutfitSetId",
                table: "OutfitParts",
                column: "OutfitSetId",
                principalTable: "OutfitSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_OutfitParts_OutfitPartId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_OutfitParts_OutfitSets_OutfitSetId",
                table: "OutfitParts");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "OutfitPartId",
                table: "Images",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Images_OutfitPartId",
                table: "Images",
                newName: "IX_Images_Image");

            migrationBuilder.AlterColumn<int>(
                name: "OutfitSetId",
                table: "OutfitParts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_OutfitParts_Image",
                table: "Images",
                column: "Image",
                principalTable: "OutfitParts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitParts_OutfitSets_OutfitSetId",
                table: "OutfitParts",
                column: "OutfitSetId",
                principalTable: "OutfitSets",
                principalColumn: "Id");
        }
    }
}
