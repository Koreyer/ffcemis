using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D03.CommodityBusiness
{
    public class CommodityTypeResultDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SortCode { get; set; }

        public List<CommodityTypeResultDTO> Children { get; set; } = new List<CommodityTypeResultDTO>();
    }
}
