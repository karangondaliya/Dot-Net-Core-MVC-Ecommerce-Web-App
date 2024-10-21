using EcommerceWebApp.Models;

namespace EcommerceWebApp.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}
