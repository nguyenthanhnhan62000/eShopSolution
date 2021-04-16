using eShopsolution.Viewmodels.Catalog.Categories;
using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.Untilities.Slides;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class SlideApiClient : BaseApiClient, ISlideApiClient
    {

        public SlideApiClient(IHttpClientFactory httpClientFactory
            , IConfiguration configuration
              , IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {

        }

        public async Task<List<SlideVm>> GetAll()
        {
            return await GetListAsync<SlideVm>("/api/slide");
        }
    }
}
