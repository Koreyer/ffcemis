using FFC01.Foundation.Data;
using FFC02.EntityModels.E01.AppIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E02.AddressBusiness
{
    public class Address:Data<Guid>
    {
        public AppUser   AppUser { get; set; }
        public string? Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string? Region { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string? DetailedAddress { get; set; }
    }
}
