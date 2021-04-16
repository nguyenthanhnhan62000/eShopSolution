using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eShopSolutionWebApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using LazZiya.ExpressLocalization;
using eShopSolution.ApiIntegration;
using System.Globalization;
using eShopSolution.Utilities.Constants;

namespace eShopSolutionWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _ProductApiClient;

        public HomeController(ILogger<HomeController> logger
            , ISharedCultureLocalizer loc
            ,ISlideApiClient slideApiClient
            , IProductApiClient productApiClient)
        {
          
            _logger = logger;
            _loc = loc;
            _slideApiClient = slideApiClient;
            _ProductApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {

            //var msg = _loc.GetLocalizedString("Vietnamese");
            var culture = CultureInfo.CurrentCulture.Name;

            var viewModel = new HomeViewModel()
            {
                   Slides = await _slideApiClient.GetAll(),
                   FeaturedProducts = await _ProductApiClient.GetFeaturedProduct(culture,SystemConstants.ProductSettings.NumberOffFeaturedProducts),
                   LatestProducts= await _ProductApiClient.GetLastedProduct(culture, SystemConstants.ProductSettings.NumberOffLastedProducts),

            };
    
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
