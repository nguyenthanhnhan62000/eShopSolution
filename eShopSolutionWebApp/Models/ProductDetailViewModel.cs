using eShopsolution.Viewmodels.Catalog.Categories;
using eShopsolution.Viewmodels.Catalog.ProductImages;
using eShopsolution.Viewmodels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolutionWebApp.Models
{
    public class ProductDetailViewModel
    {
        public CategoryVm Category { get; set; }
        public ProductVm Product { get; set; }

        public List<ProductVm> RelatedProduct { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }
    }
}
