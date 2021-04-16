using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopsolution.Viewmodels.Comons;
using eShopSolution.ApiIntegration;
using eShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _ProductApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient ProductApiClient
            , IConfiguration configuration
            , ICategoryApiClient categoryApiClient
            )
        {

            _configuration = configuration;
            _ProductApiClient = ProductApiClient;
            _categoryApiClient = categoryApiClient;
        }


        public async Task<IActionResult> Index(String keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManagaProductPagingRequest()
            {

                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                CategoryId = categoryId

            };
            var data = await _ProductApiClient.GetPaging(request);

            ViewBag.Keyword = keyword;

            var categories = await _categoryApiClient.GetAll(languageId);

            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            });

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {

            if (!ModelState.IsValid) return View(request);

            var result = await _ProductApiClient.CreateProduct(request);

            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", "Thêm sản phẩm thất bại");

            return View(request);
        }
  

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var roleAssignRequest = await GetCategoryAssignRequest(id);
            return View(roleAssignRequest);

        }
        [HttpPost]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _ProductApiClient.CategoryAssign(request.Id, request);


            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật danh mục thành công";

                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", result.Message);

            var roleAssignRequest = await GetCategoryAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);


            var productObj = await _ProductApiClient.GetById(id,languageId);
            var categories = await _categoryApiClient.GetAll(languageId);
            var CategoryAssignRequest = new CategoryAssignRequest();
            foreach (var role in categories)
            {
                CategoryAssignRequest.Catagories.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = productObj.Categories.Contains(role.Name)
                });
            }

            return CategoryAssignRequest;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var product = await _ProductApiClient.GetById(id,languageId);

            var editVm = new ProductUpdateRequest()
            {
                Id= product.Id,
                Description=product.Description,
                Details=product.Details,
                Name=product.Name,
                SeoAlias=product.SeoAlias,
                SeoDescription=product.SeoDescription,
                SeoTitle= product.SeoTitle
            };
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] ProductUpdateRequest request)
        {

            if (!ModelState.IsValid) return View(request);

            var result = await _ProductApiClient.UpdateProduct(request);

            if (result)
            {
                TempData["result"] = "cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", "cập nhật sản phẩm thất bại");

            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new ProductDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest request)
        {
            if (!ModelState.IsValid) return View();

            var result = await _ProductApiClient.DeleteProduct(request.Id);

            if (result)
            {
                TempData["result"] = "Xoá sản phẩm thành công ";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xoa khong thanh cong");

            return View(request);
        }

    }
}



