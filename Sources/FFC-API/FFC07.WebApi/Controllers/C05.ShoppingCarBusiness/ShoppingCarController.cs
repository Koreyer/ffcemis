using FFC01.Foundation.DataHelpers;
using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E05.ShoppingCarBusiness;
using FFC05.ModelsDTO.D01.AppIdentity;
using FFC05.ModelsDTO.D05.ShoppingCarBusiness;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace FFC07.WebApi.Controllers.C05.ShoppingCarBusiness
{
    [Area("ShoppingCarBusiness")]
    [Route("[area]/[controller]/[action]")]
    public class ShoppingCarController : BaseController<Guid, ShoppingCar, ShoppingCarDTO>
    {
        public ShoppingCarController(IApiService<Guid, ShoppingCar, ShoppingCarDTO> apiService) : base(apiService)
        {
        }


        public override Task<Result> AddAsync(ShoppingCarDTO apiEntity)
        {
            var user = ApiService.GetOtherAsync<Guid, AppUser, AppUserDTO>(Guid.Parse(AppUserId));

            return base.AddAsync(apiEntity);
        }
    }
}
