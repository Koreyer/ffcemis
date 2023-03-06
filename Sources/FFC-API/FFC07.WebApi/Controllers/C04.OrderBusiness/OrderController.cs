using FFC01.Foundation.DataHelpers;
using FFC01.Foundation.Tools;
using FFC02.EntityModels.E03.CommodityBusiness;
using FFC02.EntityModels.E04.OrderBusiness;
using FFC05.ModelsDTO.D03.CommodityBusiness;
using FFC05.ModelsDTO.D04.OrderBusiness;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace FFC07.WebApi.Controllers.C04.OrderBusiness
{
    [Area("OrderBusiness")]
    [Route("[area]/[controller]/[action]")]
    public class OrderController : BaseController<Guid, Order, OrderDTO>
    {
        public OrderController(IApiService<Guid, Order, OrderDTO> apiService) : base(apiService)
        {
            SetIncludeExpressions(x => x.Address,x=>x.Commodity);
        }
        public override async Task<Result> AddAsync(OrderDTO apiEntity)
        {

            apiEntity.OrderNumber = DateTime.Now.Ticks.ToString();
            apiEntity.CommodityCount = apiEntity.CommodityCount == 0 ? 1 : apiEntity.CommodityCount;
            var commodity = await ApiService.GetOtherAsync<Guid,Commodity,CommodityDTO>(Guid.Parse(apiEntity.CommodityId));
            apiEntity.TotalPrice = apiEntity.CommodityCount * commodity.Price;
            return await base.AddAsync(apiEntity);
        }
        public override async Task<ResultData<OrderDTO>> GetBySelectAsync([FromQuery] SelectParameter selectParameter)
        {
            return await ApiService.GetBySelectAsync(selectParameter.PageNum, selectParameter.PageSize, x => x.Address.AppUser.Id == Guid.Parse(AppUserId) && x.Commodity.Name.Contains(selectParameter.SearchValue));
        }
        [HttpGet]
        public async Task<ResultData<OrderDTO>> GetOrderByMySelfAsync(int pageNum,int pageSize,int payType)
        {
            var result = new ResultData<OrderDTO>();
            switch (payType)
            {
                case 0:
                    result =  await ApiService.GetBySelectAsync(pageNum, pageSize, x => x.Address.AppUser.Id == Guid.Parse(AppUserId)&&x.OrderStatus == FFC01.Foundation.Enum.OrderStatusEnum.待付款);
                    break;
                case 1:
                    result = await ApiService.GetBySelectAsync(pageNum, pageSize, x => x.Address.AppUser.Id == Guid.Parse(AppUserId) && x.OrderStatus == FFC01.Foundation.Enum.OrderStatusEnum.待发货);
                    break;
                case 2:
                    result = await ApiService.GetBySelectAsync(pageNum, pageSize, x => x.Address.AppUser.Id == Guid.Parse(AppUserId) && x.OrderStatus == FFC01.Foundation.Enum.OrderStatusEnum.待收货);
                    break;
                case 3:
                    result = await ApiService.GetBySelectAsync(pageNum, pageSize, x => x.Address.AppUser.Id == Guid.Parse(AppUserId) && x.OrderStatus == FFC01.Foundation.Enum.OrderStatusEnum.已收货);
                    break;
                case 4:
                    result = await ApiService.GetBySelectAsync(pageNum, pageSize, x => x.Address.AppUser.Id == Guid.Parse(AppUserId));
                    break;
            }
            return result;
        }

        public async override Task<Result> UpdateAsync(OrderDTO apiEntity)
        {
            var order = await ApiService.GetOneByIdAsync(apiEntity.Id);
            if(order == null)
            {
                return new Result
                {
                    Message = "订单不存在"
                };
            }
            switch (apiEntity.OrderStatus)
            {
                case FFC01.Foundation.Enum.OrderStatusEnum.待发货:
                    order.PaymentTime = DateTime.Now;
                    break;
                case FFC01.Foundation.Enum.OrderStatusEnum.待收货:
                    order.DeliveryTime = DateTime.Now;
                    break;
                case FFC01.Foundation.Enum.OrderStatusEnum.已收货:
                    var com = await ApiService.GetOtherAsync<Guid, Commodity, CommodityDTO>(Guid.Parse(apiEntity.CommodityId));
                    com.SellNum += 1;
                    await ApiService.UpdateTOherAsync<Guid, Commodity, CommodityDTO>(com,x=>x.CommodityType);
                    order.ReceivingTime = DateTime.Now;

                    break;
                default:
                    break;
            }
            order.OrderStatus = apiEntity.OrderStatus;
            return await base.UpdateAsync(order);
        }

    }
}
