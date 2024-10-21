using EcommerceWebApp.Data;
using EcommerceWebApp.Models;
using EcommerceWebApp.Repository.IRepository;
using System.Linq.Expressions;

namespace EcommerceWebApp.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.categories.Update(obj);
        }
    }
}
