using AutoMapper;
using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E02.AddressBusiness;
using FFC02.EntityModels.E03.CommodityBusiness;
using FFC02.EntityModels.E04.OrderBusiness;
using FFC02.EntityModels.E05.ShoppingCarBusiness;
using FFC02.EntityModels.E06.MessageManagementBusiness;
using FFC02.EntityModels.E07.SystemBusiness;
using FFC05.ModelsDTO.D01.AppIdentity;
using FFC05.ModelsDTO.D02.AddressBusinss;
using FFC05.ModelsDTO.D03.CommodityBusiness;
using FFC05.ModelsDTO.D04.OrderBusiness;
using FFC05.ModelsDTO.D05.ShoppingCarBusiness;
using FFC05.ModelsDTO.D06.MessageManagementBusiness;
using FFC05.ModelsDTO.D07.SystemBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC06.DTOServices.Helpers
{
    public class MappeerProfile:Profile
    {
        public MappeerProfile()
        {
            CreateMap<AppUser, AppUserDTO>().ForMember(x => x.SexName, y => y.MapFrom(a => a.Sex));
            CreateMap<CommodityType, CommodityTypeDTO>();
            CreateMap<Commodity, CommodityDTO>();
            CreateMap<Order, OrderDTO>().ForMember(x => x.OrderStatusName, y => y.MapFrom(x => x.OrderStatus));
            CreateMap<Address, AddressDTO>();
            CreateMap<ShoppingCar, ShoppingCarDTO>();
            CreateMap<ShoppingCarWithItem, ShoppingCarWithItemDTO>();
            CreateMap<HomePageCarousel, HomePageCarouselDTO>();
            CreateMap<UserMessage, UserMessageDTO>();
            CreateMap<MessageItem, MessageItemDTO>();
            CreateMap<CommodityEvaluation, CommodityEvaluationDTO>();

        }
    }
}
