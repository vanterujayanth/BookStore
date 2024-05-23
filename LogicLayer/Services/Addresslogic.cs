using LogicLayer.Interfaces;
using modelLayer;
using RepostoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class Addresslogic:IAddresslogic
    {
        private readonly IAddressRepo _addressRepo;
        public Addresslogic(IAddressRepo _addressRepo)
        {
            this._addressRepo = _addressRepo;
        }
        public AddressModel AddAddress(AddressModel model)
        {
            return _addressRepo.AddAddress(model);
        }
        public List<AddressModel> GetAddresses(int UserId)
        {
            return _addressRepo.GetAddresses(UserId);
        }
        public UpdateAddressModel UpdateAddress(UpdateAddressModel model)
        {
            return _addressRepo.UpdateAddress(model);
        }
    }
}
