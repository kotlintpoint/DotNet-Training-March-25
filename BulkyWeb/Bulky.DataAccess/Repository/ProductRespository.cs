using Bulky.DataAccess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRespository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRespository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product Obj)
        {
            var ProductFromDb = _db.Products.FirstOrDefault(p => p.Id == Obj.Id);
            if (ProductFromDb != null) {
                ProductFromDb.Title = Obj.Title;
                ProductFromDb.Description = Obj.Description;
                ProductFromDb.Author = Obj.Author;
                ProductFromDb.ListPrice = Obj.ListPrice;
                ProductFromDb.Price = Obj.Price;
                ProductFromDb.Price50 = Obj.Price50;
                ProductFromDb.Price100 = Obj.Price100;
                ProductFromDb.CategoryId = Obj.CategoryId;
                if (Obj.ImageUrl != null && Obj.ImageUrl != "") {
                    ProductFromDb.ImageUrl = Obj.ImageUrl;
                }
            }
            //_db.Products.Update(Obj);
        }
    }
}
