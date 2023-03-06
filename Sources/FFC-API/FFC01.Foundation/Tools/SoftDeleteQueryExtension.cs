using FFC01.Foundation.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FFC01.Foundation.Tools
{
    /// <summary>
    /// 软删除过滤器
    /// </summary>
    public static class SoftDeleteQueryExtension
    {
        /// <summary>
        /// 软删除过滤器
        /// </summary>
        /// <param name="entityData"></param>
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension).GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeof(Guid), entityData.ClrType);
            var filter = methodToCall.Invoke(null, new object[] { });
            entityData.SetQueryFilter((LambdaExpression)filter);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        private static LambdaExpression GetSoftDeleteFilter<T,TEntity>() where TEntity : Data<T>
        {
            Expression<Func<TEntity, bool>> filter = x => !x.SoftDeleted;
            return filter;
        }
    }

}
