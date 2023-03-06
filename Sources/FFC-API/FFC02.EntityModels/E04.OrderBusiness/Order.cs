using FFC01.Foundation.Data;
using FFC01.Foundation.Enum;
using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E02.AddressBusiness;
using FFC02.EntityModels.E03.CommodityBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E04.OrderBusiness
{
    public class Order:Data<Guid>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        //public AppUser  AppUser { get; set; }
        public Address Address { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int CommodityCount { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public Commodity Commodity { get; set; }
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
