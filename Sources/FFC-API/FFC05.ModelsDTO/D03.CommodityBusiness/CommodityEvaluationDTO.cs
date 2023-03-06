using FFC01.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D03.CommodityBusiness
{
    public class CommodityEvaluationDTO:Data<Guid>
    {
        public int Rate { get; set; }
        public string Message { get; set; }
        public string AppUserId { get; set; }
        public string? AppUserName { get; set; }
        public string? AppUserAvatar { get; set; }
        public string CommodityId { get; set; }
        public string? CommodityName { get; set; }
        public string? ImageList { get; set; }
    }
}
