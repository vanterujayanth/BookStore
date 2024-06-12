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
    public class orderlogic:IOrderlogic
    {
        private readonly IOrderRepo _repo;
        public orderlogic(IOrderRepo _repo)
        {
            this._repo = _repo;
        }
        public List<BookEntity> GetOrders(int userid)
        {
            return _repo.GetOrders(userid);
        }

        public List<BookEntity> AddToOrder(orderModel model, int UserId)
        {
            return _repo.AddToOrder(model, UserId);
        }

        public double GetPriceInOrder(int UserId)
        {
            return _repo.GetPriceInOrder(UserId);
        }

        public List<BookEntity> GetallOrders(int userid)
        {
            return _repo.GetallOrders(userid);
        }
    }
}
