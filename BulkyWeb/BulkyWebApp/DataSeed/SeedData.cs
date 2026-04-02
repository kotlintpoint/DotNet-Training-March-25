
using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataSeed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Name = "Fiction", DisplayOrder = 1 },
                        new Category { Name = "Non-Fiction", DisplayOrder = 2 },
                        new Category { Name = "Science", DisplayOrder = 3 }
                    );
                }
                if (!context.Products.Any()) {
                    context.Products.AddRange(
                        new Product
                        {
                            Title = "Mastering C#",
                            Description = "A comprehensive guide to modern C# programming.",
                            Author = "John Doe",
                            ISBN = "978-1234567890",
                            ListPrice = 499,
                            Price = 450,
                            Price50 = 400,
                            Price100 = 350,
                            CategoryId = 1
                        },
                        new Product
                        {
                            Title = "ASP.NET Core Essentials",
                            Description = "Learn how to build scalable web applications using ASP.NET Core.",
                            Author = "Jane Smith",
                            ISBN = "978-0987654321",
                            ListPrice = 599,
                            Price = 550,
                            Price50 = 500,
                            Price100 = 450,
                            CategoryId = 2
                        },
                        new Product
                        {
                            Title = "Entity Framework Core in Action",
                            Description = "Practical techniques for working with EF Core in real-world projects.",
                            Author = "Michael Brown",
                            ISBN = "978-1122334455",
                            ListPrice = 399,
                            Price = 350,
                            Price50 = 325,
                            Price100 = 300,
                            CategoryId = 1
                        }
                    );
                }
                context.SaveChanges();
            }
        }
    }
}
