using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Get the list of all the products in the DB
        /// </summary>
        /// <returns>All products</returns>

        [HttpGet]
        [Route("allProducts")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await this.productService.GetAllProductsAsync();
            
            return products;
        }

        /// <summary>
        /// Adds a product in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>201</returns>

        [HttpPost]
        [Route("addProduct")]
        public async Task<ActionResult> AddProduct(ProductRequestModel model)
        {
           var product =  await this.productService.AddProductAsync(model);

            return Created(nameof(this.Created), product);
        }
        /// <summary>
        /// Deletes a product from the database based on ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpDelete]
        [Route("deleteProduct")]
        public async Task<ActionResult> DeleteProductById(int productId)
        {
            var deleted = await this.productService.DeleteProductByIdAsync(productId);

            return deleted == true ? Ok() : BadRequest();

        }


        /// <summary>
        /// Updates product price based on ID
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateProductPrice")]
        public async Task<ActionResult> UpdateProductPrice(int productId, float price)
        {
            var updated = await this.productService.UpdateProductPriceAsync(productId, price);

            return updated == true ? Ok() : BadRequest();
        }

        /// <summary>
        /// Updates product description based on ID
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateProductDescription")]
        public async Task<ActionResult> UpdateProductDescription(int productId, string description)
        {
            var updated = await this.productService.UpdateProductDescriptionAsync(productId, description);

            return updated == true ? Ok() : BadRequest();
        }
        /// <summary>
        /// Gets product by id 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("byId/{productId}")]
        public async Task<Product> GetProductById(int productId)
        {
            var product = await this.productService.GetProductByIdAsync(productId);

            return product;
        }

        /// <summary>
        /// Gets product by name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("byName/{productName}")]
        public async Task<Product> GetProductByName(string productName)
        {
            var product = await this.productService.GetProductByNameAsync(productName);

            return product;
        }

    }
}
