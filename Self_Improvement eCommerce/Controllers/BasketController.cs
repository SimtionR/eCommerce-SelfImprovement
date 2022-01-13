﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class BasketController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IBasketService basketService;

        public BasketController(UserManager<User> userManager, IBasketService basketService)
        {
            this.userManager = userManager;
            this.basketService = basketService;
           
        }

        [HttpGet]
        [Route("basketItems")]
        public async Task<IEnumerable<BasketItem>> GetBasketItems()
        {
            var basket = await GettingBasketReadyAsync();

            var basketItems = await this.basketService.GetAllBasketItemsAsync(basket.Id);

            return basketItems;

            
        }


        [HttpPost]
        [Route("addBasketItem")]
        public async Task<ActionResult> AddBasketItem (BasketItemRequestModel model)
        {
            Basket basket = await GettingBasketReadyAsync();



            var basketItem =await  this.basketService.AddProductToBasketAsync(model, basket.Id);


            return Created(nameof(this.Created), basketItem);
        }

        [HttpDelete]
        [Route("deleteBasketItem")]
        public async Task<ActionResult> DeleteBasketItem(int basktId, int basketItemId)
        {
            var isDeleted = await this.basketService.RemoveBasketItemAsync(basktId, basketItemId);

            if (isDeleted)
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [Route("clearBasket")]
        public async Task<ActionResult> ClearBasket()
        {
            var basket =await  GettingBasketReadyAsync();

            var isEmpty = await this.basketService.RemoveAllItemsFromBasketAsync(basket.Id);

            return isEmpty ? Ok() : NotFound();
        }

        [HttpPut]
        [Route("changeQuantity")]
        public async Task<ActionResult> ChangeProductQuantity(int basketItemId, int quantity)
        {
            var isChanged = await this.basketService.ChangeQuantityToBasketItemAsync(basketItemId, quantity);

            return isChanged ? Ok() : NotFound();
        }

        private async Task<Basket> GettingBasketReadyAsync()
        {
            var claims = this.User.Identities.FirstOrDefault().Claims;

            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;


            var basket = await this.basketService.GetBasketByUserIdAsync(userId);

            if (basket == null)
            {
                basket = await this.basketService.CreateBasketAsync(userId);
            }

            return basket;
        }





    }
}
