using EcommerceWebApp.Models;

namespace EcommerceWebApp.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        public void Update(OrderHeader orderHeader);
        public void UpdateStatus(int id,string status);
    }
}
