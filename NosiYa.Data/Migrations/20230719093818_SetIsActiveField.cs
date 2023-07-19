using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class SetIsActiveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutfitSets_Regions_RegionId",
                table: "OutfitSets");

            migrationBuilder.RenameColumn(
                name: "OutfitType",
                table: "OutfitParts",
                newName: "OutfitPartType");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Regions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "OutfitSets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitSets_Regions_RegionId",
                table: "OutfitSets",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutfitSets_Regions_RegionId",
                table: "OutfitSets");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Regions");

            migrationBuilder.RenameColumn(
                name: "OutfitPartType",
                table: "OutfitParts",
                newName: "OutfitType");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "OutfitSets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitSets_Regions_RegionId",
                table: "OutfitSets",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }
    }
}
