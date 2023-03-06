using FFC01.Foundation.DataHelpers;
using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E05.ShoppingCarBusiness;
using FFC05.ModelsDTO.D01.AppIdentity;
using FFC05.ModelsDTO.D05.ShoppingCarBusiness;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FFC07.WebApi.Controllers.C05.ShoppingCarBusiness
{
    [Area("ShoppingCarBusiness")]
    [Route("[area]/[controller]/[action]")]
    public class ShoppingCarWithItemController : BaseController<Guid, ShoppingCarWithItem, ShoppingCarWithItemDTO>
    {
        public ShoppingCarWithItemController(IApiService<Guid, ShoppingCarWithItem, ShoppingCarWithItemDTO> apiService) : base(apiService)
        {
            SetIncludeExpressions(x => x.ShoppingCar,x=>x.Commodity);
        }
        public override async Task<Result> AddAsync(ShoppingCarWithItemDTO apiEntity)
        {
            var shoppingCarId = (await ApiService.GetOtherAsync<Guid, AppUser, AppUserDTO>(Guid.Parse(AppUserId))).ShoppingCarId;
            apiEntity.ShoppingCarId = shoppingCarId;
            return await base.AddAsync(apiEntity);
        }

        [HttpGet]
        public async Task<ResultData<ShoppingCarWithItemDTO>> GetShoppingCarMySelfAsync()
        {
            var shoppingCarId = (await ApiService.GetOtherAsync<Guid, AppUser, AppUserDTO>(Guid.Parse(AppUserId))).ShoppingCarId;
            return await ApiService.GetAllAsync(x => x.ShoppingCar.Id == Guid.Parse(shoppingCarId));

        }
        
        [HttpDelete]
        public async Task<Result> CleanShoppingCar()
        {
            var shoppingCarId = (await ApiService.GetOtherAsync<Guid, AppUser, AppUserDTO>(Guid.Parse(AppUserId))).ShoppingCarId;
            var result = (await ApiService.GetAllAsync(x => x.ShoppingCar.Id == Guid.Parse(shoppingCarId))).Datas;
            
            return await ApiService.DeleteRangeAsync(result,x=>x.Commodity);
        }
    }
}
