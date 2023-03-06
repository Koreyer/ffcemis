using FFC01.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D05.ShoppingCarBusiness
{
    public class ShoppingCarWithItemDTO:Data<Guid>
    {
        public string? ShoppingCarId { get; set; }
        public string CommodityId { get; set; }
        public string? CommodityName { get; set; }
        public string? CommoditySpec { get; set; }
        public string? CommodityPrice { get; set; }
        public string? CommodityImageUrl { get; set; }

        public int Count { get; set; }
    }
}
