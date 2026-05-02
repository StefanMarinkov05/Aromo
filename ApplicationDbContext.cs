using Aromo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aromo
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Scent> Scents { get; set; }
        public DbSet<Perfume> Perfumes { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Мъжки" },
                new Gender { Id = 2, Name = "Дамски"},
                new Gender { Id = 3, Name = "Унисекс" }
            );

            modelBuilder.Entity<Season>().HasData(
                new Season { Id = 1, Name = "Пролет" },
                new Season { Id = 2, Name = "Лято" },
                new Season { Id = 3, Name = "Есен" },
                new Season { Id = 4, Name = "Зима" },
                new Season { Id = 12, Name = "Пролет/Лято" },
                new Season { Id = 13, Name = "Пролет/Есен" },
                new Season { Id = 14, Name = "Пролет/Зима" },
                new Season { Id = 23, Name = "Лято/Есен" },
                new Season { Id = 24, Name = "Лято/Зима" },
                new Season { Id = 34, Name = "Есен/Зима" },
                new Season { Id = 123, Name = "Пролет/Лято/Есен" },
                new Season { Id = 124, Name = "Пролет/Лято/Зима" },
                new Season { Id = 134, Name = "Пролет/Есен/Зима" },
                new Season { Id = 1234, Name = "Целогодишен" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Arabica" },
                new Brand { Id = 2, Name = "Aladin" },
                new Brand { Id = 3, Name = "Amouage" },
                new Brand { Id = 4, Name = "Armaf" },
                new Brand { Id = 5, Name = "Azzaro" },
                new Brand { Id = 6, Name = "Bvlgari" },
                new Brand { Id = 7, Name = "Dessert Rose" },
                new Brand { Id = 8, Name = "Habibi Air" }
            );

            modelBuilder.Entity<Scent>().HasData(
                new Scent { Id = 1, Name = "Мед" },
                new Scent { Id = 2, Name = "Роза" },
                new Scent { Id = 3, Name = "Ванилия" },
                new Scent { Id = 4, Name = "Канела" },
                new Scent { Id = 5, Name = "Кедрово дърво" },
                new Scent { Id = 6, Name = "Морски" },
                new Scent { Id = 7, Name = "Портокал" },
                new Scent { Id = 8, Name = "Жасмин" },
                new Scent { Id = 9, Name = "Ориенталски" },
                new Scent { Id = 10, Name = "Бергамот" },
                new Scent { Id = 11, Name = "Лавандула" },
                new Scent { Id = 12, Name = "Сандалово дърво" },
                new Scent { Id = 13, Name = "Мускус" },
                new Scent { Id = 14, Name = "Розмарин" },
                new Scent { Id = 15, Name = "Боровинка" },
                new Scent { Id = 16, Name = "Мента" }
            );

            modelBuilder.Entity<Perfume>()
                .HasIndex(p => new { p.Name, p.BrandId, p.Volume })
                .IsUnique();

            modelBuilder.Entity<Perfume>().HasData(
                new Perfume
                {
                    Id = 1,
                    Name = "Mystic Oud",
                    BrandId = 1,
                    SeasonId = 4,
                    GenderId = 3,
                    Price = 199.99m,
                    Volume = 100,
                    Original = "Versace Inspiration"
                },
                new Perfume
                {
                    Id = 2,
                    Name = "Desert Rose",
                    BrandId = 7,
                    SeasonId = 2,
                    GenderId = 2,
                    Price = 149.99m,
                    Volume = 50,
                    Original = "Dior Salage"
                },
                new Perfume
                {
                    Id = 3,
                    Name = "Ocean Breeze",
                    BrandId = 4,
                    SeasonId = 14,
                    GenderId = 1,
                    Price = 89.99m,
                    Volume = 75,
                    Original = "Davidoff Cool Water"
                },
                new Perfume
                {
                    Id = 4,
                    Name = "Golden Sandal",
                    BrandId = 3,
                    SeasonId = 24,
                    GenderId = 3,
                    Price = 249.99m,
                    Volume = 100,
                    Original = "Guerlain Santal Royal"
                },
                new Perfume
                {
                    Id = 5,
                    Name = "Amber Night",
                    BrandId = 5,
                    SeasonId = 13,
                    GenderId = 1,
                    Price = 129.99m,
                    Volume = 100,
                    Original = "Yves Saint Laurent La Nuit de L'Homme"
                },
                new Perfume
                {
                    Id = 6,
                    Name = "Velvet Jasmine",
                    BrandId = 2,
                    SeasonId = 1234,
                    GenderId = 2,
                    Price = 109.99m,
                    Volume = 50,
                    Original = "Chanel Chance Eau Tendre"
                },
                new Perfume
                {
                    Id = 7,
                    Name = "Citrus Bloom",
                    BrandId = 6,
                    SeasonId = 12,
                    GenderId = 3,
                    Price = 99.99m,
                    Volume = 75,
                    Original = "Dolce & Gabbana Light Blue"
                },
                new Perfume
                {
                    Id = 8,
                    Name = "Midnight Vanilla",
                    BrandId = 8,
                    SeasonId = 4,
                    GenderId = 2,
                    Price = 139.99m,
                    Volume = 100,
                    Original = "Tom Ford Black Orchid"
                },
                new Perfume
                {
                    Id = 9,
                    Name = "Forest Whisper",
                    BrandId = 3,
                    SeasonId = 13,
                    GenderId = 1,
                    Price = 179.99m,
                    Volume = 100,
                    Original = "Hermes Terre d'Hermes"
                },
                new Perfume
                {
                    Id = 10,
                    Name = "White Musk",
                    BrandId = 6,
                    SeasonId = 134,
                    GenderId = 3,
                    Price = 119.99m,
                    Volume = 50,
                    Original = "Narciso Rodriguez For Her"
                },
                new Perfume
                {
                    Id = 11,
                    Name = "Oriental Spice",
                    BrandId = 1,
                    SeasonId = 124,
                    GenderId = 1,
                    Price = 189.99m,
                    Volume = 100,
                    Original = "Spicebomb by Viktor & Rolf"
                },
                new Perfume
                {
                    Id = 12,
                    Name = "Rosewood Elegance",
                    BrandId = 7,
                    SeasonId = 23,
                    GenderId = 2,
                    Price = 159.99m,
                    Volume = 75,
                    Original = "Chloé Nomade"
                },
                new Perfume
                {
                    Id = 13,
                    Name = "Aqua Mint",
                    BrandId = 4,
                    SeasonId = 24,
                    GenderId = 1,
                    Price = 79.99m,
                    Volume = 50,
                    Original = "Acqua di Gio by Giorgio Armani"
                },
                new Perfume
                {
                    Id = 14,
                    Name = "Royal Incense",
                    BrandId = 3,
                    SeasonId = 4,
                    GenderId = 3,
                    Price = 269.99m,
                    Volume = 100,
                    Original = "Amouage Interlude"
                },
                new Perfume
                {
                    Id = 15,
                    Name = "Sunny Neroli",
                    BrandId = 6,
                    SeasonId = 2,
                    GenderId = 2,
                    Price = 99.99m,
                    Volume = 75,
                    Original = "Neroli Portofino by Tom Ford"
                },
                new Perfume
                {
                    Id = 16,
                    Name = "Dark Leather",
                    BrandId = 5,
                    SeasonId = 1,
                    GenderId = 1,
                    Price = 149.99m,
                    Volume = 100,
                    Original = "John Varvatos Dark Rebel"
                },
                new Perfume
                {
                    Id = 17,
                    Name = "Soft Lavender",
                    BrandId = 2,
                    SeasonId = 14,
                    GenderId = 3,
                    Price = 89.99m,
                    Volume = 50,
                    Original = "Lavender Eau by Jo Malone"
                },
                new Perfume
                {
                    Id = 18,
                    Name = "Habibi Gold",
                    BrandId = 8,
                    SeasonId = 13,
                    GenderId = 3,
                    Price = 219.99m,
                    Volume = 100,
                    Original = "Habibi Classic"
                }
            );

            modelBuilder.Entity<Perfume>()
                .HasMany(p => p.TopScents)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "PerfumeTopScents",
                    j => j.HasOne<Scent>().WithMany().HasForeignKey("ScentId"),
                    j => j.HasOne<Perfume>().WithMany().HasForeignKey("PerfumeId"),
                    j => j.HasKey("PerfumeId", "ScentId")
                );

            modelBuilder.Entity<Perfume>()
                .HasMany(p => p.HeartScents)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "PerfumeHeartScents",
                    j => j.HasOne<Scent>().WithMany().HasForeignKey("ScentId"),
                    j => j.HasOne<Perfume>().WithMany().HasForeignKey("PerfumeId"),
                    j => j.HasKey("PerfumeId", "ScentId")
                );

            modelBuilder.Entity<Perfume>()
                .HasMany(p => p.BaseScents)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "PerfumeBaseScents",
                    j => j.HasOne<Scent>().WithMany().HasForeignKey("ScentId"),
                    j => j.HasOne<Perfume>().WithMany().HasForeignKey("PerfumeId"),
                    j => j.HasKey("PerfumeId", "ScentId")
                );


            modelBuilder.Entity("PerfumeBaseScents").HasData(
                new { PerfumeId = 1, ScentId = 12 },
                new { PerfumeId = 1, ScentId = 13 },
                new { PerfumeId = 1, ScentId = 5 },
                new { PerfumeId = 1, ScentId = 1 },

                new { PerfumeId = 2, ScentId = 13 },
                new { PerfumeId = 2, ScentId = 3 },
                new { PerfumeId = 2, ScentId = 12 },

                new { PerfumeId = 3, ScentId = 5 },
                new { PerfumeId = 3, ScentId = 12 },
                new { PerfumeId = 3, ScentId = 13 },

                new { PerfumeId = 4, ScentId = 12 },
                new { PerfumeId = 4, ScentId = 1 },
                new { PerfumeId = 4, ScentId = 13 },
                new { PerfumeId = 4, ScentId = 5 },

                new { PerfumeId = 5, ScentId = 13 },
                new { PerfumeId = 5, ScentId = 1 },
                new { PerfumeId = 5, ScentId = 12 },

                new { PerfumeId = 6, ScentId = 12 },
                new { PerfumeId = 6, ScentId = 5 },
                new { PerfumeId = 6, ScentId = 13 },

                new { PerfumeId = 7, ScentId = 1 },
                new { PerfumeId = 7, ScentId = 13 },
                new { PerfumeId = 7, ScentId = 12 },

                new { PerfumeId = 8, ScentId = 12 },
                new { PerfumeId = 8, ScentId = 5 },
                new { PerfumeId = 8, ScentId = 13 },

                new { PerfumeId = 9, ScentId = 5 },
                new { PerfumeId = 9, ScentId = 12 },
                new { PerfumeId = 9, ScentId = 1 },

                new { PerfumeId = 10, ScentId = 13 },
                new { PerfumeId = 10, ScentId = 12 },
                new { PerfumeId = 10, ScentId = 5 },

                new { PerfumeId = 11, ScentId = 12 },
                new { PerfumeId = 11, ScentId = 13 },
                new { PerfumeId = 11, ScentId = 1 },

                new { PerfumeId = 12, ScentId = 1 },
                new { PerfumeId = 12, ScentId = 12 },
                new { PerfumeId = 12, ScentId = 5 },

                new { PerfumeId = 13, ScentId = 5 },
                new { PerfumeId = 13, ScentId = 13 },
                new { PerfumeId = 13, ScentId = 12 },

                new { PerfumeId = 14, ScentId = 12 },
                new { PerfumeId = 14, ScentId = 1 },
                new { PerfumeId = 14, ScentId = 5 },

                new { PerfumeId = 15, ScentId = 13 },
                new { PerfumeId = 15, ScentId = 12 },
                new { PerfumeId = 15, ScentId = 5 },
                new { PerfumeId = 15, ScentId = 1 },

                new { PerfumeId = 16, ScentId = 5 },
                new { PerfumeId = 16, ScentId = 12 },
                new { PerfumeId = 16, ScentId = 13 },

                new { PerfumeId = 17, ScentId = 12 },
                new { PerfumeId = 17, ScentId = 1 },
                new { PerfumeId = 17, ScentId = 5 },

                new { PerfumeId = 18, ScentId = 1 },
                new { PerfumeId = 18, ScentId = 12 },
                new { PerfumeId = 18, ScentId = 13 }
            );

            modelBuilder.Entity("PerfumeTopScents").HasData(
                new { PerfumeId = 1, ScentId = 10 },
                new { PerfumeId = 1, ScentId = 7 },
                new { PerfumeId = 1, ScentId = 11 },

                new { PerfumeId = 2, ScentId = 2 },
                new { PerfumeId = 2, ScentId = 10 },
                new { PerfumeId = 2, ScentId = 14 },

                new { PerfumeId = 3, ScentId = 6 },
                new { PerfumeId = 3, ScentId = 14 },
                new { PerfumeId = 3, ScentId = 16 },

                new { PerfumeId = 4, ScentId = 10 },
                new { PerfumeId = 4, ScentId = 11 },

                new { PerfumeId = 5, ScentId = 7 },
                new { PerfumeId = 5, ScentId = 16 },
                new { PerfumeId = 5, ScentId = 14 },

                new { PerfumeId = 6, ScentId = 10 },

                new { PerfumeId = 7, ScentId = 14 },
                new { PerfumeId = 7, ScentId = 7 },
                new { PerfumeId = 7, ScentId = 6 },

                new { PerfumeId = 8, ScentId = 11 },
                new { PerfumeId = 8, ScentId = 10 },
                new { PerfumeId = 8, ScentId = 7 },

                new { PerfumeId = 9, ScentId = 6 },

                new { PerfumeId = 10, ScentId = 7 },
                new { PerfumeId = 10, ScentId = 16 },

                new { PerfumeId = 11, ScentId = 10 },
                new { PerfumeId = 11, ScentId = 6 },
                new { PerfumeId = 11, ScentId = 11 },

                new { PerfumeId = 12, ScentId = 11 },
                new { PerfumeId = 12, ScentId = 14 },

                new { PerfumeId = 13, ScentId = 14 },
                new { PerfumeId = 13, ScentId = 7 },
                new { PerfumeId = 13, ScentId = 10 },

                new { PerfumeId = 14, ScentId = 16 },

                new { PerfumeId = 15, ScentId = 10 },
                new { PerfumeId = 15, ScentId = 6 },
                new { PerfumeId = 15, ScentId = 7 },
                new { PerfumeId = 15, ScentId = 11 },

                new { PerfumeId = 16, ScentId = 11 },

                new { PerfumeId = 17, ScentId = 7 },
                new { PerfumeId = 17, ScentId = 14 },

                new { PerfumeId = 18, ScentId = 14 },
                new { PerfumeId = 18, ScentId = 6 }
            );

            modelBuilder.Entity("PerfumeHeartScents").HasData(
                new { PerfumeId = 1, ScentId = 9 },
                new { PerfumeId = 1, ScentId = 4 },
                new { PerfumeId = 1, ScentId = 8 },

                new { PerfumeId = 2, ScentId = 8 },
                new { PerfumeId = 2, ScentId = 3 },
                new { PerfumeId = 2, ScentId = 15 },

                new { PerfumeId = 3, ScentId = 11 },
                new { PerfumeId = 3, ScentId = 4 },

                new { PerfumeId = 4, ScentId = 2 },
                new { PerfumeId = 4, ScentId = 9 },
                new { PerfumeId = 4, ScentId = 3 },

                new { PerfumeId = 5, ScentId = 8 },
                new { PerfumeId = 5, ScentId = 15 },

                new { PerfumeId = 6, ScentId = 3 },
                new { PerfumeId = 6, ScentId = 4 },
                new { PerfumeId = 6, ScentId = 9 },

                new { PerfumeId = 7, ScentId = 2 },
                new { PerfumeId = 7, ScentId = 8 },

                new { PerfumeId = 8, ScentId = 9 },
                new { PerfumeId = 8, ScentId = 8 },
                new { PerfumeId = 8, ScentId = 15 },

                new { PerfumeId = 9, ScentId = 15 },
                new { PerfumeId = 9, ScentId = 3 },

                new { PerfumeId = 10, ScentId = 3 },
                new { PerfumeId = 10, ScentId = 4 },

                new { PerfumeId = 11, ScentId = 8 },
                new { PerfumeId = 11, ScentId = 9 },

                new { PerfumeId = 12, ScentId = 4 },
                new { PerfumeId = 12, ScentId = 15 },

                new { PerfumeId = 13, ScentId = 9 },
                new { PerfumeId = 13, ScentId = 2 },

                new { PerfumeId = 14, ScentId = 2 },
                new { PerfumeId = 14, ScentId = 8 },

                new { PerfumeId = 15, ScentId = 15 },
                new { PerfumeId = 15, ScentId = 3 },

                new { PerfumeId = 16, ScentId = 3 },
                new { PerfumeId = 16, ScentId = 4 },

                new { PerfumeId = 17, ScentId = 8 },
                new { PerfumeId = 17, ScentId = 2 },

                new { PerfumeId = 18, ScentId = 9 },
                new { PerfumeId = 18, ScentId = 15 }
            );

        }
    }
}
