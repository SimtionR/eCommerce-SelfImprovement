using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Self_Improve_eCommerce.IServices;
using Self_Improve_eCommerce.Models.DomainObjects;
using Self_Improve_eCommerce.Models.RequestObjects;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Controllers
{

    [Authorize]
    public class AddresController : ApiController
    {
        private readonly IAddressService addressService;

        public AddresController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpPost]
        [Route("addDeliveryAddress")]
        public async Task<ActionResult> AddDeliveryAddres(AddressRequestModel model)
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var address = await this.addressService.AddDeliveryAddresAsync(model, userId);


            return Created(nameof(this.Created), address);
        }


        [HttpGet]
        [Route("getAddress")]
        public async Task<DeliveryAddres> GetDeliveryAddres(int deliveryAddresId)
        {
            var address = await this.addressService.GetDeliveryAddress(deliveryAddresId);
            return address;
        }


        [HttpGet]
        [Route("userDeliveryAddresses")]
        public async Task<IEnumerable<DeliveryAddres>> GetUsersDeliveryAddresses()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var addresses = await this.addressService.GetUserDeliveryAddresses(userId);

            return addresses;


        }

        [HttpDelete]
        [Route("deleteAddress")]
        public async Task<ActionResult> DeleteDeliveryAddress(int addressId)
        {
            var isDeleted = await this.addressService.DeleteDeliveryAddressByIdAsync(addressId);
            return isDeleted ? Ok() : NotFound();   
        }
    }
}
