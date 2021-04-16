using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface IRoleApiClient
    {

        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}
