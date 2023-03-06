using FFC01.Foundation.Data;
using FFC01.Foundation.DataHelpers;
using FFC03.EntityModels.ORM;
using FFC04.DataRepository;
using FFC06.DTOServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FFC06.DTOServices
{
    public interface IApiService<T,TEntity, TApiEntity>
       where TEntity : class, IData<T>, new()
       where TApiEntity : class, IData<T>, new()
    {
        FFCContext Context { get; }
        IRepository<T,TEntity> Repository { get; }
        IMapperHelp<T,TEntity, TApiEntity> Mapper { get; }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        Task<Result> AddAsync(TApiEntity request, params Expression<Func<TEntity, object>>[] expressions);
        Task<Result> AddTOherAsync<Tid,TOher, TApiOher>(TApiOher request, params Expression<Func<TOher, object>>[] expressions) where TOher : class, IData<Tid>, new() where TApiOher : class, IData<Tid>, new();
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="request"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        Task<Result> UpdateAsync(TApiEntity request, params Expression<Func<TEntity, object>>[] expressions);
        Task<Result> UpdateTOherAsync<Tid,TOher, TApiOher>(TApiOher request, params Expression<Func<TOher, object>>[] expressions) where TOher : class, IData<Tid>, new() where TApiOher : class, IData<Tid>, new();
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> DeleteByIdAsync(T id);

        Task<Result> DeleteRangeByIdAsync(List<T> ids);
        Task<Result> DeleteRangeAsync(List<TApiEntity> bos, params Expression<Func<TEntity, object>>[] expressions);
        /// <summary>
        /// 根据id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TApiEntity> GetOneByIdAsync(T id);
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TApiEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TApiOther> GetOtherAsync<Tid,Other, TApiOther>(Tid id) where Other : class, IData<Tid>, new() where TApiOther : class, IData<Tid>, new();
        Task<TApiOther> GetOtherAsync<Tid, Other, TApiOther>(Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new() where TApiOther : class, IData<Tid>, new();
        Task<ResultData<TApiOther>> GetAllOtherAsync<Tid,Other, TApiOther>(Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new() where TApiOther : class, IData<Tid>, new();
        /// <summary>
        /// 获取一组数据
        /// </summary>
        /// <returns></returns>
        Task<ResultData<TApiEntity>> GetAllAsync();
        /// <summary>
        /// 根据条件获取一组数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<ResultData<TApiEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据查询条件获取一组数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<ResultData<TApiEntity>> GetBySelectAsync(int pageNum, int pageSize, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据查询条件获取一组数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<ResultData<TApiOther>> GetOtherBySelectAsync<Tid, Other, TApiOther>(int pageNum, int pageSize, Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new() where TApiOther : class, IData<Tid>, new();
        /// <summary>
        /// 根据id查找是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> HasAsync(T id);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
    }
}
