using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Data.Models;

namespace NosiYa.Services.Tests
{
    using Microsoft.Extensions.Logging;
    using NosiYa.Data.Models.Enums;
    using NosiYa.Data.Models.Outfit;
    using NosiYa.Services.Data;
    using static Common.SeedingConstants;

    public static class DbSeedData
    {
        public static ICollection<OutfitRenterDate> OutfitRenterDates = new HashSet<OutfitRenterDate>();
        public static ICollection<ApplicationUser> Users = new HashSet<ApplicationUser>();
        public static ICollection<OutfitForCart> OutfitsForCarts = new HashSet<OutfitForCart>();
        public static ICollection<Event> Events = new HashSet<Event>();
        public static ICollection<OutfitSet> OutfitSets = new HashSet<OutfitSet>();
        public static ICollection<OutfitPart> OutfitParts = new HashSet<OutfitPart>();
        public static ICollection<Comment> Comments = new HashSet<Comment>();
        public static ICollection<Region> Regions = new HashSet<Region>();


        public static void SeedDatabase(NosiYaDbContext dbContext)
        {

            //Users ---------------------------------------------------------------------------------------------------------

            var admin = new ApplicationUser
            {
                Id = Guid.Parse(AdminId),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = DevAdminEmail,
                NormalizedEmail = "ADMIN@NOSIYA.COM",
                EmailConfirmed = true,
                PasswordHash = "random_password_hash",
                SecurityStamp = "random_security_stamp",
                ConcurrencyStamp = "random_concurrency_stamp",
                PhoneNumber = "1234567890",
            };
            var user = new ApplicationUser
            {
                Id = Guid.Parse(UserId),
                UserName = "user",
                NormalizedUserName = "USER",
                Email = DevUserEmail,
                NormalizedEmail = "USER@NOSIYA.COM",
                EmailConfirmed = true,
                PasswordHash = "random_password_hash",
                SecurityStamp = "random_security_stamp",
                ConcurrencyStamp = "random_concurrency_stamp",
                PhoneNumber = "1234567220",
            };

            Users.Add(admin);
            Users.Add(user);
            dbContext.Users.AddRange(Users);

            //Outfit Renter dates /Orders or Rents/-----------------------------------------------------------------------------

            OutfitRenterDates = new HashSet<OutfitRenterDate>()
            {
                new OutfitRenterDate
                {
                    OrderId = Guid.NewGuid(),
                    OutfitId = 2,
                    RenterId = Guid.Parse(AdminId),
                    DateRangeStart = DateTime.Parse("27-09-2023"),
                    DateRangeEnd = DateTime.Parse("28-09-2023"),
                    IsActive = false,
                    IsApproved = false
                },
                new OutfitRenterDate
                {
                    OrderId = Guid.NewGuid(),
                    OutfitId = 1,
                    RenterId = Guid.Parse(AdminId),
                    DateRangeStart = DateTime.Parse("27-09-2023"),
                    DateRangeEnd = DateTime.Parse("27-09-2023"),
                    IsActive = true,
                    IsApproved = false
                },
                new OutfitRenterDate
                {
                    OrderId = Guid.NewGuid(),
                    OutfitId = 1,
                    RenterId = Guid.Parse(AdminId),
                    DateRangeStart = DateTime.Parse("30-08-2023"),
                    DateRangeEnd = DateTime.Parse("02-09-2023"),
                    IsActive = true,
                    IsApproved = true
                },
                new OutfitRenterDate
                {
                    OrderId = Guid.NewGuid(),
                    OutfitId = 2,
                    RenterId = Guid.Parse(AdminId),
                    DateRangeStart = DateTime.Parse("01-08-2023"),
                    DateRangeEnd = DateTime.Parse("04-08-2023"),
                    IsActive = true,
                    IsApproved = true
                },
                new OutfitRenterDate
                {
                    OrderId = Guid.NewGuid(),
                    OutfitId = 2,
                    RenterId = Guid.Parse(AdminId),
                    DateRangeStart = DateTime.Parse("05-08-2023"),
                    DateRangeEnd = DateTime.Parse("07-08-2023"),
                    IsActive = true,
                    IsApproved = false
                }
            };
            dbContext.OutfitRenterDates.AddRange(OutfitRenterDates);

            //OutfitSets    ------------------------------------------------------------------------------------------------------
            OutfitSets = new HashSet<OutfitSet>
            {
                new OutfitSet
                {
                    Id = 1,
                    Name = "Носия 01",
                    Description =
                        @"Тракийска женска носия.
            Състои се от:
            - Риза
            - Сукман
            - Престилка

            Ръчно шити орнаменти. 
            Към носията Има възможност да се добавят различни аксесоари - цветя, накити и др.
            ",
                    RegionId = 73,
                    PricePerDay = 25,
                    Color = "Червен",
                    RenterType = (RenterType)2,
                    IsAvailable = true,
                    Size = "-S-M-"
                },
                new OutfitSet
                {
                    Id = 2,
                    Name = "Носия 17",
                    Description =
                        @"Родопска детска носия за момче.
            Състои се от:
            - Риза
            - Елек
            - Панталон
            - Пояс

            Подходяща за момче между 7 и 9 години.
            ",
                    RegionId = 1,
                    PricePerDay = 30,
                    Color = "Кафяв",
                    RenterType = (RenterType)3,
                    IsAvailable = true,
                    Size = "-XS-S-"
                },
                new OutfitSet
                {
                    Id = InMaintenanceSetContainerId, // 3
                    Name = "In maintenance",
                    Description =
                        @"Тази носия е неактивна и служи като контейнер за елементите/частите, когато са временно неактивни.
            Тази носия не трябва да бъде активирана!
            ",
                    RegionId = 1,
                    PricePerDay = 0,
                    Color = "",
                    RenterType = (RenterType)1,
                    IsAvailable = false,
                    Size = "-S-"
                }
            };
            dbContext.OutfitSets.AddRange(OutfitSets);


            //OutfitParts    ------------------------------------------------------------------------------------------------------

            OutfitParts = new HashSet<OutfitPart>
{
    new OutfitPart
    {
        Id = 1,
        Name = "Детска Риза",
        Description = @"Риза: 
                        Рамене - 31см
                        Гръдна обиколка - 73см,
                        Дължина - 41см,
                        Обиколка на ръкав - 30см,
                        Дължина на ръкав от рамото - 42см",
        Color = "бял",
        RenterType = (RenterType)3,
        OutfitPartType = (OutfitPartType)4,
        Size = "-XS-",
        OutfitSetId = 2,
        OwnerId = Guid.Parse(AdminId)
    },
    new OutfitPart
    {
        Id = 2,
        Name = "Детски Елек",
        Description = @"Елек: 
                        Рамене - 32см
                        Дължина - 34см
                        Отвор за ръкав - 27см",
        Color = "кафяв",
        RenterType = (RenterType)3,
        OutfitPartType = (OutfitPartType)5,
        Size = "-XS-",
        OutfitSetId = 2,
        OwnerId = Guid.Parse(AdminId)
    },
    new OutfitPart
    {
        Id = 3,
        Name = "Панталон",
        Description = @"Талия - регулира се с връзки,
                        Дължина - 71см",
        Color = "кафяв",
        RenterType = (RenterType)3,
        OutfitPartType = (OutfitPartType)8,
        Size = "-XS-",
        OutfitSetId = 2,
        OwnerId = Guid.Parse(UserId)
    },
    new OutfitPart
    {
        Id = 4,
        Name = "Пояс",
        Description = @"Ширина - 27см, Дължина - 146см",
        Color = "червен",
        RenterType = (RenterType)3,
        OutfitPartType = (OutfitPartType)1,
        Size = "-XS-",
        OutfitSetId = 2,
        OwnerId = Guid.Parse(AdminId)
    },
    new OutfitPart
    {
        Id = 5,
        Name = "Престилка",
        Description = @"Ширина - 46см, Дължина - 57см",
        Color = "черен",
        RenterType = (RenterType)2,
        OutfitPartType = (OutfitPartType)2,
        Size = "-S-М-",
        OutfitSetId = 1,
        OwnerId = Guid.Parse(AdminId)
    },
    new OutfitPart
    {
        Id = 6,
        Name = "Риза",
        Description = @"Рамене - 41см, Гръдна обиколка - 106см, Дължина - 56см, Ръкав обиколка - 36см, Ръкав дължина от рамото - 49",
        Color = "бял",
        RenterType = (RenterType)2,
        OutfitPartType = (OutfitPartType)4,
        Size = "-S-М-",
        OutfitSetId = 1,
        OwnerId = Guid.Parse(AdminId)
    },
    new OutfitPart
    {
        Id = 7,
        Name = "Сукман",
        Description = @"Рамене - 40см, Гръдна обиколка - 90см, Талия - 74см, Дължина - 103см",
        Color = "червен",
        RenterType = (RenterType)2,
        OutfitPartType = (OutfitPartType)6,
        Size = "-S-М-",
        OutfitSetId = 1,
        OwnerId = Guid.Parse(AdminId)
    }
};
            dbContext.OutfitParts.AddRange(OutfitParts);


            //OutfitForCarts  - Cart items    ---------------------------------------------------------------------------------------


            OutfitsForCarts = new HashSet<OutfitForCart>()
            {

                new OutfitForCart
            {
                Id = 222,
                OutfitId = 2,
                FromDate = DateTime.Parse("14-08-2023"),
                ToDate = DateTime.Parse("17-08-2023"),
                CartId = user.Cart.Id,
                IsActive = false
            },
                new OutfitForCart
                {
                    Id = 171,
                    OutfitId = 1,
                    FromDate = DateTime.Parse("17-08-2023"),
                    ToDate = DateTime.Parse("19-08-2023"),
                    CartId = 1
                },
                new OutfitForCart
                {
                    Id = 172,
                    OutfitId = 1,
                    FromDate = DateTime.Parse("22-08-2023"),
                    ToDate = DateTime.Parse("22-08-2023"),
                    CartId = 1
                },
                new OutfitForCart
                {
                    Id = 173,
                    OutfitId = 1,
                    FromDate = DateTime.Parse("22-08-2023"),
                    ToDate = DateTime.Parse("24-08-2023"),
                    CartId = 2
                },
                new OutfitForCart
                {
                    Id = 174,
                    OutfitId = 2,
                    FromDate = DateTime.Parse("14-08-2023"),
                    ToDate = DateTime.Parse("17-08-2023"),
                    CartId = 2
                },
                new OutfitForCart
                {
                    Id = 175,
                    OutfitId = 2,
                    FromDate = DateTime.Parse("22-08-2023"),
                    ToDate = DateTime.Parse("24-08-2023"),
                    CartId = 2
                },
                new OutfitForCart
                {
                    Id = 333,
                    OutfitId = 2,
                    FromDate = DateTime.Parse("14-08-2023"),
                    ToDate = DateTime.Parse("17-08-2023"),
                    CartId = admin.Cart.Id,
                    IsActive = true
                }
            };
            dbContext.OutfitsForCarts.AddRange(OutfitsForCarts);


            //Event: ---------------------------------------------------------------------------------------

            Events = new HashSet<Event>()
            {
                new Event
                {
                    Id = 1,
                    Name = "Фестивал на фолклорната носия - Жеравна",
                    Description = "Фестивалът е създаден през 2008 година по идея на Христо Димитров – продуцент, хореограф и режисьор на Национален фолклорен ансамбъл „Българе”. Атрактивната им сватба с фолклорната певица Албена Вескова през 2005 г. в местността \"Костина\" край с. Рибарица по старинен български обичай, на която младоженците и присъстващите 400 гости са с български народни носии, има широк позитивен отзвук. Това мотивира създателя на „Българе” с помощта на неговите партньори Ян и Елена Андерсон, на тогавашния кмет на Жеравна Лъчезар Германов, бизнесмените Георги Манев и Калин Григоров, както и на други съмишленици, да организира много по-мащабно събитие, което да дава възможност на всеки желаещ поне за три дни в годината да облече българска носия и се откъсне от цивилизацията, като направи скок век и половина назад във времето.",
                    Location = "42.8336195015454, 26.45904413725397",
                    IsApproved = true,
                    OwnerId = Guid.Parse(AdminId),
                    EventStartDate = DateTime.Parse("07-10-2023"),
                    EventEndDate = DateTime.Parse("12-10-2023"),
                },
                new Event
                {
                    Id = 2,
                    Name = "Фестивал на  Жеравна",
                    Description = "Това мотивира създателя на „Българе” с помощта на неговите партньори Ян и Елена Андерсон, на тогавашния кмет на Жеравна Лъчезар Германов, бизнесмените Георги Манев и Калин Григоров, както и на други съмишленици, да организира много по-мащабно събитие, което да дава възможност на всеки желаещ поне за три дни в годината да облече българска носия и се откъсне от цивилизацията, като направи скок век и половина назад във времето.",
                    Location = "42.8336195015454, 26.45904413725397",
                    IsApproved = false,
                    OwnerId = Guid.Parse(UserId),
                    EventStartDate = DateTime.Parse("07-07-2023"),
                    EventEndDate = DateTime.Parse("07-07-2023"),
                },

                new Event
                {
                Id = 73,
                Name = "Международен фолклорен фестивал „Витоша",
                Description = "Местоположение на сцените\r\n\r\nПешеходна зона пред Национален исторически музей, Бояна – 10.07, 11.07, 12.07, 13.07, 14.07 и15.07\r\nКино Кабана – 11.07\r\nс. Владая, НЧ „Светлина – 1906“ – 12.07\r\nкв. Княжево, пл. „Сред село“ – 13.07\r\nкв. Симеоново, НЧ „Отец Паисий“ – 14.07\r\nкв. Драгалевци, ул. „Карнобатски проход“ – 13.07\r\nкв. Бояна, парк „Воденичница“ – 14.07\r\nс. Мърчаево, 152 ОУ – 12.07\r\nМега Мол София, бул. „Царица Йоанна“ № 15 – 15.07",
                Location = "42.652049852021584, 23.26517291652124",
                IsApproved = true,
                OwnerId = Guid.Parse(UserId),
                EventStartDate = DateTime.Parse("27-09-2023"),
                EventEndDate = DateTime.Parse("04-10-2023"),
                },
                new Event
                {
                    Id = 77,
                    Name = "Test Event",
                    Description = "Test Event description",
                    Location = "Test event location",
                    IsApproved = true,
                    IsActive = true,
                    OwnerId = admin.Id,
                    Owner = admin,
                    EventStartDate = DateTime.UtcNow,
                    EventEndDate = DateTime.UtcNow.AddDays(3),
                }
            };

            dbContext.Events.AddRange(Events);

            // Comments  ---------------------------------------------------------------------------------------

            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = 71,
                    Content = "Здравейте, Тази година входа за паркинга ще е зад сцената! ",
                    OwnerId = Guid.Parse(AdminId),
                    EventId = 1,
                    IsApproved = true
                },
                new Comment
                {
                    Id = 72,
                    Content = "Някой знае ли дали може да се плати вход само за първия ден?",
                    OwnerId = Guid.Parse(UserId),
                    EventId = 1,
                    IsApproved = true
                },
                new Comment
                {
                    Id = 73,
                    Content = "Миналата година можеше. Цената беше 10лв.",
                    OwnerId = Guid.Parse(AdminId),
                    EventId = 1,
                    IsApproved = true
                },
                new Comment
                {
                    Id = 74,
                    Content = "Този фестивал вече е добавен. Можете да премахнете това събитие.",
                    OwnerId = Guid.Parse(AdminId),
                    EventId = 2,
                    IsApproved = true
                },
                new Comment
                {
                    Id = 75,
                    Content = "Международен фолклорен фестивал „Витоша“ е неделима част от Културния календар на София. Провеждането му е уникална възможност за българската публика да се запознае с музикалната и танцова традиция на държави от цял свят. Това е шанс младата аудитория да види най-атрактивното лице на фолклора.",
                    OwnerId = Guid.Parse(AdminId),
                    EventId = 73,
                    IsApproved = true
                },

                new Comment
                {
                    Id = 76,
                    Content = "Международен фолклорен фестивал „Витоша“ е неделима част от Културния календар на София. Провеждането му е уникална възможност за българската публика да се запознае с музикалната и танцова традиция на държави от цял свят. Това е шанс младата аудитория да види най-атрактивното лице на фолклора.",
                    OwnerId = Guid.Parse(AdminId),
                    EventId = 73,
                    IsApproved = false,
                    IsActive = false
                },
                new Comment
                {
                    Id = 77,
                    Content = "Visible Comment 1",
                    OwnerId = admin.Id,
                    EventId = 77,
                    IsApproved = true,
                    IsActive = true,
                    CreatedOn = DateTime.UtcNow
                },
                new Comment
                {
                    Id = 78,
                    Content = "Visible Comment 2",
                    OwnerId = admin.Id,
                    EventId = 77,
                    IsApproved = false, // Visible due to user ownership
                    IsActive = true,
                    ModifiedContent = "Modified Content",
                    CreatedOn = DateTime.UtcNow
                },
                new Comment
                {
                    Id = 79,
                    Content = "Visible Comment 3",
                    OwnerId = Guid.NewGuid(),
                    EventId = 77,
                    IsApproved = true,
                    IsActive = true,
                    CreatedOn = DateTime.UtcNow
                }
            };

            dbContext.Comments.AddRange(Comments);

            //Regions  ---------------------------------------------------------------------------------------

            Regions = new HashSet<Region>
{
    new Region
    {
        Id = 1,
        Name = "Родопски Регион",
        Description = "Родопската фолклорна област преди е била включена като район от тракийската област. Обособяването ѝ като самостоятелна се е наложило от по-съществените различия между двете области.\r\n\r\nРодопските танци са бавни и умерени с малко разнообразие на движенията и сравнителна простота. Играят се най-често на песен, като характерно тук е, че мъжете също пеят.\r\n\r\nХора̀та се играят в полукръг или кръг и най-често са само мъжки или само женски. Срещат се и разделно-смесени хора, но при тях мъжете и жените не се нареждат един до друг мъж-жена, а в началото на хорото се хващат само мъжете, а след тях – жените.\r\n\r\nМъжете се залавят най-често за длани, което е много характерно за Родопите. В другите области този захват е много рядко срещан. Мъжете играят с широки стъпки, клякат и коленичат бавно и тромаво."
    },
    new Region
    {
        Id = 72,
        Name = "Шопски Регион",
        Description = "Шопската фолклорна област има няколко района – софийски, граовски, кюстендилски, самоковски, ихтимански и годечки, в които има известни различия, въпреки общото в стила.\r\n\r\nТанцуването предизвиква силни преживявания в емоционалната душа на шопите, което се изразява в мощни провиквания, ръмжене и свиркане. Добре известно е движението „натрисане” – силно раздрусване на раменете, предизвикано от динамичните, твърди и отсечени движения в краката, както и от постановката на тялото и хвата за пояс. Високото повдигане на крака е проява на сила и мъжественост. Характерен е подчертаният стремеж за откъсване от земята."
    },
    new Region
    {
        Id = 73,
        Name = "Тракийски регион",
        Description = "Обхваща Родопа планина. Тракийската фолклорна област обикновено се разделя на три района – западнотракийски, източнотракийски и странджански.\r\n\r\nМного от странджанските танци са във връзка с някакъв обред. Най-типични са нестинарските танци, великденските ръченици и кукерските игри. Странджанците играят с голяма съсредоточеност и сериозност. Мъжете респектират със сложни пляскания, а ръцете им излъчват сила и достойнство.\r\n\r\nАкцентът в тракийските танци е движението, насочено надолу – олицетворение на почитта към земята. В някои танци ясно се виждат заемки от битови трудови процеси като месене на хляб и точене на тесто. Тракиецът слива празника и делника в танца си, показвайки, че всеки ден е своеобразен празник.\r\n\r\n"
    }
};
            dbContext.Regions.AddRange(Regions);


            //Images ---------------------------------------------------------------------------------------

            //// Image for Outfitset 1 
            var outfitSet1Images = new List<Image>
            {
                new Image
                {
                    Id = 171,
                    Url = "/images/common/nosiq1/3245e510-10ee-4ac5-a181-3141cbef91a7.jpg",
                    OutfitSetId = 1,
                    IsDefault = true
                },
                new Image
                {
                    Id = 172,
                    Url = "/images/common/nosiq1/da5b3ea8-df94-4602-a255-251ac228396a.jpg",
                    OutfitSetId = 1
                },
                new Image
                {
                    Id = 173,
                    Url = "/images/common/nosiq1/f24a886e-d320-4f8d-afe3-2156eca67d0c.jpg",
                    OutfitSetId = 1
                }
            };
            dbContext.Images.AddRange(outfitSet1Images);

            //// Image for Outfitset 2 
            var outfitSet2Images = new List<Image>
            {
                new Image
                {
                    Id = 174,
                    Url = "/images/common/nosiq17/054848d5-9b31-4b18-a1a2-bc9f91bac96d.jpg",
                    OutfitSetId = 2,
                    IsDefault = true
                },
                new Image
                {
                    Id = 175,
                    Url = "/images/common/nosiq17/82c31014-e3d5-42b6-b7ad-0bb533e401ef.jpg",
                    OutfitSetId = 2
                },
                new Image
                {
                    Id = 176,
                    Url = "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg",
                    OutfitSetId = 2
                }
            };
            dbContext.Images.AddRange(outfitSet2Images);


            //// Event 1 
            var event1Images = new List<Image>
{
    new Image
    {
        Id = 177,
        Url = "/images/event/02b1f635-597e-421c-b7c0-25542c6bf6fe.jpg",
        EventId = 1
    },
    new Image
    {
        Id = 178,
        Url = "/images/event/92c33c52-81c0-4234-9540-d110fe071f97.jpeg",
        EventId = 1
    }
};
            dbContext.Images.AddRange(event1Images);


            //// Event 2 
            var event2Images = new List<Image>
{
    new Image
    {
        Id = 179,
        Url = "/images/event/92c33c52-81c0-4234-9540-d110fe071f97.jpeg",
        EventId = 2,
        IsDefault = true
    }
};
            dbContext.Images.AddRange(event2Images);

            //// Event 3 
            var event3Images = new List<Image>
{
    new Image
    {
        Id = 180,
        Url = "/images/event/f74ddf23-68d9-4b58-a982-18f3cba1a1f3.jpg",
        EventId = 73,
        IsDefault = true
    },
    new Image
    {
        Id = 181,
        Url = "/images/event/59b6dce3-8fe8-42a1-8998-7967e282c7b8.jpg",
        EventId = 73
    },
    new Image
    {
        Id = 182,
        Url = "/images/event/30fde865-6e69-4408-b490-ff5b0c49e70e.jpg",
        EventId = 73
    }
};
            dbContext.Images.AddRange(event3Images);

            //// Region 1 
            var region1Images = new List<Image>
{
    new Image
    {
        Id = 183,
        Url = "/images/region/12f52c5a-24ac-4b8b-99fb-34e245967555.jpg",
        RegionId = 1,
        IsDefault = true
    },
    new Image
    {
        Id = 184,
        Url = "/images/region/85deec0a-67cc-47ab-997b-a17ad7546041.jpg",
        RegionId = 1
    }
};
            dbContext.Images.AddRange(region1Images);

            //// Region 2 
            var region2Images = new List<Image>
{
    new Image
    {
        Id = 185,
        Url = "/images/region/04d45bb6-1100-453f-9377-862a4c10dda6.jpg",
        RegionId = 72,
        IsDefault = true
    },
    new Image
    {
        Id = 186,
        Url = "/images/region/1ccc5fc0-15c9-4fa9-bad8-a3f23ca301c1.jpg",
        RegionId = 72
    }
};
            dbContext.Images.AddRange(region2Images);

            //// Region 3 
            var region3Images = new List<Image>
{
    new Image
    {
        Id = 187,
        Url = "/images/region/ce68a779-2c71-488b-8cde-25b01df35ec5.jpg",
        RegionId = 73,
        IsDefault = true
    }
};
            dbContext.Images.AddRange(region3Images);

            // OutfitParts with demo pictures 

            var images = new HashSet<Image>();

            for (int i = 0; i < 7; i++)
            {
                var image = new Image
                {
                    Id = 188 + i,
                    Url = "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg",
                    OutfitPartId = 1 + i,
                    IsDefault = true
                };
                images.Add(image);
            }
            dbContext.Images.AddRange(images);


            //dbContext.Carts.AddRange(Carts);
            dbContext.SaveChanges();

            var userw = dbContext.Users.Find(Guid.Parse(AdminId));
            var usasder = dbContext.Events.Find(1);
            var role = dbContext.OutfitSets.Find(1);
            var rodle = dbContext.UserRoles.ToArray();
        }
    }
}
