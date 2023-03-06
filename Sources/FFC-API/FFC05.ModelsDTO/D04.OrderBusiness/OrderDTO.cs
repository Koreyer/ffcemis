using FFC01.Foundation.Data;
using FFC01.Foundation.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D04.OrderBusiness
{
    public class OrderDTO : Data<Guid>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string? OrderNumber { get; set; }
        //public string AppUserId { get; set; }
        //public string AppUserName { get; set; }
        public OrderStatusEnum OrderStatus  { get; set; }
        public string? OrderStatusName { get; set; }
        /// <summary>
        /// 收货地址ID
        /// </summary>
        public string? AddressId { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string? AddressLocation { get; set; }
        /// <summary>
        /// 收获手机号
        /// </summary>
        public string? AddressPhone { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string? AddressName { get; set; }
        ///// <summary>
        ///// 收货人Id
        ///// </summary>
        //public string AddressAppUserId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public string? CommodityId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string? CommodityName { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public double CommodityPrice { get; set; }
        /// <summary>
        /// 商品首页图
        /// </summary>
        public string? CommodityImageUrl { get; set; }
        /// <summary>
        /// 商品规格
        /// </summary>
        public string? CommoditySpec { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int CommodityCount { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime DeliveryTime { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime PaymentTime { get; set; }
        /// <summary>
        /// 收货时间
        /// </summary>
        public DateTime ReceivingTime { get; set; }

    }
}
