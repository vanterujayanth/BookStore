using LogicLayer.Interfaces;
using modelLayer;
using RepostoryLayer.Entity;
using RepostoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class Userlogic:IUserlogic
    {
        private readonly IUserRepo _userRepo;
        public Userlogic(IUserRepo _userRepo) 
        {
            this._userRepo = _userRepo;
        }
        public UserModel RegisterUser(UserModel model)
        { return _userRepo.RegisterUser(model); }
        public LoginToken Login(LoginModel loginModel)
        {
            return _userRepo.Login(loginModel);
        }
        public ForgotPasswordModel ForgotPassword(string email)
        {
            return _userRepo.ForgotPassword(email);
        }
        public bool ResetPassword(string email, string password)
        {
            return _userRepo.ResetPassword(email, password);    
        }
    }
}
