using EcommerceWebApp.Data;
using EcommerceWebApp.Models;
using EcommerceWebApp.Repository.IRepository;

namespace EcommerceWebApp.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
            }
		}
    }
}
