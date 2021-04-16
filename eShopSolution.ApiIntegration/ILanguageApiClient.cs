using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.Untilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface ILanguageApiClient
    {

        Task<ApiResult<List<SlideVm>>> GetAll();

    }
}
