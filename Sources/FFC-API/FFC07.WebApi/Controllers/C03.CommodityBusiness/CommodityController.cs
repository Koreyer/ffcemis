using FFC01.Foundation.DataHelpers;
using FFC01.Foundation.Tools;
using FFC02.EntityModels.E03.CommodityBusiness;
using FFC05.ModelsDTO.D03.CommodityBusiness;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace FFC07.WebApi.Controllers.C03.CommodityBusiness
{
    [Area("CommodityBusiness")]
    [Route("[area]/[controller]/[action]")]
    public class CommodityController : BaseController<Guid, Commodity, CommodityDTO>
    {
        public CommodityController(IApiService<Guid, Commodity, CommodityDTO> apiService) : base(apiService)
        {
            SetIncludeExpressions(x => x.CommodityType);
        }

        [HttpGet]
        public async Task<ResultData<CommodityDTO>> GetCommodityByType([FromQuery] SelectParameter selectParmeter)
        {
            return await ApiService.GetBySelectAsync(selectParmeter.PageNum, selectParmeter.PageSize, x => x.CommodityType.Name.Contains(selectParmeter.SearchValue));
        }
        public override Task<Result> AddAsync(CommodityDTO apiEntity)
        {
            apiEntity.UserBusinessId = AppUserId;
            apiEntity.Carousels = FunctionHelpers.SubStringComma(apiEntity.Carousels);
            apiEntity.DescriptionImageUrl = FunctionHelpers.SubStringComma(apiEntity.DescriptionImageUrl);
            return base.AddAsync(apiEntity);
        }
        public override Task<Result> UpdateAsync(CommodityDTO apiEntity)
        {
            apiEntity.UserBusinessId = AppUserId;
            apiEntity.Carousels = FunctionHelpers.SubStringComma(apiEntity.Carousels);
            apiEntity.DescriptionImageUrl = FunctionHelpers.SubStringComma(apiEntity.DescriptionImageUrl);
            return base.UpdateAsync(apiEntity);
        }

        [HttpGet]
        public async Task<Result> AddCommodityViewNum(string id)
        {
            var commodity = await ApiService.GetOneByIdAsync(Guid.Parse(id));
            commodity.ViewNum += 1;
            await ApiService.UpdateAsync(commodity);
            return null;
        }

        [HttpGet]
        public async Task<List<CommodityResultDTO>> GetCascadeCommodity()
        {
            var commodities = await ApiService.GetAllAsync();
            var comType = await ApiService.GetAllOtherAsync<Guid, CommodityType, CommodityTypeDTO>(null);
            var comTypeGro = commodities.Datas.GroupBy(x => x.CommodityTypeId);
            var result = new List<CommodityResultDTO>();
            foreach (var item in comTypeGro)
            {
                var res = new CommodityResultDTO
                {
                    Value = item.Key,
                    Label = comType.Datas.FirstOrDefault(x => x.Id == Guid.Parse(item.Key)).Name,
                    Children = new List<CommodityResultDTO>()
                };
                foreach (var commodity in item)
                {
                    res.Children.Add(new CommodityResultDTO
                    {
                        Value = commodity.Id.ToString(),
                        Label = commodity.Name
                    });
                }
                result.Add(res);
            }
            return result;
        }
    }
}
