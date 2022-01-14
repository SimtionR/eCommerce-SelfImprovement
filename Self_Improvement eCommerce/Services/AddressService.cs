using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Self_Improve_eCommerce.Data;
using Self_Improve_eCommerce.IServices;
using Self_Improve_eCommerce.Models.DomainObjects;
using Self_Improve_eCommerce.Models.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Services
{
    public class AddressService : IAddressService
    {
        private readonly SelfImproveDbContext ctx;
        private readonly ILogger<AddressService> logger;

        public AddressService(SelfImproveDbContext ctx, ILogger<AddressService> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }
        public async Task<int> AddDeliveryAddresAsync(AddressRequestModel addressRequestModel, string userId)
        {
            var address = new DeliveryAddres
            {
                City = addressRequestModel.City,
                ApartamenetNumber = addressRequestModel.ApartamenetNumber,
                Street = addressRequestModel.Street,
                ZipCode = addressRequestModel.ZipCode,
                UserId= userId
                

            };
            this.ctx.DeliveryAddres.Add(address);
            await this.ctx.SaveChangesAsync();

            return address.Id ;
        }

        public async Task<bool> DeleteDeliveryAddressByIdAsync(int deliveryAddressId)
        {
            var address = await this.ctx.DeliveryAddres.Where(a => a.Id == deliveryAddressId).FirstOrDefaultAsync();
            if(address!=null)
            {
                this.ctx.DeliveryAddres.Remove(address);
                await this.ctx.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<DeliveryAddres> GetDeliveryAddress(int deliveryAddressId)
        {
            var address = await this.ctx.DeliveryAddres.Where(d => d.Id == deliveryAddressId).FirstOrDefaultAsync();

            return address;
        }

        public async Task<IEnumerable<DeliveryAddres>> GetUserDeliveryAddresses(string userId)
        {
            var addresses = await this.ctx.DeliveryAddres.Where(d => d.UserId == userId).ToListAsync();
            return addresses;
        }
    }
}
