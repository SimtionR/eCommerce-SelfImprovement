using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Self_Improve_eCommerce.IServices;
using Self_Improve_eCommerce.Models.DomainObjects;
using Self_Improve_eCommerce.Models.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        private readonly IOrderService orderService;
        private readonly IAddressService addressService;
        private readonly IBasketService basketService;
        private const string No_Discount = "No Discount";
        private const string HCustomer = "Home Customer";
        private const string FavCustomer = "Favourite Customer";

        public OrderController(IOrderService orderService, IAddressService addressService, IBasketService basketService)
        {
            this.orderService = orderService;
            this.addressService = addressService;
            this.basketService = basketService;
        }
        [HttpPost]
        [Route("createOrder")]
        public async Task<ActionResult> CreateOrder( int addressId)
        {
            var address = await this.addressService.GetDeliveryAddress(addressId);
            if (address == null)
            {
                return BadRequest();
            }

            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var basket = await this.basketService.GetBasketByUserIdAsync(userId);


            if (basket.ListOfBasketItems.Count > 0)
            {

                var discountName = "";
                float discount = 0;
                var orders = await this.orderService.GetAllOrdersFromUser(userId);
                var numberOfOrders = orders.Count();

                ApplyDiscounts(userId, out discountName, out discount, numberOfOrders);

                var order = await this.orderService.CreateOrderAsync(addressId, userId, basket, discount, discountName);



                return Created(nameof(this.Created), order);
            }

            return BadRequest();

        }

        private  void ApplyDiscounts(string userId, out string discountName, out float discount, int numberOfOrders)
        {
          
           
            switch (numberOfOrders)
            {
                case int n when (n > 4 && n < 10):
                    discount = 0.05f;
                    discountName = HCustomer;
                    break;


                case > 9:
                    discount = 0.1f;
                    discountName = FavCustomer;
                    break;

                default:
                    discountName = No_Discount;
                    discount = 0;
                    break;

            }
        }

        [HttpGet]
        [Route("orderById")]
        public async Task<Order> GetOrderById(int orderId)
        {
            var order = await this.orderService.GetOrder(orderId);
            return order;
           
         
        }

        [HttpGet]
        [Route("allOrders")]
        public async Task<IEnumerable<Order>> GetAllOrdersFromUser()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var orders = await this.orderService.GetAllOrdersFromUser(userId);

            return orders;
        }

        [HttpDelete]
        [Route("cancelOrder")]
        public async Task<ActionResult> CancelOrder(int orderId)
        {
            var isCanceled = await this.orderService.CancelOrderAsync(orderId);

            return isCanceled ? Ok() : NotFound();
        }
    }
}
