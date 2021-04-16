using eShopsolution.Viewmodels.Catalog.Categories;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolutionWebApp.Models
{
    public class ProductCategoryViewModel
    {
        public CategoryVm category { get; set; }

        public PageResult<ProductVm> products { get; set; }


    }
}
