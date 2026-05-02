using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aromo.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Catalogues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogues_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perfumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Original = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perfumes_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perfumes_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perfumes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CataloguePerfume",
                columns: table => new
                {
                    CataloguesId = table.Column<int>(type: "int", nullable: false),
                    PerfumesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CataloguePerfume", x => new { x.CataloguesId, x.PerfumesId });
                    table.ForeignKey(
                        name: "FK_CataloguePerfume_Catalogues_CataloguesId",
                        column: x => x.CataloguesId,
                        principalTable: "Catalogues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CataloguePerfume_Perfumes_PerfumesId",
                        column: x => x.PerfumesId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfumeBaseScents",
                columns: table => new
                {
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    ScentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumeBaseScents", x => new { x.PerfumeId, x.ScentId });
                    table.ForeignKey(
                        name: "FK_PerfumeBaseScents_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfumeBaseScents_Scents_ScentId",
                        column: x => x.ScentId,
                        principalTable: "Scents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfumeHeartScents",
                columns: table => new
                {
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    ScentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumeHeartScents", x => new { x.PerfumeId, x.ScentId });
                    table.ForeignKey(
                        name: "FK_PerfumeHeartScents_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfumeHeartScents_Scents_ScentId",
                        column: x => x.ScentId,
                        principalTable: "Scents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfumeTopScents",
                columns: table => new
                {
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    ScentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumeTopScents", x => new { x.PerfumeId, x.ScentId });
                    table.ForeignKey(
                        name: "FK_PerfumeTopScents_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfumeTopScents_Scents_ScentId",
                        column: x => x.ScentId,
                        principalTable: "Scents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Arabica" },
                    { 2, "Aladin" },
                    { 3, "Amouage" },
                    { 4, "Armaf" },
                    { 5, "Azzaro" },
                    { 6, "Bvlgari" },
                    { 7, "Dessert Rose" },
                    { 8, "Habibi Air" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Мъжки" },
                    { 2, "Дамски" },
                    { 3, "Унисекс" }
                });

            migrationBuilder.InsertData(
                table: "Scents",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Мед" },
                    { 2, "Роза" },
                    { 3, "Ванилия" },
                    { 4, "Канела" },
                    { 5, "Кедрово дърво" },
                    { 6, "Морски" },
                    { 7, "Портокал" },
                    { 8, "Жасмин" },
                    { 9, "Ориенталски" },
                    { 10, "Бергамот" },
                    { 11, "Лавандула" },
                    { 12, "Сандалово дърво" },
                    { 13, "Мускус" },
                    { 14, "Розмарин" },
                    { 15, "Боровинка" },
                    { 16, "Мента" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Пролет" },
                    { 2, "Лято" },
                    { 3, "Есен" },
                    { 4, "Зима" },
                    { 12, "Пролет/Лято" },
                    { 13, "Пролет/Есен" },
                    { 14, "Пролет/Зима" },
                    { 23, "Лято/Есен" },
                    { 24, "Лято/Зима" },
                    { 34, "Есен/Зима" },
                    { 123, "Пролет/Лято/Есен" },
                    { 124, "Пролет/Лято/Зима" },
                    { 134, "Пролет/Есен/Зима" },
                    { 1234, "Целогодишен" }
                });

            migrationBuilder.InsertData(
                table: "Perfumes",
                columns: new[] { "Id", "BrandId", "GenderId", "Name", "Original", "Price", "SeasonId", "Volume" },
                values: new object[,]
                {
                    { 1, 1, 3, "Mystic Oud", "Versace Inspiration", 199.99m, 4, 100 },
                    { 2, 7, 2, "Desert Rose", "Dior Salage", 149.99m, 2, 50 },
                    { 3, 4, 1, "Ocean Breeze", "Davidoff Cool Water", 89.99m, 14, 75 },
                    { 4, 3, 3, "Golden Sandal", "Guerlain Santal Royal", 249.99m, 24, 100 },
                    { 5, 5, 1, "Amber Night", "Yves Saint Laurent La Nuit de L'Homme", 129.99m, 13, 100 },
                    { 6, 2, 2, "Velvet Jasmine", "Chanel Chance Eau Tendre", 109.99m, 1234, 50 },
                    { 7, 6, 3, "Citrus Bloom", "Dolce & Gabbana Light Blue", 99.99m, 12, 75 },
                    { 8, 8, 2, "Midnight Vanilla", "Tom Ford Black Orchid", 139.99m, 4, 100 },
                    { 9, 3, 1, "Forest Whisper", "Hermes Terre d'Hermes", 179.99m, 13, 100 },
                    { 10, 6, 3, "White Musk", "Narciso Rodriguez For Her", 119.99m, 134, 50 },
                    { 11, 1, 1, "Oriental Spice", "Spicebomb by Viktor & Rolf", 189.99m, 124, 100 },
                    { 12, 7, 2, "Rosewood Elegance", "Chloé Nomade", 159.99m, 23, 75 },
                    { 13, 4, 1, "Aqua Mint", "Acqua di Gio by Giorgio Armani", 79.99m, 24, 50 },
                    { 14, 3, 3, "Royal Incense", "Amouage Interlude", 269.99m, 4, 100 },
                    { 15, 6, 2, "Sunny Neroli", "Neroli Portofino by Tom Ford", 99.99m, 2, 75 },
                    { 16, 5, 1, "Dark Leather", "John Varvatos Dark Rebel", 149.99m, 1, 100 },
                    { 17, 2, 3, "Soft Lavender", "Lavender Eau by Jo Malone", 89.99m, 14, 50 },
                    { 18, 8, 3, "Habibi Gold", "Habibi Classic", 219.99m, 13, 100 }
                });

            migrationBuilder.InsertData(
                table: "PerfumeBaseScents",
                columns: new[] { "PerfumeId", "ScentId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 5 },
                    { 1, 12 },
                    { 1, 13 },
                    { 2, 3 },
                    { 2, 12 },
                    { 2, 13 },
                    { 3, 5 },
                    { 3, 12 },
                    { 3, 13 },
                    { 4, 1 },
                    { 4, 5 },
                    { 4, 12 },
                    { 4, 13 },
                    { 5, 1 },
                    { 5, 12 },
                    { 5, 13 },
                    { 6, 5 },
                    { 6, 12 },
                    { 6, 13 },
                    { 7, 1 },
                    { 7, 12 },
                    { 7, 13 },
                    { 8, 5 },
                    { 8, 12 },
                    { 8, 13 },
                    { 9, 1 },
                    { 9, 5 },
                    { 9, 12 },
                    { 10, 5 },
                    { 10, 12 },
                    { 10, 13 },
                    { 11, 1 },
                    { 11, 12 },
                    { 11, 13 },
                    { 12, 1 },
                    { 12, 5 },
                    { 12, 12 },
                    { 13, 5 },
                    { 13, 12 },
                    { 13, 13 },
                    { 14, 1 },
                    { 14, 5 },
                    { 14, 12 },
                    { 15, 1 },
                    { 15, 5 },
                    { 15, 12 },
                    { 15, 13 },
                    { 16, 5 },
                    { 16, 12 },
                    { 16, 13 },
                    { 17, 1 },
                    { 17, 5 },
                    { 17, 12 },
                    { 18, 1 },
                    { 18, 12 },
                    { 18, 13 }
                });

            migrationBuilder.InsertData(
                table: "PerfumeHeartScents",
                columns: new[] { "PerfumeId", "ScentId" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 1, 8 },
                    { 1, 9 },
                    { 2, 3 },
                    { 2, 8 },
                    { 2, 15 },
                    { 3, 4 },
                    { 3, 11 },
                    { 4, 2 },
                    { 4, 3 },
                    { 4, 9 },
                    { 5, 8 },
                    { 5, 15 },
                    { 6, 3 },
                    { 6, 4 },
                    { 6, 9 },
                    { 7, 2 },
                    { 7, 8 },
                    { 8, 8 },
                    { 8, 9 },
                    { 8, 15 },
                    { 9, 3 },
                    { 9, 15 },
                    { 10, 3 },
                    { 10, 4 },
                    { 11, 8 },
                    { 11, 9 },
                    { 12, 4 },
                    { 12, 15 },
                    { 13, 2 },
                    { 13, 9 },
                    { 14, 2 },
                    { 14, 8 },
                    { 15, 3 },
                    { 15, 15 },
                    { 16, 3 },
                    { 16, 4 },
                    { 17, 2 },
                    { 17, 8 },
                    { 18, 9 },
                    { 18, 15 }
                });

            migrationBuilder.InsertData(
                table: "PerfumeTopScents",
                columns: new[] { "PerfumeId", "ScentId" },
                values: new object[,]
                {
                    { 1, 7 },
                    { 1, 10 },
                    { 1, 11 },
                    { 2, 2 },
                    { 2, 10 },
                    { 2, 14 },
                    { 3, 6 },
                    { 3, 14 },
                    { 3, 16 },
                    { 4, 10 },
                    { 4, 11 },
                    { 5, 7 },
                    { 5, 14 },
                    { 5, 16 },
                    { 6, 10 },
                    { 7, 6 },
                    { 7, 7 },
                    { 7, 14 },
                    { 8, 7 },
                    { 8, 10 },
                    { 8, 11 },
                    { 9, 6 },
                    { 10, 7 },
                    { 10, 16 },
                    { 11, 6 },
                    { 11, 10 },
                    { 11, 11 },
                    { 12, 11 },
                    { 12, 14 },
                    { 13, 7 },
                    { 13, 10 },
                    { 13, 14 },
                    { 14, 16 },
                    { 15, 6 },
                    { 15, 7 },
                    { 15, 10 },
                    { 15, 11 },
                    { 16, 11 },
                    { 17, 7 },
                    { 17, 14 },
                    { 18, 6 },
                    { 18, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CataloguePerfume_PerfumesId",
                table: "CataloguePerfume",
                column: "PerfumesId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogues_UserId",
                table: "Catalogues",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumeBaseScents_ScentId",
                table: "PerfumeBaseScents",
                column: "ScentId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumeHeartScents_ScentId",
                table: "PerfumeHeartScents",
                column: "ScentId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfumes_BrandId",
                table: "Perfumes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfumes_GenderId",
                table: "Perfumes",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfumes_Name_BrandId_Volume",
                table: "Perfumes",
                columns: new[] { "Name", "BrandId", "Volume" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Perfumes_SeasonId",
                table: "Perfumes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumeTopScents_ScentId",
                table: "PerfumeTopScents",
                column: "ScentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CataloguePerfume");

            migrationBuilder.DropTable(
                name: "PerfumeBaseScents");

            migrationBuilder.DropTable(
                name: "PerfumeHeartScents");

            migrationBuilder.DropTable(
                name: "PerfumeTopScents");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Catalogues");

            migrationBuilder.DropTable(
                name: "Perfumes");

            migrationBuilder.DropTable(
                name: "Scents");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Seasons");
        }
    }
}
