using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D00.Common
{
    public class AdminRoutesDTO
    {
        public string Name { get; set; }
        public bool AlwaysShow { get; set; }
        public List<AdminRoutesDTO> Children { get; set; }
        public string Component { get; set; }
        public bool Hidden { get; set; } = false;
        public RouteMetaDTO Meta { get; set; } = new RouteMetaDTO();
        public string Path { get; set; }
        public string Redirect { get; set; }

    }
}
