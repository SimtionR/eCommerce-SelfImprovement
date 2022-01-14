using Self_Improve_eCommerce.Models.DomainObjects;
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
        Task<int> AddDeliveryAddresAsync(AddressRequestModel addressRequestModel, string userId);
        Task<bool> DeleteDeliveryAddressByIdAsync(int deliveryAddressId);
        Task<DeliveryAddres> GetDeliveryAddress(int deliveryAddressId);
        Task<IEnumerable<DeliveryAddres>> GetUserDeliveryAddresses(string userId);
    }
}
