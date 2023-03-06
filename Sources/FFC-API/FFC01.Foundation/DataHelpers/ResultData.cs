using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC01.Foundation.DataHelpers
{
    public class ResultData<TApiEntity>
    {
        public int TotalCount { get; set; }
        public List<TApiEntity> Datas { get; set; } = new List<TApiEntity> ();
        public int Code { get; set; } = 200;
    }
}
