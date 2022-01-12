using Self_Improve_eCommerce.Models.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.IServices
{
    public interface IAddressService
    {
        Task<int> AddDeliveryAddresAsync(AddressRequestModel addressRequestModel);
        Task<bool> DeleteDeliveryAddressByIdAsync(int deliveryId);
        Task<bool> ChangeDeliveryAddresByIdAsync(int deliveryId, AddressRequestModel addressRequestModel);
    }
}
