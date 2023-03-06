using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E02.AddressBusiness;
using FFC02.EntityModels.E03.CommodityBusiness;
using FFC02.EntityModels.E04.OrderBusiness;
using FFC02.EntityModels.E05.ShoppingCarBusiness;
using FFC02.EntityModels.E06.MessageManagementBusiness;
using FFC02.EntityModels.E07.SystemBusiness;
using FFC04.DataRepository;
using FFC05.ModelsDTO.D01.AppIdentity;
using FFC05.ModelsDTO.D02.AddressBusinss;
using FFC05.ModelsDTO.D03.CommodityBusiness;
using FFC05.ModelsDTO.D04.OrderBusiness;
using FFC05.ModelsDTO.D05.ShoppingCarBusiness;
using FFC05.ModelsDTO.D06.MessageManagementBusiness;
using FFC05.ModelsDTO.D07.SystemBusiness;
using FFC06.DTOServices;
using FFC06.DTOServices.Helpers;

namespace FFC07.WebApi.Helpers
{
    public static class InjectionHelp
    {
        public static void Injection(this IServiceCollection services)
        {
            #region 用户类
            services.AddScoped<IRepository<Guid,AppUser>,Repository<Guid,AppUser>>();
            services.AddScoped<IApiService<Guid, AppUser, AppUserDTO>, ApiService<Guid, AppUser, AppUserDTO>>();
            services.AddScoped<IMapperHelp<Guid, AppUser, AppUserDTO>, MapperHelp<Guid, AppUser, AppUserDTO>>();
            #endregion


            #region 商品类型
            services.AddScoped<IRepository<Guid, CommodityType>, Repository<Guid, CommodityType>>();
            services.AddScoped<IApiService<Guid, CommodityType, CommodityTypeDTO>, ApiService<Guid, CommodityType, CommodityTypeDTO>>();
            services.AddScoped<IMapperHelp<Guid, CommodityType, CommodityTypeDTO>, MapperHelp<Guid, CommodityType, CommodityTypeDTO>>();
            #endregion

            #region 商品信息
            services.AddScoped<IRepository<Guid, Commodity>, Repository<Guid, Commodity>>();
            services.AddScoped<IApiService<Guid, Commodity, CommodityDTO>, ApiService<Guid, Commodity, CommodityDTO>>();
            services.AddScoped<IMapperHelp<Guid, Commodity, CommodityDTO>, MapperHelp<Guid, Commodity, CommodityDTO>>();
            #endregion

            #region 地址信息
            services.AddScoped<IRepository<Guid, Address>, Repository<Guid, Address>>();
            services.AddScoped<IApiService<Guid, Address, AddressDTO>, ApiService<Guid, Address, AddressDTO>>();
            services.AddScoped<IMapperHelp<Guid, Address, AddressDTO>, MapperHelp<Guid, Address, AddressDTO>>();
            #endregion

            #region 购物车
            services.AddScoped<IRepository<Guid, ShoppingCar>, Repository<Guid, ShoppingCar>>();
            services.AddScoped<IApiService<Guid, ShoppingCar, ShoppingCarDTO>, ApiService<Guid, ShoppingCar, ShoppingCarDTO>>();
            services.AddScoped<IMapperHelp<Guid, ShoppingCar, ShoppingCarDTO>, MapperHelp<Guid, ShoppingCar, ShoppingCarDTO>>();
            services.AddScoped<IRepository<Guid, ShoppingCarWithItem>, Repository<Guid, ShoppingCarWithItem>>();
            services.AddScoped<IApiService<Guid, ShoppingCarWithItem, ShoppingCarWithItemDTO>, ApiService<Guid, ShoppingCarWithItem, ShoppingCarWithItemDTO>>();
            services.AddScoped<IMapperHelp<Guid, ShoppingCarWithItem, ShoppingCarWithItemDTO>, MapperHelp<Guid, ShoppingCarWithItem, ShoppingCarWithItemDTO>>();
            #endregion
            #region 轮播图
            services.AddScoped<IRepository<Guid, HomePageCarousel>, Repository<Guid, HomePageCarousel>>();
            services.AddScoped<IApiService<Guid, HomePageCarousel, HomePageCarouselDTO>, ApiService<Guid, HomePageCarousel, HomePageCarouselDTO>>();
            services.AddScoped<IMapperHelp<Guid, HomePageCarousel, HomePageCarouselDTO>, MapperHelp<Guid, HomePageCarousel, HomePageCarouselDTO>>();
            #endregion
            #region 订单
            services.AddScoped<IRepository<Guid, Order>, Repository<Guid, Order>>();
            services.AddScoped<IApiService<Guid, Order, OrderDTO>, ApiService<Guid, Order, OrderDTO>>();
            services.AddScoped<IMapperHelp<Guid, Order, OrderDTO>, MapperHelp<Guid, Order, OrderDTO>>();
            #endregion
            #region 消息
            services.AddScoped<IRepository<Guid, UserMessage>, Repository<Guid, UserMessage>>();
            services.AddScoped<IApiService<Guid, UserMessage, UserMessageDTO>, ApiService<Guid, UserMessage, UserMessageDTO>>();
            services.AddScoped<IMapperHelp<Guid, UserMessage, UserMessageDTO>, MapperHelp<Guid, UserMessage, UserMessageDTO>>();

            services.AddScoped<IRepository<Guid, MessageItem>, Repository<Guid, MessageItem>>();
            services.AddScoped<IApiService<Guid, MessageItem, MessageItemDTO>, ApiService<Guid, MessageItem, MessageItemDTO>>();
            services.AddScoped<IMapperHelp<Guid, MessageItem, MessageItemDTO>, MapperHelp<Guid, MessageItem, MessageItemDTO>>();
            #endregion
            #region 商品评价
            services.AddScoped<IRepository<Guid, CommodityEvaluation>, Repository<Guid, CommodityEvaluation>>();
            services.AddScoped<IApiService<Guid, CommodityEvaluation, CommodityEvaluationDTO>, ApiService<Guid, CommodityEvaluation, CommodityEvaluationDTO>>();
            services.AddScoped<IMapperHelp<Guid, CommodityEvaluation, CommodityEvaluationDTO>, MapperHelp<Guid, CommodityEvaluation, CommodityEvaluationDTO>>();
            #endregion


        }
    }
}
