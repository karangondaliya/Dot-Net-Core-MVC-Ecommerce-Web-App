using EcommerceWebApp.Models;

namespace EcommerceWebApp.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public void Update(ShoppingCart shoppingCart);
    }
}
