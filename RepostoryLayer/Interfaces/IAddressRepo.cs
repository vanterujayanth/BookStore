using modelLayer;

namespace RepostoryLayer.Services
{
    public interface IAddressRepo
    {
        public AddressModel AddAddress(AddressModel model);
        public List<AddressModel> GetAddresses(int UserId);
        public UpdateAddressModel UpdateAddress(UpdateAddressModel model);
    }
}