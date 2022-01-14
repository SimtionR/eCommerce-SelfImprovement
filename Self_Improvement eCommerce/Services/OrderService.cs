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
    public class OrderService : IOrderService
    {
        private readonly SelfImproveDbContext ctx;
        private readonly ILogger<OrderService> logger;
     

        public OrderService(SelfImproveDbContext ctx, ILogger<OrderService> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }
        public async Task<bool> CancelOrderAsync(int orderId)
        {
            this.logger.LogInformation($"Called order with orderId: {orderId}"); ;
            var order = await this.ctx.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync();

            if(order!=null)
            {

                this.logger.LogInformation("Deleting order");
                var orderItemsToBeRemoved = await this.ctx.OrderItems.Where(o => o.OrderId == orderId).ToListAsync();

                this.ctx.RemoveRange(orderItemsToBeRemoved);
                this.ctx.Remove(order);
                await this.ctx.SaveChangesAsync();
                return true;
            }
            this.logger.LogInformation("Order not found");
            return false;
            
        }

        public async Task<int> CreateOrderAsync(int addressId, string userId, Basket basket, float discount, string discountName)
        {
            float totalPrice = 0;
            List<OrderItem> orderedItems = new List<OrderItem>();
            foreach(var itm in basket.ListOfBasketItems)
            {
                totalPrice += itm.UnitPrice * itm.Quantity;
               
            }
            var deliveryPrice = totalPrice > 500 ? 0 : 10;

            var order = new Order
            {
                DeliveryId = addressId,
                DeliveryPrice = deliveryPrice,
                Discount = discount,
                DiscountName = discountName,
                TotalPrice = totalPrice -(totalPrice*discount),
                FinalPrice = totalPrice+deliveryPrice,
                UserId = userId,
             
            };
            await this.ctx.Orders.AddAsync(order);

            foreach(var itm in basket.ListOfBasketItems)
            {
                var orderItem = new OrderItem
                {
                    ImageUrl = itm.ImageUrl,
                    ProductId = itm.ProductId,
                    ProductName = itm.ProductName,
                    Quantity = itm.Quantity,
                    UnitPrice = itm.UnitPrice,
                    OrderId = order.Id
                };

                orderedItems.Add(orderItem);
            }

            order.ListOfOrderItems = orderedItems;
            this.ctx.OrderItems.AddRange(orderedItems);
            await this.ctx.SaveChangesAsync();
            return order.Id;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync(int orderId)
        {
            this.logger.LogInformation($"Get order items from order {orderId}");
            var orderItems = await this.ctx.OrderItems.Where(o => o.OrderId == orderId).ToListAsync();
            return orderItems;

        }

        public async Task<IEnumerable<Order>> GetAllOrdersFromUser(string userId)
        {

            this.logger.LogInformation($"Get All orders for user ${userId}");
            var orders = await this.ctx.Orders.Include(o=> o.ListOfOrderItems).Where(o => o.UserId == userId).ToListAsync();

            return orders;
        }

        public async Task<Order> GetOrder(int orderId)
        {
            var order = await this.ctx.Orders.Include(o => o.ListOfOrderItems).Where(o => o.Id == orderId).FirstOrDefaultAsync();
            return order;
        }
    }
}
