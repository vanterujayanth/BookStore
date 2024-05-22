using modelLayer;
using RepostoryLayer.Entity;

namespace RepostoryLayer.Interfaces
{
    public interface IUserRepo
    {
        public UserModel RegisterUser(UserModel model);
        public LoginToken Login(LoginModel loginModel);
        public ForgotPasswordModel ForgotPassword(string email);
        public bool ResetPassword(string email, string password);
    }
}