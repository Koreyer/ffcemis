using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFC01.Foundation.Data
{
    public interface IData<T>
    {
        public T? Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreateTime { get; init; }
        public bool SoftDeleted { get; set; }
        public string? Description { get; set; }
        public string? SortCode { get; set; }
    }
}
