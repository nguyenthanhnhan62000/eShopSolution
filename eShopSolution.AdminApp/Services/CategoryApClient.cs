using eShopsolution.Viewmodels.Catalog.Categories;
using eShopsolution.Viewmodels.Comons;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class CategoryApClient : BaseApiClient,ICategoryApiClient
    {

        public CategoryApClient(IHttpClientFactory httpClientFactory
            , IConfiguration configuration
              , IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {

        }


        public async Task<List<CategoryVm>> GetAll(String languageId)
        {
            return await GetListAsync<CategoryVm>("/api/categories?languageId=" + languageId);

        }

    
    }
}
