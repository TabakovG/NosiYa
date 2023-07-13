using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class EventsAndCommentInitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EventEndDate", "EventStartDate", "IsApproved", "Location", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "Фестивалът е създаден през 2008 година по идея на Христо Димитров – продуцент, хореограф и режисьор на Национален фолклорен ансамбъл „Българе”. Атрактивната им сватба с фолклорната певица Албена Вескова през 2005 г. в местността \"Костина\" край с. Рибарица по старинен български обичай, на която младоженците и присъстващите 400 гости са с български народни носии, има широк позитивен отзвук. Това мотивира създателя на „Българе” с помощта на неговите партньори Ян и Елена Андерсон, на тогавашния кмет на Жеравна Лъчезар Германов, бизнесмените Георги Манев и Калин Григоров, както и на други съмишленици, да организира много по-мащабно събитие, което да дава възможност на всеки желаещ поне за три дни в годината да облече българска носия и се откъсне от цивилизацията, като направи скок век и половина назад във времето.", new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "42.8336195015454, 26.45904413725397", "Фестивал на фолклорната носия - Жеравна", new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") },
                    { 2, "Това мотивира създателя на „Българе” с помощта на неговите партньори Ян и Елена Андерсон, на тогавашния кмет на Жеравна Лъчезар Германов, бизнесмените Георги Манев и Калин Григоров, както и на други съмишленици, да организира много по-мащабно събитие, което да дава възможност на всеки желаещ поне за три дни в годината да облече българска носия и се откъсне от цивилизацията, като направи скок век и половина назад във времето.", new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "42.8336195015454, 26.45904413725397", "Фестивал на  Жеравна", new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e") }
                });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "Url",
                value: "Shirt image");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "Url",
                value: "Vest image");

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "EventId", "IsApproved", "OwnerId" },
                values: new object[,]
                {
                    { 1, "Страхотна локация! :) ", 1, true, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") },
                    { 2, "бля бля бля", 1, false, new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e") },
                    { 3, "Този фестивал вече е добавен.", 2, true, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[] { 5, 1, null, null, null, "Jeravna image 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "Url",
                value: "Set image");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "Url",
                value: "Set image");
        }
    }
}
