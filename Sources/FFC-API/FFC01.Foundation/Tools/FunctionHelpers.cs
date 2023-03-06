using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC01.Foundation.Tools
{
    public static class FunctionHelpers
    {
        public static string CreateSortCode(string className)
        {
            //根据当前时间生成字符串用以排序
            return className + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff").Replace("-", "").Replace(" ", "").Replace("T", "").Replace(":", "");
        }

        public static string SubStringComma(string sub)
        {
            var result = sub;
            if (sub.Substring(sub.Length - 1, 1) == ",")
            {
                result = sub.Substring(0, sub.Length - 1);
            }
            return result;
        }

    }
}
