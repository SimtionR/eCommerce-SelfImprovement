using Self_Improve_eCommerce.Models.DomainObjects;
using Self_Improve_eCommerce.Models.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByNameAsync(string productName);
        Task<Product> GetProductByIdAsync(int productId);
        Task<bool> DeleteProductByIdAsync(int productId);
        Task<int> AddProductAsync(ProductRequestModel requestProduct);
        Task<bool> UpdateProductPriceAsync(int productId, float price);

        
    }
}
