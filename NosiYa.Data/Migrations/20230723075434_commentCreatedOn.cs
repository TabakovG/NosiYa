using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class commentCreatedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Comments");
        }
    }
}
