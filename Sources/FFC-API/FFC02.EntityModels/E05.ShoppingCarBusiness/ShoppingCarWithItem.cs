using FFC01.Foundation.Data;
using FFC02.EntityModels.E03.CommodityBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E05.ShoppingCarBusiness
{
    public class ShoppingCarWithItem:Data<Guid >
    {
        public ShoppingCar ShoppingCar { get; set; }
        public Commodity Commodity { get; set; }
        public int Count { get; set; }
    }
}
