﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NosiYa.Data;

#nullable disable

namespace NosiYa.Data.Migrations
{
    [DbContext(typeof(NosiYaDbContext))]
    [Migration("20230713185006_EventsAndCommentInitialSeed")]
    partial class EventsAndCommentInitialSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NosiYa.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("NosiYa.Data.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Страхотна локация! :) ",
                            EventId = 1,
                            IsApproved = true,
                            OwnerId = new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7")
                        },
                        new
                        {
                            Id = 2,
                            Content = "бля бля бля",
                            EventId = 1,
                            IsApproved = false,
                            OwnerId = new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e")
                        },
                        new
                        {
                            Id = 3,
                            Content = "Този фестивал вече е добавен.",
                            EventId = 2,
                            IsApproved = true,
                            OwnerId = new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7")
                        });
                });

            modelBuilder.Entity("NosiYa.Data.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("EventEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EventStartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Фестивалът е създаден през 2008 година по идея на Христо Димитров – продуцент, хореограф и режисьор на Национален фолклорен ансамбъл „Българе”. Атрактивната им сватба с фолклорната певица Албена Вескова през 2005 г. в местността \"Костина\" край с. Рибарица по старинен български обичай, на която младоженците и присъстващите 400 гости са с български народни носии, има широк позитивен отзвук. Това мотивира създателя на „Българе” с помощта на неговите партньори Ян и Елена Андерсон, на тогавашния кмет на Жеравна Лъчезар Германов, бизнесмените Георги Манев и Калин Григоров, както и на други съмишленици, да организира много по-мащабно събитие, което да дава възможност на всеки желаещ поне за три дни в годината да облече българска носия и се откъсне от цивилизацията, като направи скок век и половина назад във времето.",
                            EventEndDate = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventStartDate = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsApproved = true,
                            Location = "42.8336195015454, 26.45904413725397",
                            Name = "Фестивал на фолклорната носия - Жеравна",
                            OwnerId = new Guid("7c34fb52-0fdb-4cd7-027f-08db822aa1b7")
                        },
                        new
                        {
                            Id = 2,
                            Description = "Това мотивира създателя на „Българе” с помощта на неговите партньори Ян и Елена Андерсон, на тогавашния кмет на Жеравна Лъчезар Германов, бизнесмените Георги Манев и Калин Григоров, както и на други съмишленици, да организира много по-мащабно събитие, което да дава възможност на всеки желаещ поне за три дни в годината да облече българска носия и се откъсне от цивилизацията, като направи скок век и половина назад във времето.",
                            EventEndDate = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventStartDate = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsApproved = false,
                            Location = "42.8336195015454, 26.45904413725397",
                            Name = "Фестивал на  Жеравна",
                            OwnerId = new Guid("2f29d591-89ef-45b2-89a9-08db83ceb60e")
                        });
                });

            modelBuilder.Entity("NosiYa.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("OutfitPartId")
                        .HasColumnType("int");

                    b.Property<int?>("OutfitSetId")
                        .HasColumnType("int");

                    b.Property<int?>("RegionId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("OutfitPartId");

                    b.HasIndex("OutfitSetId");

                    b.HasIndex("RegionId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OutfitSetId = 1,
                            Url = "Set image"
                        },
                        new
                        {
                            Id = 2,
                            OutfitPartId = 1,
                            Url = "Shirt image"
                        },
                        new
                        {
                            Id = 3,
                            OutfitPartId = 2,
                            Url = "Vest image"
                        },
                        new
                        {
                            Id = 5,
                            EventId = 1,
                            Url = "Jeravna image 2"
                        });
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitForCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OutfitId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("OutfitId");

                    b.ToTable("OutfitsForCarts");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitPart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OutfitSetId")
                        .HasColumnType("int");

                    b.Property<int>("OutfitType")
                        .HasColumnType("int");

                    b.Property<int>("RenterType")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("OutfitSetId");

                    b.ToTable("OutfitParts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "бял",
                            Description = "Риза: \r\n                            Рамене - 31\r\n                            Гръдна обиколка - 73",
                            Name = "Детска Риза",
                            OutfitSetId = 1,
                            OutfitType = 4,
                            RenterType = 3,
                            Size = "XS"
                        },
                        new
                        {
                            Id = 2,
                            Color = "кафяв",
                            Description = "Елек: \r\n                            Рамене - 32\r\n                            Дължина - 34\r\n                            Отвор за ръкав - 27",
                            Name = "Детски Елек",
                            OutfitSetId = 1,
                            OutfitType = 4,
                            RenterType = 3,
                            Size = "XS"
                        });
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitRenterDate", b =>
                {
                    b.Property<int>("OutfitId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("RenterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OutfitId", "Date");

                    b.HasIndex("RenterId");

                    b.ToTable("OutfitRenterDates");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("PricePerDay")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("RegionId")
                        .HasColumnType("int");

                    b.Property<int>("RenterType")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("OutfitSets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "Кафяв",
                            Description = "Родопска детска носия за момче.Риза, елек, панталон и пояс,\r\n                    Състои се от:\r\n                    - Риза\r\n                    - Елек\r\n                    - Панталон\r\n                    - Пояс\r\n\r\n                    Подходяща за момче между 7 и 9 години.\r\n                    ",
                            IsAvailable = true,
                            Name = "Носия 17",
                            PricePerDay = 25m,
                            RegionId = 1,
                            RenterType = 3,
                            Size = "XS"
                        });
                });

            modelBuilder.Entity("NosiYa.Data.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Обхваща Родопа планина.",
                            Name = "Родопски Регион"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("NosiYa.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("NosiYa.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NosiYa.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("NosiYa.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NosiYa.Data.Models.Cart", b =>
                {
                    b.HasOne("NosiYa.Data.Models.ApplicationUser", "Owner")
                        .WithOne("Cart")
                        .HasForeignKey("NosiYa.Data.Models.Cart", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Comment", b =>
                {
                    b.HasOne("NosiYa.Data.Models.Event", "Event")
                        .WithMany("Comments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NosiYa.Data.Models.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Event", b =>
                {
                    b.HasOne("NosiYa.Data.Models.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Image", b =>
                {
                    b.HasOne("NosiYa.Data.Models.Event", "Event")
                        .WithMany("Images")
                        .HasForeignKey("EventId");

                    b.HasOne("NosiYa.Data.Models.Outfit.OutfitPart", "OutfitPart")
                        .WithMany("Images")
                        .HasForeignKey("OutfitPartId");

                    b.HasOne("NosiYa.Data.Models.Outfit.OutfitSet", "OutfitSet")
                        .WithMany("Images")
                        .HasForeignKey("OutfitSetId");

                    b.HasOne("NosiYa.Data.Models.Region", "Region")
                        .WithMany("Images")
                        .HasForeignKey("RegionId");

                    b.Navigation("Event");

                    b.Navigation("OutfitPart");

                    b.Navigation("OutfitSet");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitForCart", b =>
                {
                    b.HasOne("NosiYa.Data.Models.Cart", "Cart")
                        .WithMany("Outfits")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NosiYa.Data.Models.Outfit.OutfitSet", "OutfitSet")
                        .WithMany()
                        .HasForeignKey("OutfitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("OutfitSet");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitPart", b =>
                {
                    b.HasOne("NosiYa.Data.Models.Outfit.OutfitSet", "OutfitSet")
                        .WithMany("OutfitParts")
                        .HasForeignKey("OutfitSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OutfitSet");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitRenterDate", b =>
                {
                    b.HasOne("NosiYa.Data.Models.Outfit.OutfitSet", "Outfit")
                        .WithMany("OutfitRenterDates")
                        .HasForeignKey("OutfitId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NosiYa.Data.Models.ApplicationUser", "Renter")
                        .WithMany("OutfitRenterDates")
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Outfit");

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitSet", b =>
                {
                    b.HasOne("NosiYa.Data.Models.Region", "Region")
                        .WithMany("Outfits")
                        .HasForeignKey("RegionId");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("NosiYa.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("OutfitRenterDates");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Cart", b =>
                {
                    b.Navigation("Outfits");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Event", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitPart", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Outfit.OutfitSet", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("OutfitParts");

                    b.Navigation("OutfitRenterDates");
                });

            modelBuilder.Entity("NosiYa.Data.Models.Region", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Outfits");
                });
#pragma warning restore 612, 618
        }
    }
}
