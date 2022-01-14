using Self_Improve_eCommerce.Models.DomainObjects;
using Self_Improve_eCommerce.Models.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.IServices
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketItem>> GetAllBasketItemsAsync(int basketId);
        Task<int> AddProductToBasketAsync(Product product, int basketId, int quantity);
        Task<bool> ChangeQuantityToBasketItemAsync(int basketItemId, int quantity);
       
        Task<bool> RemoveAllItemsFromBasketAsync(int basketId);
        Task<Basket> CreateBasketAsync(string userId);
        Task<Basket> GetBasketByUserIdAsync(string userId);
        Task<Basket> GetBasketByBasketIdAsync(int basketId);
        Task<bool> RemoveBasketItemAsync(int basketId, int basketItemId);
    }
}
