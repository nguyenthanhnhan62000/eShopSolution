using eShopsolution.Viewmodels.Catalog.Categories;
using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface ICategoryApiClient
    {

        Task<List<CategoryVm>> GetAll(String languageId);



    }
}
