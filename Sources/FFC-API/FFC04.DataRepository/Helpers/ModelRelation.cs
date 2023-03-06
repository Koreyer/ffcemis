using FFC01.Foundation.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FFC04.DataRepository.Helpers
{
    /// <summary>
    /// 领域模型数据处理的一些助理方法
    /// </summary>
    public static class ModelRelation
    {
        /// <summary>
        /// 提取包含属性的表达式
        /// </summary>
        /// <typeparam name="TDdo"></typeparam>
        /// <returns></returns>
        public static List<Expression<Func<TEntity, object>>> GetIncludeExpression<T,TEntity>() where TEntity : class, IData<T>, new()
        {
            var result = new List<Expression<Func<TEntity, object>>>();

            PropertyInfo[] ddoPropertyCollection = typeof(TEntity).GetProperties();
            foreach (var ddoProperty in ddoPropertyCollection)
            {
                var ddoPropertyTypeName = ddoProperty.PropertyType.Name;
                var ddoPropertyTypeFullName = ddoProperty.PropertyType.FullName;

                if (ddoPropertyTypeFullName!.Contains("FFC02.EntityModels"))
                {
                    ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "i");
                    var property = Expression.Property(parameter, ddoProperty.Name);
                    var lambda = Expression.Lambda<Func<TEntity, object>>(property, parameter);
                    result.Add(lambda);
                }

            }
            return result;
        }

        public static void SetInclude<T,TEntity>(IQueryable<TEntity> dbSet) where TEntity : class, IData<T>, new()
        {
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<T,TEntity>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
        }
    }
}
