using modelLayer;
using RepostoryLayer.Entity;

namespace RepostoryLayer.Services
{
    public interface IOrderRepo
    {
        public List<BookEntity> GetOrders(int userid);

        public List<BookEntity> AddToOrder(orderModel model, int UserId);

        public double GetPriceInOrder(int UserId);

        public List<BookEntity> GetallOrders(int userid);

    }
}