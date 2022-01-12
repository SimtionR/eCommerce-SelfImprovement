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
        Task<bool> AddProductToBasketAsync(BasketItemRequestModel basketItemRequestModel);
        Task<bool> ChangeQuantityToBasketItemAsync(int basketItemid, int quantity);
        Task<bool> RemoveProductFromBasketAsync(int basketItemId);
    }
}
