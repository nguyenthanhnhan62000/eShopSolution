using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.System;
using eShopsolution.Viewmodels.System.Languages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application_.System.Languages
{
    public interface ILanguageService
    {
        Task<ApiResult<List<LanguageVm>>> GetAll();

       


    }
}
