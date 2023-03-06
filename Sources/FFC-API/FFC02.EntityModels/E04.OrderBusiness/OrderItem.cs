using FFC01.Foundation.Data;
using FFC02.EntityModels.E03.CommodityBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E04.OrderBusiness
{
    public class OrderItem:Data<Guid>
    {
        public Order Order { get; set; }
        public Commodity Commodity { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count  { get; set; }
    }
}
