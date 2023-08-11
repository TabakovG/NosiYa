using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NosiYa.Data.Migrations
{
    public partial class RealDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Images",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "EventId", "IsActive", "IsApproved", "ModifiedContent", "OwnerId" },
                values: new object[,]
                {
                    { 71, "Здравейте, Тази година входа за паркинга ще е зад сцената! ", 1, true, true, null, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") },
                    { 72, "Някой знае ли дали може да се плати вход само за първия ден?", 1, true, true, null, new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e") },
                    { 73, "Миналата година можеше. Цената беше 10лв.", 1, true, true, null, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") },
                    { 74, "Този фестивал вече е добавен. Можете да премахнете това събитие.", 2, true, true, null, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventEndDate", "EventStartDate" },
                values: new object[] { new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EventEndDate", "EventStartDate", "IsActive", "IsApproved", "Location", "Name", "OwnerId" },
                values: new object[] { 73, "Местоположение на сцените\r\n\r\nПешеходна зона пред Национален исторически музей, Бояна – 10.07, 11.07, 12.07, 13.07, 14.07 и15.07\r\nКино Кабана – 11.07\r\nс. Владая, НЧ „Светлина – 1906“ – 12.07\r\nкв. Княжево, пл. „Сред село“ – 13.07\r\nкв. Симеоново, НЧ „Отец Паисий“ – 14.07\r\nкв. Драгалевци, ул. „Карнобатски проход“ – 13.07\r\nкв. Бояна, парк „Воденичница“ – 14.07\r\nс. Мърчаево, 152 ОУ – 12.07\r\nМега Мол София, бул. „Царица Йоанна“ № 15 – 15.07", new DateTime(2023, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "42.652049852021584, 23.26517291652124", "Международен фолклорен фестивал „Витоша", new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e") });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "IsDefault", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[,]
                {
                    { 171, null, true, null, 1, null, "/images/common/nosiq1/3245e510-10ee-4ac5-a181-3141cbef91a7.jpg" },
                    { 172, null, false, null, 1, null, "/images/common/nosiq1/da5b3ea8-df94-4602-a255-251ac228396a.jpg" },
                    { 173, null, false, null, 1, null, "/images/common/nosiq1/f24a886e-d320-4f8d-afe3-2156eca67d0c.jpg" },
                    { 177, 1, false, null, null, null, "/images/event/02b1f635-597e-421c-b7c0-25542c6bf6fe.jpg" },
                    { 178, 1, false, null, null, null, "/images/event/92c33c52-81c0-4234-9540-d110fe071f97.jpeg" },
                    { 179, 2, true, null, null, null, "/images/event/92c33c52-81c0-4234-9540-d110fe071f97.jpeg" },
                    { 183, null, true, null, null, 1, "/images/region/12f52c5a-24ac-4b8b-99fb-34e245967555.jpg" },
                    { 184, null, false, null, null, 1, "/images/region/85deec0a-67cc-47ab-997b-a17ad7546041.jpg" },
                    { 188, null, true, 1, null, null, "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg" },
                    { 189, null, true, 2, null, null, "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg" }
                });

            migrationBuilder.InsertData(
                table: "OutfitParts",
                columns: new[] { "Id", "Color", "Description", "IsActive", "Name", "OutfitPartType", "OutfitSetId", "OwnerId", "RenterType", "Size" },
                values: new object[,]
                {
                    { 5, "черен", "Ширина - 46см, Дължина - 57см", true, "Престилка", 2, 1, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7"), 2, "-S-М-" },
                    { 6, "бял", "Рамене - 41см, Гръдна обиколка - 106см, Дължина - 56см, Ръкав обиколка - 36см, Ръкав дължина от рамото - 49", true, "Риза", 4, 1, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7"), 2, "-S-М-" },
                    { 7, "червен", "Рамене - 40см, Гръдна обиколка - 90см, Талия - 74см, Дължина - 103см", true, "Сукман", 6, 1, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7"), 2, "-S-М-" }
                });

            migrationBuilder.InsertData(
                table: "OutfitRenterDates",
                columns: new[] { "OrderId", "DateRangeEnd", "DateRangeStart", "IsActive", "IsApproved", "OutfitId", "RenterId" },
                values: new object[,]
                {
                    { new Guid("13bf08ed-1805-4eb6-9b60-cc56834e567c"), new DateTime(2023, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 1, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") },
                    { new Guid("c17279d7-83ce-4acb-9890-f6f8d47949f5"), new DateTime(2023, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") }
                });

            migrationBuilder.InsertData(
                table: "OutfitSets",
                columns: new[] { "Id", "Color", "Description", "IsActive", "IsAvailable", "Name", "PricePerDay", "RegionId", "RenterType", "Size" },
                values: new object[,]
                {
                    { 2, "Кафяв", "Родопска детска носия за момче.\r\n                    Състои се от:\r\n                    - Риза\r\n                    - Елек\r\n                    - Панталон\r\n                    - Пояс\r\n\r\n                    Подходяща за момче между 7 и 9 години.\r\n                    ", true, true, "Носия 17", 30m, 1, 3, "-XS-S-" },
                    { 73, "", "Тази носия е неактивна и служи като контейнер за елементите/частите, когато са временно неактивни.\r\n					Тази носия не трябва да бъде активирана!\r\n                    ", true, false, "In maintenance", 0m, 1, 1, "-S-" }
                });

            migrationBuilder.InsertData(
                table: "OutfitsForCarts",
                columns: new[] { "Id", "CartId", "FromDate", "IsActive", "OutfitId", "ToDate" },
                values: new object[,]
                {
                    { 171, 1, new DateTime(2023, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, new DateTime(2023, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 172, 1, new DateTime(2023, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, new DateTime(2023, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 173, 2, new DateTime(2023, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, new DateTime(2023, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Родопската фолклорна област преди е била включена като район от тракийската област. Обособяването ѝ като самостоятелна се е наложило от по-съществените различия между двете области.\r\n\r\nРодопските танци са бавни и умерени с малко разнообразие на движенията и сравнителна простота. Играят се най-често на песен, като характерно тук е, че мъжете също пеят.\r\n\r\nХора̀та се играят в полукръг или кръг и най-често са само мъжки или само женски. Срещат се и разделно-смесени хора, но при тях мъжете и жените не се нареждат един до друг мъж-жена, а в началото на хорото се хващат само мъжете, а след тях – жените.\r\n\r\nМъжете се залавят най-често за длани, което е много характерно за Родопите. В другите области този захват е много рядко срещан. Мъжете играят с широки стъпки, клякат и коленичат бавно и тромаво.");

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 72, "Шопската фолклорна област има няколко района – софийски, граовски, кюстендилски, самоковски, ихтимански и годечки, в които има известни различия, въпреки общото в стила.\r\n\r\nТанцуването предизвиква силни преживявания в емоционалната душа на шопите, което се изразява в мощни провиквания, ръмжене и свиркане. Добре известно е движението „натрисане” – силно раздрусване на раменете, предизвикано от динамичните, твърди и отсечени движения в краката, както и от постановката на тялото и хвата за пояс. Високото повдигане на крака е проява на сила и мъжественост. Характерен е подчертаният стремеж за откъсване от земята.", true, "Шопски Регион" },
                    { 73, "Обхваща Родопа планина.Тракийската фолклорна област обикновено се разделя на три района – западнотракийски, източнотракийски и странджански.\r\n\r\nМного от странджанските танци са във връзка с някакъв обред. Най-типични са нестинарските танци, великденските ръченици и кукерските игри. Странджанците играят с голяма съсредоточеност и сериозност. Мъжете респектират със сложни пляскания, а ръцете им излъчват сила и достойнство.\r\n\r\nАкцентът в тракийските танци е движението, насочено надолу – олицетворение на почитта към земята. В някои танци ясно се виждат заемки от битови трудови процеси като месене на хляб и точене на тесто. Тракиецът слива празника и делника в танца си, показвайки, че всеки ден е своеобразен празник.\r\n\r\n", true, "Тракийски регион" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "EventId", "IsActive", "IsApproved", "ModifiedContent", "OwnerId" },
                values: new object[] { 75, "Международен фолклорен фестивал „Витоша“ е неделима част от Културния календар на София. Провеждането му е уникална възможност за българската публика да се запознае с музикалната и танцова традиция на държави от цял свят. Това е шанс младата аудитория да види най-атрактивното лице на фолклора.", 73, true, true, null, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "IsDefault", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[,]
                {
                    { 174, null, true, null, 2, null, "/images/common/nosiq17/054848d5-9b31-4b18-a1a2-bc9f91bac96d.jpg" },
                    { 175, null, false, null, 2, null, "/images/common/nosiq17/82c31014-e3d5-42b6-b7ad-0bb533e401ef.jpg" },
                    { 176, null, false, null, 2, null, "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg" },
                    { 180, 73, true, null, null, null, "/images/event/f74ddf23-68d9-4b58-a982-18f3cba1a1f3.jpg" },
                    { 181, 73, false, null, null, null, "/images/event/59b6dce3-8fe8-42a1-8998-7967e282c7b8.jpg" },
                    { 182, 73, false, null, null, null, "/images/event/30fde865-6e69-4408-b490-ff5b0c49e70e.jpg" },
                    { 185, null, true, null, null, 72, "/images/region/04d45bb6-1100-453f-9377-862a4c10dda6.jpg" },
                    { 186, null, false, null, null, 72, "/images/region/1ccc5fc0-15c9-4fa9-bad8-a3f23ca301c1.jpg" },
                    { 187, null, true, null, null, 73, "/images/region/ce68a779-2c71-488b-8cde-25b01df35ec5.jpg" },
                    { 192, null, true, 5, null, null, "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg" },
                    { 193, null, true, 6, null, null, "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg" },
                    { 194, null, true, 7, null, null, "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg" }
                });

            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "OutfitSetId" },
                values: new object[] { "Риза: \r\n                            Рамене - 31см\r\n                            Гръдна обиколка - 73см,\r\n							Дължина - 41см,\r\n							Обиколка на ръкав - 30см,\r\n							Дължина на ръкав от рамото - 42см", 2 });

            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "OutfitPartType", "OutfitSetId", "OwnerId" },
                values: new object[] { "Елек: \r\n                            Рамене - 32см\r\n                            Дължина - 34см\r\n                            Отвор за ръкав - 27см", 5, 2, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") });

            migrationBuilder.InsertData(
                table: "OutfitParts",
                columns: new[] { "Id", "Color", "Description", "IsActive", "Name", "OutfitPartType", "OutfitSetId", "OwnerId", "RenterType", "Size" },
                values: new object[,]
                {
                    { 3, "кафяв", "Талия - регулира се с връзки,\r\n								Дължина - 71см", true, "Панталон", 8, 2, new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e"), 3, "-XS-" },
                    { 4, "червен", "Ширина - 27см, Дължина - 146см", true, "Пояс", 1, 2, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7"), 3, "-XS-" }
                });

            migrationBuilder.InsertData(
                table: "OutfitRenterDates",
                columns: new[] { "OrderId", "DateRangeEnd", "DateRangeStart", "IsActive", "IsApproved", "OutfitId", "RenterId" },
                values: new object[,]
                {
                    { new Guid("dcf5cfb0-1fed-4808-8421-cddfca785e09"), new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 2, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") },
                    { new Guid("f5e540b6-949c-4dc4-9fd3-b775590cc023"), new DateTime(2023, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 2, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") }
                });

            migrationBuilder.UpdateData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Description", "Name", "RegionId", "RenterType", "Size" },
                values: new object[] { "Червен", "Тракийска женска носия.\r\n                    Състои се от:\r\n                    - Риза\r\n					-Сукман\r\n					-Престилка\r\n\r\n                    Ръчно шити орнаменти. \r\n					Към носията Има възможност да се добавят различни аксесоари - цветя, накити и др.\r\n                    ", "Носия 01", 73, 2, "-S-M-" });

            migrationBuilder.InsertData(
                table: "OutfitsForCarts",
                columns: new[] { "Id", "CartId", "FromDate", "IsActive", "OutfitId", "ToDate" },
                values: new object[,]
                {
                    { 174, 2, new DateTime(2023, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, new DateTime(2023, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 175, 2, new DateTime(2023, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, new DateTime(2023, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "IsDefault", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[] { 190, null, true, 3, null, null, "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "IsDefault", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[] { 191, null, true, 4, null, null, "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "OutfitRenterDates",
                keyColumn: "OrderId",
                keyValue: new Guid("13bf08ed-1805-4eb6-9b60-cc56834e567c"));

            migrationBuilder.DeleteData(
                table: "OutfitRenterDates",
                keyColumn: "OrderId",
                keyValue: new Guid("c17279d7-83ce-4acb-9890-f6f8d47949f5"));

            migrationBuilder.DeleteData(
                table: "OutfitRenterDates",
                keyColumn: "OrderId",
                keyValue: new Guid("dcf5cfb0-1fed-4808-8421-cddfca785e09"));

            migrationBuilder.DeleteData(
                table: "OutfitRenterDates",
                keyColumn: "OrderId",
                keyValue: new Guid("f5e540b6-949c-4dc4-9fd3-b775590cc023"));

            migrationBuilder.DeleteData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "OutfitsForCarts",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "OutfitsForCarts",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "OutfitsForCarts",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "OutfitsForCarts",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "OutfitsForCarts",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedOn", "EventId", "IsActive", "IsApproved", "ModifiedContent", "OwnerId" },
                values: new object[,]
                {
                    { 1, "Страхотна локация! :) ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, true, null, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") },
                    { 2, "бля бля бля", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, false, null, new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e") },
                    { 3, "Този фестивал вече е добавен.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, true, null, new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7") }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventEndDate", "EventStartDate" },
                values: new object[] { new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EventId", "IsDefault", "OutfitPartId", "OutfitSetId", "RegionId", "Url" },
                values: new object[,]
                {
                    { 1, null, false, null, 1, null, "Set image" },
                    { 2, null, false, 1, null, null, "Shirt image" },
                    { 3, null, false, 2, null, null, "Vest image" },
                    { 4, 1, false, null, null, null, "Jeravna image" },
                    { 5, 1, false, null, null, null, "Jeravna image 2" }
                });

            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "OutfitSetId" },
                values: new object[] { "Риза: \r\n                            Рамене - 31\r\n                            Гръдна обиколка - 73", 1 });

            migrationBuilder.UpdateData(
                table: "OutfitParts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "OutfitPartType", "OutfitSetId", "OwnerId" },
                values: new object[] { "Елек: \r\n                            Рамене - 32\r\n                            Дължина - 34\r\n                            Отвор за ръкав - 27", 4, 1, new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e") });

            migrationBuilder.UpdateData(
                table: "OutfitSets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Description", "Name", "RegionId", "RenterType", "Size" },
                values: new object[] { "Кафяв", "Родопска детска носия за момче.\r\n                    Състои се от:\r\n                    - Риза\r\n                    - Елек\r\n                    - Панталон\r\n                    - Пояс\r\n\r\n                    Подходяща за момче между 7 и 9 години.\r\n                    ", "Носия 17", 1, 3, "-XS-S-" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Обхваща Родопа планина.");
        }
    }
}
