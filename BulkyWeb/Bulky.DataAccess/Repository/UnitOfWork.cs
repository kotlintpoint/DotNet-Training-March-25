using Bulky.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }

        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public UnitOfWork(ApplicationDbContext db) {
            _db = db;
            Category = new CategoryRespository(db);
            Products = new ProductRespository(db);
            Companies = new CompanyRespository(db);
            ShoppingCarts = new ShoppingCartRespository(db);
            ApplicationUsers = new ApplicationUserRespository(db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
