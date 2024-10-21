using EcommerceWebApp.Models;

namespace EcommerceWebApp.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        public void Update(OrderDetail orderDetail);
    }
}
