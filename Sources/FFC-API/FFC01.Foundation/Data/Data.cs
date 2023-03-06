using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC01.Foundation.Data
{
    public class Data<T> : IData<T>
    {
        public T? Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreateTime { get; init; }
        public bool SoftDeleted { get; set; } = false;
        public string? Description { get; set; }
        public string? SortCode { get; set; }
        public Data()
        {
            CreateTime = DateTime.Now;
        }

    }
}
