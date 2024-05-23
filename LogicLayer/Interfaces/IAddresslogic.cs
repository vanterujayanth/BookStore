using modelLayer;

namespace LogicLayer.Interfaces
{
    public interface IAddresslogic
    {
        public AddressModel AddAddress(AddressModel model);
        public List<AddressModel> GetAddresses(int UserId);
        public UpdateAddressModel UpdateAddress(UpdateAddressModel model);
    }
}