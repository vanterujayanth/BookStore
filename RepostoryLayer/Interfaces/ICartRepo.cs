using modelLayer;
using RepostoryLayer.Entity;

namespace RepostoryLayer.Services
{
    public interface ICartRepo
    {
        public List<BookEntity> GetCartBooks(int UserId);
        public List<BookEntity> AddToCart(CartModel model, int UserId);
        public CartModel UpdateQuantity(int UserId, CartModel model);
        public bool DeleteCart(DeleteCartModel model);
    }
}