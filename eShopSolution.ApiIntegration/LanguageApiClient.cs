using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.Untilities.Slides;
using eShopSolution.ApiIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class LanguageApiClient : BaseApiClient,ILanguageApiClient
    {

       

        public LanguageApiClient(IHttpClientFactory httpClientFactory
            , IConfiguration configuration
              , IHttpContextAccessor httpContextAccessor):base(httpClientFactory, configuration,httpContextAccessor)
        {
           
        }


        public async Task<ApiResult<List<SlideVm>>> GetAll()
        {
            return await GetAsync<ApiResult<List<SlideVm>>>("/api/languages");

        }
    }
}
