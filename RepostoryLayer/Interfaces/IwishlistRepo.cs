using modelLayer;
using RepostoryLayer.Entity;

namespace RepostoryLayer.Interfaces
{
    public interface IwishlistRepo
    {
        public List<BookEntity> GetWhishListBooks(int userid);
        public List<BookEntity> AddToWishList(AddWhishlist model);
        public bool DeleteWhishlist(DeleteCartModel model);
    }
}