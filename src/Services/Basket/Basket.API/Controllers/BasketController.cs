using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController :ControllerBase
    {
        private readonly IBasketRepository repository;

        public BasketController(IBasketRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{userName}",Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket (string userName)
        {
            var basket = await repository.GetBasket(userName);

            return Ok(basket ?? new ShoppingCart(userName));

        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await repository.DeleteBasket(userName);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
           

            return Ok(await repository.UpdateBasket(shoppingCart));

        }
    }
}
