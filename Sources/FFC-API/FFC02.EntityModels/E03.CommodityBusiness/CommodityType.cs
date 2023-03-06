using FFC01.Foundation.Data;
using FFC01.Foundation.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E03.CommodityBusiness
{
    public class CommodityType:Data<Guid>
    {
        //public  CommodityType Parent { get; set; }
        public string ImageId { get; set; }

        public CommodityType()
        {
            Id = Guid.NewGuid();
            SortCode = FunctionHelpers.CreateSortCode("CommodityType");
        }
    }
}
