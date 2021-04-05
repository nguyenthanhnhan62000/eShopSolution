using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application_.System.Users
{
    public   interface IUserService
    {
        Task<ApiResult<String>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> Update(Guid id,UserUpdateRequest request);

        Task<ApiResult<PageResult<UserVm>>> getUserPaging(GetUserPagingRequest request );
        Task<ApiResult<UserVm>> GetById(Guid id );
    }
}
