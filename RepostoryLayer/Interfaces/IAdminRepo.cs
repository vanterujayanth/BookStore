using modelLayer;

namespace RepostoryLayer.Interfaces
{
    public interface IAdminRepo
    {
        public LoginToken Login(LoginModel loginModel);
    }
}