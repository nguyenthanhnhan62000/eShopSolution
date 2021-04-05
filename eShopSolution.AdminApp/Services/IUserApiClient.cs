using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<String>> Authenticate(LoginRequest request);

        Task<ApiResult<PageResult<UserVm>>> GetUserPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(RegisterRequest request);

        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
        Task<ApiResult<UserVm>> GetById(Guid id);

    }
}
