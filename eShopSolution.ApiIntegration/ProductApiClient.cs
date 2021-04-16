﻿using eShopsolution.Viewmodels.Catalog.Products;
using eShopsolution.Viewmodels.Comons;
using eShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ProductApiClient(IHttpClientFactory httpClientFactory
            , IConfiguration configuration
              , IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }


        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;

                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");
            requestContent.Add(new StringContent(request.Details.ToString()), "details");
            requestContent.Add(new StringContent(request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent("vi"), "languageId");

            var response = await client.PostAsync($"/api/products/", requestContent);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> UpdateProduct(ProductUpdateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbNailImage != null)
            {
                byte[] data;

                using (var br = new BinaryReader(request.ThumbNailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbNailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnailImage", request.ThumbNailImage.FileName);
            }

            //requestContent.Add(new StringContent(request.Id.ToString()), "id");
            requestContent.Add(new StringContent(String.IsNullOrEmpty(request.Name)? "": request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");
            requestContent.Add(new StringContent(String.IsNullOrEmpty(request.Details) ? "" : request.Details.ToString()), "details");
            requestContent.Add(new StringContent(String.IsNullOrEmpty(request.Description) ? "" : request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(String.IsNullOrEmpty(request.SeoTitle) ? "" : request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(String.IsNullOrEmpty(request.SeoAlias) ? "" : request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent("vi"), "languageId");

            var response = await client.PutAsync($"/api/products/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;

        }
         
        public async Task<PageResult<ProductVm>> GetPaging(GetManagaProductPagingRequest request)
        {
            var data = await GetAsync<PageResult<ProductVm>>(
                "/api/products/paging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}" +
                $"&languageId={request.LanguageId}&categoryId={request.CategoryId}");

            return data;


        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PutAsync($"/api/products/{id}/categories", httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ProductVm> GetById(int id, string languageId)
        {
            var data = await GetAsync<ProductVm>($"/api/products/{id}/{languageId}");

            return data;
        }

        public async Task<List<ProductVm>> GetFeaturedProduct(string languageId, int take)
        {

            var data = await GetListAsync<ProductVm>($"/api/products/featured/{languageId}/{take}");

            return data;
        }

        public async Task<List<ProductVm>> GetLastedProduct(string languageId, int take)
        {
            var data = await GetListAsync<ProductVm>($"/api/products/latest/{languageId}/{take}");

            return data;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await Delete($"/api/products/"+id);
        }
    }
}
