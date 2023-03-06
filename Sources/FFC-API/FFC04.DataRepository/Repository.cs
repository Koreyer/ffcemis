using FFC01.Foundation.Data;
using FFC01.Foundation.DataHelpers;
using FFC01.Foundation.Enum;
using FFC03.EntityModels.ORM;
using FFC04.DataRepository.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FFC04.DataRepository
{
    /// <summary>
    /// 针对仓储泛型接口 <see cref="IRepository{TEntity}" /> 的具体实现。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<T, TEntity> : IRepository<T,TEntity> where TEntity : class, IData<T>, new()
    {
        /// <summary>
        /// EF 中实现领域模型对象和关系数据库表对象映射关联上下文对象
        /// </summary>
        private readonly FFCContext _context;
        internal FFCContext NewsContext { get => _context; }
        public FFCContext Context { get => _context; }
        public Repository(FFCContext context)
        {
            _context = context;
        }
        public async Task<Result> AddAsync(TEntity ddo)
        {

            var result = new Result()
            {
                Message = "新增失败",
                ResultEnum = ResultEnum.操作失败,
                Code = 500

            };
            try
            {
                _context.Set<TEntity>().Add(ddo);
                await _context.SaveChangesAsync();
                result.Code = 200;
                result.Message = "新增成功";
                result.ResultEnum = ResultEnum.操作成功;
                return result;
            }
            catch
            {

                return result;
            }
        }

        public async Task<Result> AddTOherAsync<Tid,TOher>(TOher oher)
            where TOher : class, IData<Tid>, new()
        {
            var result = new Result()
            {
                Message = "新增失败",
                ResultEnum = ResultEnum.操作失败,
                Code = 500

            };
            try
            {
                _context.Set<TOher>().Add(oher);
                await _context.SaveChangesAsync();
                result.Code = 200;
                result.Message = "新增成功";
                result.ResultEnum = ResultEnum.操作成功;
                return result;
            }
            catch
            {

                return result;
            }
        }

        public async Task<int> CountAsync() => await _context.Set<TEntity>()!.CountAsync();

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>()!.Where(predicate).CountAsync();

        public async Task<Result> DeleteAsync(T id)
        {
            var result = new Result()
            {
                Message = "删除失败",
                ResultEnum = ResultEnum.操作失败 

            };
            var ddo = await _context!.Set<TEntity>()!.FindAsync(id);
            var trackingBo = _context.ChangeTracker.Entries<TEntity>().AsQueryable().Where(x => x.Entity.Id.Equals(ddo.Id)).FirstOrDefault();
            trackingBo.State = EntityState.Deleted;
            if (ddo == null)
                return result;
            else
            {
                try
                {
                    _context.Set<TEntity>().Remove(ddo);
                    await _context.SaveChangesAsync();
                    result.Message = "删除成功";
                    result.ResultEnum = ResultEnum.操作成功;
                    result.Code = 200;
                    return result;
                }
                catch
                {
                    return result;
                }
            }
        }

        public async Task<Result> DeleteRangeByIdAsync(List<T> ids)
        {
            var result = new Result();
            var bos = new List<TEntity>();
            try
            {
                if(ids != null)
                {
                    foreach (var item in ids)
                    {
                        var bo = await _context!.Set<TEntity>()!.FindAsync(item);
                        var trackingBo = _context.ChangeTracker.Entries<TEntity>().AsQueryable().Where(x => x.Entity.Id.Equals(bo.Id)).FirstOrDefault();
                        trackingBo.State = EntityState.Deleted;
                        if (bo != null)
                        {
                            bos.Add(bo);
                        }
                    }
                    _context.RemoveRange(bos);
                    await _context.SaveChangesAsync();
                    result.ResultEnum = ResultEnum.操作成功;
                    result.Message = "删除成功";
                }
            }
            catch (Exception ex)
            {

                result.ResultEnum = ResultEnum.操作失败;
                result.Message = "删除失败";
            }
            return result;
        }

        public async Task<Result> DeleteRangeAsync(List<TEntity> bos)
        {
            var result = new Result();
            try
            {
                if(bos != null)
                {
                    foreach (var bo in bos)
                    {
                        var trackingBo = _context.ChangeTracker.Entries<TEntity>().AsQueryable().Where(x => x.Entity.Id.Equals(bo.Id)).FirstOrDefault();
                        trackingBo.State = EntityState.Deleted;
                    }
                    _context.RemoveRange(bos);
                    await _context.SaveChangesAsync();
                    result.ResultEnum = ResultEnum.操作成功;
                    result.Message = "删除成功";
                }
            }
            catch (Exception ex )
            {
                result.ResultEnum = ResultEnum.操作失败;
                result.Message = "删除失败";
                throw;
            }
            return result;
        }

        public async Task<ResultData<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> dbSet = _context!.Set<TEntity>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<T,TEntity>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
            var data = await dbSet!.ToListAsync();
            var result = new ResultData<TEntity>() { Datas = data, TotalCount = data.Count };
            return result!;
        }

        public async Task<ResultData<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> dbSet = _context!.Set<TEntity>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<T,TEntity>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
            var data = await dbSet!.Where(predicate).ToListAsync();
            var result = new ResultData<TEntity>() { Datas = data, TotalCount = data.Count };
            return result!;
        }

        public async Task<TEntity> GetAsync(T id)
        {
            IQueryable<TEntity> dbSet = _context!.Set<TEntity>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<T,TEntity>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
            var result = await dbSet!.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return result!;
        }

        public async Task<Other> GetOtherByIdAsync<Tid,Other>(Tid id)
            where Other : class, IData<Tid>, new()
        {
            IQueryable<Other> dbSet = _context!.Set<Other>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<Tid, Other>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
            var result = await dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return result!;
        }

        public async Task<Other> GetOtherAsync<Tid, Other>(Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new()
        {
            IQueryable<Other> dbSet = _context!.Set<Other>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<Tid, Other>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
            var result = await dbSet.FirstOrDefaultAsync(predicate);
            return result!;
        }
        public Other GetOther<Tid,Other>(Tid id)
            where Other : class, IData<Tid>, new()
        {
            var result = _context.Set<Other>().Find(id);
            return result!;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> dbSet = _context!.Set<TEntity>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<T,TEntity>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }

            var result = await dbSet!.FirstOrDefaultAsync(predicate);
            return result!;
        }
        /// <summary>
        /// 根据查询条件，获取仓储中满足条件，并且按照给定批次（页码）、批量（每页条数）返回的领域对象集合
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <param name="start">起始索引</param>
        /// <param name="length">数量</param>
        /// <returns></returns>
        public async Task<ResultData<TEntity>> GetBySelectAsync(int pageNum, int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> dbSet = _context!.Set<TEntity>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<T,TEntity>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
            if(pageNum == 0)
            {
                pageNum = 1;
            }
            if (predicate != null)
            {
                
                if (pageSize < 1)
                    pageSize = 100;
                var datas = await dbSet.Where(predicate).Skip((pageNum - 1)*pageSize).Take(pageSize).ToListAsync();
                return new ResultData<TEntity>() { TotalCount = datas.Count, Datas = datas };
            }
            else
            {
                var total = await dbSet!.CountAsync();
                if (pageSize < 1)
                    pageSize = 100;
                var datas = await dbSet.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
                return new ResultData<TEntity>() { TotalCount = total, Datas = datas };
            }

        }

        public async Task<ResultData<Other>> GetOtherBySelectAsync<Tid, Other>(int pageNum, int pageSize, Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new()
        {
            IQueryable<Other> dbSet = _context!.Set<Other>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<Tid, Other>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
            if (pageNum == 0)
            {
                pageNum = 1;
            }
            if (predicate != null)
            {

                if (pageSize < 1)
                    pageSize = 100;
                var datas = await dbSet.Where(predicate).Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
                return new ResultData<Other>() { TotalCount = datas.Count, Datas = datas };
            }
            else
            {
                var total = await dbSet!.CountAsync();
                if (pageSize < 1)
                    pageSize = 100;
                var datas = await dbSet.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
                return new ResultData<Other>() { TotalCount = total, Datas = datas };
            }
        }
        public async Task<ResultData<Other>> GetAllOther<Tid,Other>(Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new()
        {

            IQueryable<Other> dbSet = _context!.Set<Other>();
            var includePropertyExpressionCollection = ModelRelation.GetIncludeExpression<Tid,Other>();
            if (includePropertyExpressionCollection != null)
            {
                foreach (var includePropertyExpression in includePropertyExpressionCollection)
                {
                    dbSet = dbSet.Include(includePropertyExpression);
                }
            }
            var result = new ResultData<Other>();
            if (predicate != null)
            {
                result = new ResultData<Other>() { Datas = await dbSet!.Where(predicate).ToListAsync(), TotalCount = await dbSet!.CountAsync() };
            }
            else
            {
                result = new ResultData<Other>() { Datas = await dbSet!.ToListAsync(), TotalCount = await dbSet!.CountAsync() };
            }

            return result;
        }

        public async Task<bool> HasAsync(T id) => await _context.Set<TEntity>()!.AnyAsync(x => x.Id.Equals(id));

        public async Task<Result> UpdateAsync(TEntity ddo)
        {
            var result = new Result()
            {
                Message = "这条Id没有数据",
                ResultEnum = ResultEnum.操作失败,
                Code = 400

            };
            if (!await _context.Set<TEntity>().AnyAsync(x => x.Id.Equals(ddo.Id)))
                return result;
            else
            {
                try
                {
                    if (_context.ChangeTracker.Entries<TEntity>().Any(x => x.Entity.Id.Equals(ddo.Id)))
                    {
                        var trackingBo = _context.ChangeTracker.Entries<TEntity>().AsQueryable().Where(x => x.Entity.Id.Equals(ddo.Id)).FirstOrDefault();
                        trackingBo.State = EntityState.Detached;
                    }
                    _context.Set<TEntity>().Update(ddo);
                    await _context.SaveChangesAsync();
                    result.Message = "更新成功";
                    result.ResultEnum = ResultEnum.操作成功;
                    result.Code = 200;
                    return result;
                }
                catch
                {
                    result.Message = "更新失败";
                    result.Code = 500;
                    return result;
                }
            }
        }
        public async Task<Result> UpdateTOherAsync<Tid,TOher>(TOher oher)
            where TOher : class, IData<Tid>, new()
        {
            var result = new Result()
            {
                Message = "这条Id没有数据",
                ResultEnum = ResultEnum.操作失败,
                Code = 400

            };
            if (!await _context.Set<TOher>().AnyAsync(x => x.Id.Equals(oher.Id)))
                return result;
            else
            {
                try
                {
                    if (_context.ChangeTracker.Entries<TOher>().Any(x => x.Entity.Id.Equals(oher.Id)))
                    {
                        var trackingBo = _context.ChangeTracker.Entries<TOher>().AsQueryable().Where(x => x.Entity.Id.Equals(oher.Id)).FirstOrDefault();
                        trackingBo.State = EntityState.Detached;
                    }
                    _context.Set<TOher>().Update(oher);
                    await _context.SaveChangesAsync();
                    result.Message = "更新成功";
                    result.ResultEnum = ResultEnum.操作成功;
                    result.Code = 200;
                    return result;
                }
                catch
                {
                    result.Message = "更新失败";
                    result.Code = 500;
                    return result;
                }
            }
        }
    }
}
