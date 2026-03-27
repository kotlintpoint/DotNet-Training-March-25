
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
                if (context.Categories.Any())
                {
                    return;   // DB has been seeded
                }
                context.Categories.AddRange(
                    new Category { Name = "Fiction", DisplayOrder = 1 },
                    new Category { Name = "Non-Fiction", DisplayOrder = 2 },
                    new Category { Name = "Science", DisplayOrder = 3 }
                );
                context.SaveChanges();
            }
        }
    }
}
