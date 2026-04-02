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
            _db.Products.Update(Obj);
        }
    }
}
