using FFC01.Foundation.DataHelpers;
using FFC02.EntityModels.E02.AddressBusiness;
using FFC05.ModelsDTO.D02.AddressBusinss;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace FFC07.WebApi.Controllers.C02.AddressManageBusiness
{
    [Area("AddressManageBusiness")]
    [Route("[area]/[controller]/[action]")]
    public class AddressManageController : BaseController<Guid, Address, AddressDTO>
    {
        public AddressManageController(IApiService<Guid, Address, AddressDTO> apiService) : base(apiService)
        {
            SetIncludeExpressions(x => x.AppUser);
        }
        public override async Task<Result> AddAsync(AddressDTO apiEntity)
        {
            apiEntity.AppUserId = AppUserId;
            return  await base.AddAsync(apiEntity);
        }


        [HttpGet]
        public async Task<ResultData<AddressDTO>> GetMyAddress()
        {
            return await ApiService.GetAllAsync(x => x.AppUser.Id == Guid.Parse(AppUserId));
        }
    }
}
