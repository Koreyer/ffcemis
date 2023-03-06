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
    public class CommodityEvaluationController : BaseController<Guid, CommodityEvaluation, CommodityEvaluationDTO>
    {
        public CommodityEvaluationController(IApiService<Guid, CommodityEvaluation, CommodityEvaluationDTO> apiService) : base(apiService)
        {
            SetIncludeExpressions(x => x.AppUser, x => x.Commodity);
        }

        public async override Task<ResultData<CommodityEvaluationDTO>> GetBySelectAsync([FromQuery] SelectParameter selectParameter)
        {
            return await ApiService.GetBySelectAsync(selectParameter.PageNum, selectParameter.PageSize, x => x.Commodity.Id == Guid.Parse(selectParameter.SearchValue));

        }
        public async override Task<Result> AddAsync(CommodityEvaluationDTO apiEntity)
        {
            apiEntity.ImageList = FunctionHelpers.SubStringComma(apiEntity.ImageList);
            return await base.AddAsync(apiEntity);
        }
        /// <summary>
        /// 好评率
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetHighOpinionAsync(Guid id)
        {
            var res = await ApiService.GetAllAsync(x => x.Commodity.Id == id);
            if(res.TotalCount == 0)
            {
                return "0";
            }
            double dividend = res.Datas.Where(x=>x.Rate>3).Count();
            return ((dividend / res.TotalCount) * 100).ToString();
        }
    }
}
