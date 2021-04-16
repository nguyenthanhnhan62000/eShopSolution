using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.ApiIntegration;
using eShopSolution.Utilities.Constants;
using eShopSolutionWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eShopSolutionWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public CartController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

    
        public async Task<IActionResult> AddtoCart(int id, string LanguageId)
        {
            var product = await _productApiClient.GetById(id,LanguageId);
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
            currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            int quantity = 1;

            if(currentCart.Any(x => x.ProductId == id))
            {
                quantity = currentCart.First(x => x.ProductId == id).Quantity + 1;
            }
            var cartItem = new CartItemViewModel()
            {
                ProductId = id,
                Description = product.Description,
                Image = product.ThumbnailImage,
                Name = product.Name,
                Quantity = quantity
            };
            currentCart.Add(cartItem);
            var a = JsonConvert.SerializeObject(currentCart);
            var b= JsonConvert.DeserializeObject(a);
            HttpContext.Session.SetString(SystemConstants.CartSession,JsonConvert.SerializeObject(currentCart));
            return Ok();
        }

    }
}
