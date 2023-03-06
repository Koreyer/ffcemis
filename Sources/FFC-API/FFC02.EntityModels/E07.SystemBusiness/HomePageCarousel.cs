using FFC01.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E07.SystemBusiness
{
    /// <summary>
    /// 首页轮播图
    /// </summary>
    public class HomePageCarousel:Data<Guid>
    {
        /// <summary>
        /// 点击图片的链接地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图片Id
        /// </summary>
        public string ImageId { get; set; }
    }
}
