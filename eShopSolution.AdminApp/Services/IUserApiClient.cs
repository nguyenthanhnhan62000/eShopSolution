﻿using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<String> Authenticate(LoginRequest request);

        Task<PageResult<UserVm>> GetUserPaging(GetUserPagingRequest request);
    }
}
