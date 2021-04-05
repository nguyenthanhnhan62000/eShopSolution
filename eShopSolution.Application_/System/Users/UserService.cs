﻿using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.System;
using eShopSolution.data_.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application_.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
    

        public UserService(UserManager<AppUser> userManager
            , SignInManager<AppUser> singInManager
            , RoleManager<AppRole> roleManager
          
            , IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = singInManager;
            _roleManager = roleManager;
        
            _config = config;
        }
        public async Task<ApiResult<String>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) return null; 

            var result = await _signInManager.PasswordSignInAsync(user,request.Password,request.RememberMe,true);

            if (!result.Succeeded)
            {
                return new ApiErrorResult<String>("Login is Incorrect");

            }
            var roles = await _userManager.GetRolesAsync(user);


            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role,String.Join(";",roles)),
                new Claim(ClaimTypes.Name,request.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);

            return new ApiSuccessResult<String>(tokenResult);

        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null)
            {
                return new ApiErrorResult<UserVm>("User không tồn tại");

            }

            var userVm = new UserVm()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Id = user.Id,
                Dob = user.Dob,
                LastName = user.LastName
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<PageResult<UserVm>>> getUserPaging(GetUserPagingRequest request)
        {
            var query =  _userManager.Users;
            if(!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) 
                || x.PhoneNumber.Contains(request.Keyword));
            }

            //3.paging

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName    = x.UserName,
                    FirstName = x.FirstName,
                    Id=x.Id,
                    LastName=x.LastName
                }).ToListAsync();
            ;

            //4.select and projection
            var pageResult =  new PageResult<UserVm>()
            {
                TotalRecord = totalRow,
                Items = data,

            };
            return new ApiSuccessResult<PageResult<UserVm>>(pageResult);
        }

        public async Task<ApiResult< bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user != null)
            {
               
                return new ApiErrorResult<bool>("User name đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }


             user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return  new ApiErrorResult<bool>("Đăng kí không thành công"); 

        }

        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {
        
            if (await _userManager.Users.AnyAsync(x=> x.Email== request.Email &&x.Id != id))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            };
            var user = await _userManager.FindByIdAsync(id.ToString());

            user.Dob = request.Dob;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
          
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Update không thành công");
        }
    }
}
