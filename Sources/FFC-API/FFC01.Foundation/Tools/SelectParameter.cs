using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC01.Foundation.Tools
{
    public class SelectParameter
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string? SearchName { get; set; }
        public string? SearchValue { get; set; }
        public string? SortOrder { get; set; }
        public bool IsDesc { get; set; }
        public string? extendValue1 { get; set; }
        public string? extndValue2 { get; set; }

    }
}
