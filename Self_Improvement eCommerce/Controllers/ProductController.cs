using Microsoft.AspNetCore.Mvc;
using Self_Improve_eCommerce.IServices;
using Self_Improve_eCommerce.Models.DomainObjects;
using Self_Improve_eCommerce.Models.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Route("allProducts")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await this.productService.GetAllProductsAsync();
            
            return products;
        }

        [HttpPost]
        [Route("addProduct")]
        public async Task<ActionResult> AddProduct(ProductRequestModel model)
        {
           var product =  await this.productService.AddProductAsync(model);

            return Created(nameof(this.Created), product);
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public async Task<ActionResult> DeleteProductById(int productId)
        {
            var deleted = await this.productService.DeleteProductByIdAsync(productId);

            return deleted == true ? Ok() : BadRequest();

        }

        [HttpPut]
        [Route("updateProduct")]
        public async Task<ActionResult> UpdateProductPrice(int productId, float price)
        {
            var updated = await this.productService.UpdateProductPriceAsync(productId, price);

            return updated == true ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("byId/{productId}")]
        public async Task<Product> GetProductById(int productId)
        {
            var product = await this.productService.GetProductByIdAsync(productId);

            return product;
        }

        [HttpGet]
        [Route("byName/{productName}")]
        public async Task<Product> GetProductByName(string productName)
        {
            var product = await this.productService.GetProductByNameAsync(productName);

            return product;
        }

    }
}
