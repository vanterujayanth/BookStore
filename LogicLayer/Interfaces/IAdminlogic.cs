using modelLayer;

namespace LogicLayer.Interfaces
{
    public interface IAdminlogic
    {
        public LoginToken Login(LoginModel loginModel);
    }
}