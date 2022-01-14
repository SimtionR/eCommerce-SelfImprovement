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
    public class BasketService : IBasketService
    {
        private readonly SelfImproveDbContext ctx;
        private readonly ILogger<BasketService> logger;
      

        public BasketService(SelfImproveDbContext ctx, ILogger<BasketService> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }
        public async Task<int> AddProductToBasketAsync(Product product, int basketId, int quantity)
        {
            var basketItem = new BasketItem
            {
                ImageUrl = product.ImageUrl,
                ProductId = product.Id,
                Quantity = quantity,
                ProductName = product.Name,
                UnitPrice = product.Price,
                BasketId = basketId

            };

           

            var basket = await GetBasketByBasketIdAsync(basketId);
            basket.ListOfBasketItems.Add(basketItem);

            await this.ctx.SaveChangesAsync();
            return basketItem.Id;



            
        }

        public async Task<bool> ChangeQuantityToBasketItemAsync(int basketItemId, int quantity)
        {
            var basketItem = await this.ctx.BasketItems.Where(i => i.Id == basketItemId).FirstOrDefaultAsync();

            if (basketItem != null)
            {
                basketItem.Quantity = quantity;
                await ctx.SaveChangesAsync();
                return true;
            }

            return false;



        }

        public async Task<Basket> CreateBasketAsync(string userId)
        {

            this.logger.LogInformation("Created Basket and associated it with user's ID");
            var basket = new Basket { UserId = userId };

            await this.ctx.Baskets.AddAsync(basket);

            await ctx.SaveChangesAsync();

            return basket;
        }

        public async Task<IEnumerable<BasketItem>> GetAllBasketItemsAsync(int basketId)
        {
            var basket = await this.ctx.Baskets.Include(b=> b.ListOfBasketItems)
                .Where(b => b.Id == basketId).FirstOrDefaultAsync();

            return basket.ListOfBasketItems.ToList();
        }

        public async Task<Basket> GetBasketByBasketIdAsync(int basketId)
        {
            var basket = await ctx.Baskets.Where(b => b.Id == basketId).FirstOrDefaultAsync();
            return basket;
        }

        public async Task<Basket> GetBasketByUserIdAsync(string userId)
        {
            this.logger.LogInformation($"Called basket with id {userId}");

            var basket = await this.ctx.Baskets.Include(b=> b.ListOfBasketItems).Where(b => b.UserId == userId).FirstOrDefaultAsync();

            return basket;
        }

        public async Task<bool> RemoveAllItemsFromBasketAsync(int basketId)
        {
            var basket = await this.ctx.Baskets.Include(b=> b.ListOfBasketItems).Where(b => b.Id == basketId).FirstOrDefaultAsync();

            basket.ListOfBasketItems.Clear();

            await this.ctx.SaveChangesAsync();

            return basket.ListOfBasketItems.Count == 0 ? true : false;

            
        }

        public async Task<bool> RemoveBasketItemAsync(int basketId, int basketItemId)
        {
            var basketItem = await ctx.BasketItems.Where(b => b.BasketId == basketId && b.Id == basketItemId).FirstOrDefaultAsync();

            if(basketItem!=null)
            {
                ctx.BasketItems.Remove(basketItem);
                await ctx.SaveChangesAsync();
                return true;
            }

            return false;

        }

        
    }
}
