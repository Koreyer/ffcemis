using FFC01.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D04.OrderBusiness
{
    public class OrderItemDTO : Data<Guid>
    {
        public string OrderId { get; set; }
        public string OrderOrderNumber { get; set; }

        public string CommodityId { get; set; }
        public string CommodityName { get; set; }


        /// <summary>
        /// 商品价格
        /// </summary>
        public double CommodityPrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
