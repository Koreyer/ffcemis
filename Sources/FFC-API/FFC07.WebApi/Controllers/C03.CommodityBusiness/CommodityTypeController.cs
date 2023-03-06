using FFC01.Foundation.DataHelpers;
using FFC02.EntityModels.E03.CommodityBusiness;
using FFC05.ModelsDTO.D03.CommodityBusiness;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace FFC07.WebApi.Controllers.C03.CommodityBusiness
{
    [Area("CommodityBusiness")]
    [Route("[area]/[controller]/[action]")]
    public class CommodityTypeController : BaseController<Guid, CommodityType, CommodityTypeDTO>
    {
        public CommodityTypeController(IApiService<Guid, CommodityType, CommodityTypeDTO> apiService) : base(apiService)
        {
        }


        //[HttpGet]
        //public  async Task<ResultData<CommodityTypeResultDTO>> GetCommodityTypeAsync()
        //{
            
        //    var data = await  base.GetAsync();
        //    var dataGroup = data.Datas.GroupBy(x => x.ParentId).Select(x => x).Where(x=>x.Key == Guid.Empty.ToString()).ToList();
        //    var result = new ResultData<CommodityTypeResultDTO>();
        //    foreach (var item in dataGroup)
        //    {
        //        var a = item;
        //        foreach (var commodityType in item)
        //        {
        //            var ctData = new CommodityTypeResultDTO
        //            {
        //                Id = commodityType.Id,
        //                SortCode = commodityType.SortCode,
        //                Name = commodityType.Name
        //            };
        //            var datas = data.Datas.Where(x => x.ParentId == commodityType.Id.ToString());
        //            foreach (var chiData in datas)
        //            {
        //                ctData.Children.Add(new CommodityTypeResultDTO
        //                {
        //                    Id = chiData.Id,
        //                    SortCode = chiData.SortCode,
        //                    Name = chiData.Name
        //                });
        //            }
        //            result.Datas.Add(ctData);
        //        }
        //    }
        //    result.TotalCount = data.Datas.Count;
        //    return  result; ;
        //}

        //public override async Task<Result> AddAsync(CommodityTypeDTO apiEntity)
        //{
        //    if(apiEntity.ParentId == null)
        //    {
        //        return await ApiService.AddAsync(apiEntity);
        //    }
        //    else
        //    {
        //        return await ApiService.AddAsync(apiEntity, x => x.Parent);
        //    }
            
        //}
    }
}
