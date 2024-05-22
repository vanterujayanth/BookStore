using modelLayer;
using RepostoryLayer.Entity;

namespace LogicLayer.Interfaces
{
    public interface IUserlogic
    {
        public UserModel RegisterUser(UserModel model);
        public LoginToken Login(LoginModel loginModel);
        public ForgotPasswordModel ForgotPassword(string email);
        public bool ResetPassword(string email, string password);
    }
}