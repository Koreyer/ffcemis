using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D00.Common
{
    public class RouteMetaDTO
    {
        public string Icon { get; set; }
        public string Link { get; set; }
        public bool NoCache { get; set; } = false;
        public string Title { get; set; }
    }
}
