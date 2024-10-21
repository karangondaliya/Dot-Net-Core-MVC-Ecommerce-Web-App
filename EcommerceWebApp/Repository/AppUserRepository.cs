using EcommerceWebApp.Data;
using EcommerceWebApp.Models;
using EcommerceWebApp.Repository.IRepository;

namespace EcommerceWebApp.Repository
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private ApplicationDbContext _db;
        public AppUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(AppUser appUser)
        {
            _db.AppUsers.Update(appUser);
        }
    }
}
