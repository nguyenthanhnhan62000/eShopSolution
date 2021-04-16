using eShopsolution.Viewmodels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application_.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryVm>> GetAll(String languageId);
        Task<CategoryVm> GetById(String languageId,int id);
    }
}
