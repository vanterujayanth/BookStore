using LogicLayer.Interfaces;
using modelLayer;
using RepostoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class Adminlogic:IAdminlogic
    {
        private readonly IAdminRepo _repo;
        public Adminlogic(IAdminRepo _repo)
        {
            this._repo = _repo;
        }
        public LoginToken Login(LoginModel loginModel)
        { 
            return _repo.Login(loginModel); 
        }
    }
}
