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
        Task<IEnumerable<Order>> GetAllOrdersFromUser(string userId);
        Task<int> CreateOrderAsync(int addressId, string userId, Basket basket, float discount, string discountName);
        //Task<bool> ChangeOrderAsync(OrderRequestModel orderModel, int orderId);
        Task<bool> CancelOrderAsync(int orderId);
        Task<Order> GetOrder(int orderId);

    }
}
