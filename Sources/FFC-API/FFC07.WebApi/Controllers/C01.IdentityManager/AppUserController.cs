using FFC01.Foundation.DataHelpers;
using FFC02.EntityModels.E01.AppIdentity;
using FFC05.ModelsDTO.D01.AppIdentity;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace FFC07.WebApi.Controllers.C01.IdentityManager
{
    [Area("IdentityManager")]
    [Route("[area]/[controller]/[action]")]
    public class AppUserController:BaseController<Guid,AppUser,AppUserDTO>
    {
        private readonly UserManager<AppUser> userManager;
        public AppUserController(IApiService<Guid, AppUser, AppUserDTO> apiService, UserManager<AppUser> userManager) : base(apiService)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<Result> UserGetSelf()
        {
            var user = await base.GetByIdAsync(Guid.Parse(AppUserId));
            var result = new Result
            {
                Data = user
            };
            return result;
        }

        //public async override Task<Result> UpdateAsync(AppUserDTO apiEntity)
        //{
        //    try
        //    {
        //        var user = await ApiService.Mapper.MapToEntityAsync(apiEntity, x => x.ShoppingCar);
                

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    return null;
        //}
    }
}
