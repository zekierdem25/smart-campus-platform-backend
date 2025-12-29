using SmartCampus.API.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartCampus.API.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // 1. Ensure Database Created
        // await context.Database.EnsureCreatedAsync(); // Using Migrate() in Program.cs instead

        // 2. Ensure Cafeteria Exists
        var cafeteria = await context.Cafeterias.FirstOrDefaultAsync(c => c.Name == "Merkez Kafeterya");
        if (cafeteria == null)
        {
            cafeteria = new Cafeteria
            {
                Id = Guid.NewGuid(),
                Name = "Merkez Kafeterya",
                Location = "Kampüs Merkezi",
                Capacity = 500,
                IsActive = true
            };
            context.Cafeterias.Add(cafeteria);
            await context.SaveChangesAsync();
        }

        // 3. Add Menus for 29 Dec 2025 - 2 Jan 2026
        // Base start date: 29 Dec 2025 (Monday)
        var startDate = new DateTime(2025, 12, 29, 0, 0, 0, DateTimeKind.Utc);

        var menus = new List<MealMenu>
        {
            // Monday 29/12
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate,
                MealType = MealType.Lunch,
                ItemsJson = "[\"Mercimek Çorbası\", \"Orman Kebabı\", \"Bulgur Pilavı\", \"Cacık\"]",
                Price = 60.00m,
                CalorieCount = 850,
                IsPublished = true
            },
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate,
                MealType = MealType.Dinner,
                ItemsJson = "[\"Ezogelin Çorbası\", \"Tavuk Sote\", \"Pirinç Pilavı\", \"Salata\"]",
                Price = 60.00m,
                CalorieCount = 780,
                IsPublished = true
            },

            // Tuesday 30/12
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate.AddDays(1),
                MealType = MealType.Lunch,
                ItemsJson = "[\"Domates Çorbası\", \"Etli Nohut\", \"Pirinç Pilavı\", \"Turşu\"]",
                Price = 60.00m,
                CalorieCount = 900,
                IsPublished = true
            },
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate.AddDays(1),
                MealType = MealType.Dinner,
                ItemsJson = "[\"Tarhana Çorbası\", \"Köfte Patates\", \"Makarna\", \"Meyve\"]",
                Price = 60.00m,
                CalorieCount = 950,
                IsPublished = true
            },

            // Wednesday 31/12
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate.AddDays(2),
                MealType = MealType.Lunch,
                ItemsJson = "[\"Yayla Çorbası\", \"Kuru Fasulye\", \"Pilav\", \"Yoğurt\"]",
                Price = 60.00m,
                CalorieCount = 880,
                IsPublished = true
            },
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate.AddDays(2),
                MealType = MealType.Dinner,
                ItemsJson = "[\"Mercimek Çorbası\", \"Hünkar Beğendi\", \"Pilav\", \"Tatlı\"]",
                Price = 75.00m, // Special new year menu?
                CalorieCount = 1100,
                IsPublished = true
            },

            // Thursday 1/1
             new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate.AddDays(3),
                MealType = MealType.Lunch,
                ItemsJson = "[\"Ezogelin Çorbası\", \"Tavuk Şiş\", \"Bulgur Pilavı\", \"Ayran\"]",
                Price = 60.00m,
                CalorieCount = 820,
                IsPublished = true
            },
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate.AddDays(3),
                MealType = MealType.Dinner,
                ItemsJson = "[\"Domates Çorbası\", \"Kıymalı Pide\", \"Salata\", \"İçecek\"]",
                Price = 65.00m,
                CalorieCount = 900,
                IsPublished = true
            },

            // Friday 2/1
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate.AddDays(4),
                MealType = MealType.Lunch,
                ItemsJson = "[\"Mercimek Çorbası\", \"Balık Buğulama\", \"Roka Salata\", \"Helva\"]",
                Price = 70.00m,
                CalorieCount = 850,
                IsPublished = true
            },
            new MealMenu
            {
                CafeteriaId = cafeteria.Id,
                Date = startDate.AddDays(4),
                MealType = MealType.Dinner,
                ItemsJson = "[\"Sebze Çorbası\", \"Et Haşlama\", \"Pilav\", \"Meyve\"]",
                Price = 60.00m,
                CalorieCount = 800,
                IsPublished = true
            }
        };

        foreach (var menu in menus)
        {
            // Check if menu exists for this date/mealtype/cafeteria
            var exists = await context.MealMenus.AnyAsync(m => 
                m.CafeteriaId == menu.CafeteriaId && 
                m.Date.Date == menu.Date.Date && 
                m.MealType == menu.MealType);

            if (!exists)
            {
                context.MealMenus.Add(menu);
            }
        }

        await context.SaveChangesAsync();
    }
}
