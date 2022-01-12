using Self_Improve_eCommerce.Models.DomainObjects;
using Self_Improve_eCommerce.Models.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync(int orderId);
        Task<int> CreateOrderAsync(OrderRequestModel orderModel, int addressId, string userId);
        Task<bool> ChangeOrderAsync(OrderRequestModel orderModel, int orderId);
        Task<bool> CancelOrderAsync(int orderId);

    }
}
