using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Products { get; }

        ICompanyRepository Companies { get; }
        IShoppingCartRepository ShoppingCarts { get; }

        IApplicationUserRepository ApplicationUsers { get; }

        void Save();
    }
}
