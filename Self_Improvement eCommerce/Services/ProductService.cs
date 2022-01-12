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
    public class ProductService : IProductService
    {
        private readonly SelfImproveDbContext ctx;
        private readonly ILogger<ProductService> logger;

        public ProductService(SelfImproveDbContext ctx, ILogger<ProductService> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }
        public async Task<int> AddProductAsync(ProductRequestModel requestProduct)
        {
            var product = new Product
            {
                Price = requestProduct.Price,
                Description = requestProduct.Description,
                ImageUrl = requestProduct.ImageUrl,
                Name = requestProduct.Name
            };

            ctx.Products.Add(product);
            await ctx.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> DeleteProductByIdAsync(int productId)
        {
            logger.LogInformation($"Deleting product with id {productId}");
            var prod = await ctx.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();

            if(prod !=null)
            {
                ctx.Remove(prod);
                await ctx.SaveChangesAsync();
                return true;
            }

            return false;
            
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            logger.LogInformation("All products called");
            return await ctx.Products.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            logger.LogInformation($"Product with id {productId} was called");
            return await ctx.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            logger.LogInformation($"Product with the name {productName} was called");
            return await ctx.Products.Where(p => p.Name == productName).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProductDescriptionAsync(int productId, string description)
        {
            var product = await ctx.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();

            if (product != null)
            {
                product.Description = description;
                await this.ctx.SaveChangesAsync();
                return true;
            }

            return false;

        }

        public async Task<bool> UpdateProductPriceAsync(int productId, float price)
        {
            var product = await ctx.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();

            if (product != null)
            {
                product.Price = price;
                await this.ctx.SaveChangesAsync();
                return true;
            }

            return false;


        }
    }
}
