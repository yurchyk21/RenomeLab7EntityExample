using Bogus;
using Lesson5_Lab.Domain.Entities;
using System;
using Lesson5_Lab.Domain;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lesson5_Lab
{
    class Program
    {
        static AppEFConfig context = new AppEFConfig();

        static void Main(string[] args)
        {
            context.Database.Migrate();
            SeedRoles();
            SeedUsers();
            SeedAppUserRole();
            SeedCategory();
            SeedProduct();
            //var query = context.Products.Select(x =>
            //new { x.Id, x.Name, CategoryName = x.Category.Name })
            //     .AsQueryable();

            //string name = "Tasty";
            //query = query.Where(x => x.Name.Contains(name));

            //string cat = "Game";
            //query = query.Where(x => x.CategoryName.Contains(cat));
             var query = context.Users
                .Include(x=>x.Roles)
                .AsQueryable();

            foreach (var user in query)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine($"Id:{user.Id}, Name:{user.Name}");
                Console.WriteLine("Roles:");
                foreach (var role in user.Roles)
                {
                    Console.WriteLine($"RoleName:{role.Name}");
                }
            }


        }

        static void SeedCategory()
        {
            var testCategories = new Faker<Category>("uk")
           .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);

            for (int i = 0; i < 100; i++)
            {
                var data = testCategories.Generate();
                var category = context.Categories
                    .SingleOrDefault(x => x.Name == data.Name);
                if (category == null)
                {
                    context.Categories.Add(data);
                    context.SaveChanges();
                }
            }
        }

        static void SeedProduct()
        {
            long[] catIds = context.Categories
                .Select(x => x.Id).ToArray();

            var testProducts = new Faker<Product>("uk")
            .RuleFor(c => c.CategoryId, f => f.PickRandom(catIds))
            .RuleFor(c => c.Name, f => f.Commerce.ProductName())
            .RuleFor(c => c.Price, f => f.Finance.Amount(1, 100));

            if (!context.Products.Any())
            {
                for (int i = 0; i < 1000; i++)
                {
                    var p = testProducts.Generate();
                    context.Products.Add(p);
                }
                context.SaveChanges();

            }

        }

        static void SeedUsers()
        {
            if (!context.Users.Any())
            {
                context.Users
                    .Add(
                    new AppUser
                    {
                        Name = "Ivan",
                        Phone = "222222222"
                    });
                context.Users
                    .Add(
                    new AppUser
                    {
                        Name = "Semen",
                        Phone = "33333333333"
                    });
                context.SaveChanges();
            }
        }
        static void SeedRoles()
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new AppRole { Name = "Admin" });
                context.Roles.Add(new AppRole { Name = "User" });
                context.Roles.Add(new AppRole { Name = "Buhgalter" });
                context.SaveChanges();
            }
        }

        static void SeedAppUserRole()
        {
            var appRole = context.Roles.SingleOrDefault(x => x.Name == "Buhgalter");
            var user = context.Users.SingleOrDefault(x => x.Name == "Semen");
            if (user.Roles == null)
                user.Roles = new Collection<AppRole>();
            user.Roles.Add(appRole);
            context.SaveChanges();        

        }


}
}
