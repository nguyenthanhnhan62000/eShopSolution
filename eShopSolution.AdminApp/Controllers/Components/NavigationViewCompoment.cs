using eShopSolution.AdminApp.Models;
using eShopSolution.AdminApp.Services;
using eShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers.Components
{
    public class NavigationViewCompoment : ViewComponent
    {
        private readonly ILanguageApiClient _LanguageApiClient;
        public NavigationViewCompoment(ILanguageApiClient LanguageApiClient)
        {

            _LanguageApiClient = LanguageApiClient;
        }

        

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _LanguageApiClient.GetAll();
            var navigation = new NavigationViewModel()
            {
                CurrentLanguageId = HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.DefaultLanguageId),Languages = languages.ResultObj
            };

            return View("Default", navigation);
        }
    }
}
