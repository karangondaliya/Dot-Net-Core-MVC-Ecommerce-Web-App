using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Linq.Expressions;
using EcommerceWebApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceWebApp.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository Category { get; }
		IProductRepository Product { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IAppUserRepository AppUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        void Save();
        IDbContextTransaction BeginTransaction();
    }
}
