using FFC01.Foundation.Data;
using FFC02.EntityModels.E01.AppIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E03.CommodityBusiness
{
    public class CommodityEvaluation:Data<Guid>
    {
        public int Rate { get; set; }
        public string Message { get; set; }
        public AppUser AppUser { get; set; }
        public Commodity Commodity { get; set; }
        public string? ImageList { get; set; }
    }
}
