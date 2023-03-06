using FFC01.Foundation.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC01.Foundation.DataHelpers
{
    public class Result
    {
        public ResultEnum ResultEnum { get; set; }
        public string Message { get; set; } = "操作成功";
        public int Code { get; set; } = 200;
        public object? Data { get; set; }
    }
}
