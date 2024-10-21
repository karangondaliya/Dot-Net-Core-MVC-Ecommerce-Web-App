using EcommerceWebApp.Models;

namespace EcommerceWebApp.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
