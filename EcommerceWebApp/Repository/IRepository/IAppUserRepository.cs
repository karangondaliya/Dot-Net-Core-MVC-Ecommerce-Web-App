using EcommerceWebApp.Models;

namespace EcommerceWebApp.Repository.IRepository
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        public void Update(AppUser appUser);
    }
}
