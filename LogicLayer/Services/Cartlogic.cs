using LogicLayer.Interfaces;
using modelLayer;
using RepostoryLayer.Entity;
using RepostoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{

    public class Cartlogic:ICartlogic
    {
        private readonly ICartRepo _repo;
        public Cartlogic(ICartRepo _repo)
        {
            this._repo = _repo;
        }
        public List<BookEntity> GetCartBooks(int UserId)
        {
            return _repo.GetCartBooks(UserId);
        }
        public List<BookEntity> AddToCart(CartModel model, int UserId)
        {
            return _repo.AddToCart(model, UserId);
        }
        public CartModel UpdateQuantity(int UserId, CartModel model)
        {
            return _repo.UpdateQuantity(UserId, model);
        }
        public bool DeleteCart(DeleteCartModel model)
        {
            return _repo.DeleteCart(model);
        }

    }

    
    
}
