using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D03.CommodityBusiness
{
    public class CommodityResultDTO
    {
        public string Value { get; set; }
        public string Label { get; set; }
        public List<CommodityResultDTO> Children { get; set; }

    }
}
