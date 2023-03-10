using FFC01.Foundation.Data;
using FFC02.EntityModels.E01.AppIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E03.CommodityBusiness
{
    public class Commodity:Data<Guid>
    {
        public string UserBusinessId { get; set; }
        public CommodityType CommodityType { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public double OriginalPrice { get; set; }
        /// <summary>
        /// 首页图
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 商品轮播图
        /// </summary>
        public string Carousels { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int SellNum { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int ViewNum { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 详情内容的图片组
        /// </summary>
        public string DescriptionImageUrl { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string Producer { get; set; }
        /// <summary>
        /// 推荐理由
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string WarrantyTime { get; set; }
        /// <summary>
        /// 储存方式
        /// </summary>
        public string StorageMode { get; set; }

        public Commodity()
        {
            Id = Guid.NewGuid();
        }

    }
}
