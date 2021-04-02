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
        Task<String> Authencate(LoginRequest request);

        Task<bool> Register(RegisterRequest request);

        Task<PageResult<UserVm>> getUserPaging(GetUserPagingRequest request );
    }
}
