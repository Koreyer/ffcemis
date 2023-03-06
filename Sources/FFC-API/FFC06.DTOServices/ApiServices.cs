using FFC01.Foundation.Data;
using FFC01.Foundation.DataHelpers;
using FFC03.EntityModels.ORM;
using FFC04.DataRepository;
using FFC04.DataRepository.Helpers;
using FFC06.DTOServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FFC06.DTOServices
{
    public class ApiService<T,TEntity, TApiEntity> : IApiService<T,TEntity, TApiEntity>
        where TEntity : class, IData<T>, new()
        where TApiEntity : class, IData<T>, new()
    {
        private readonly IRepository<T,TEntity> _repository;
        private readonly IMapperHelp<T,TEntity, TApiEntity> _mapper;
        public ApiService(IRepository<T,TEntity> repository, IMapperHelp<T,TEntity, TApiEntity> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public FFCContext Context { get => _repository.Context; }
        public IRepository<T,TEntity> Repository { get => _repository; }
        public IMapperHelp<T,TEntity, TApiEntity> Mapper { get => _mapper; }


        public virtual async Task<Result> AddAsync(TApiEntity request, params Expression<Func<TEntity, object>>[] expressions)
        {
            var bo = await _mapper.MapToEntityAsync(request, expressions);
            return await _repository.AddAsync(bo);
        }
        public async Task<Result> AddTOherAsync<Tid, TOher, TApiOher>(TApiOher request, params Expression<Func<TOher, object>>[] expressions) where TOher : class, IData<Tid>, new() where TApiOher : class, IData<Tid>, new()
        {
            var bo = await _mapper.MapToOtherEntityAsync<Tid, TOher, TApiOher>(request, expressions);
            bo.Id = request.Id;
            return await _repository.AddTOherAsync<Tid, TOher>(bo);
        }

        public async Task<Result> DeleteByIdAsync(T id) => await _repository.DeleteAsync(id);

        public async Task<Result> DeleteRangeByIdAsync(List<T> ids) => await _repository.DeleteRangeByIdAsync (ids);
        public async Task<Result> DeleteRangeAsync(List<TApiEntity> request, params Expression<Func<TEntity, object>>[] expressions)
        {
            var bos = await _mapper.MapToEntityListAsync(request, expressions);
            return await _repository.DeleteRangeAsync(bos);
        }
        public async Task<ResultData<TApiEntity>> GetAllAsync()
        {
            var ddos = await _repository.GetAllAsync();
            var dtos = _mapper.Mapper.Map<List<TApiEntity>>(ddos.Datas);
            return new ResultData<TApiEntity>()
            {
                Datas = dtos,
                TotalCount = ddos.TotalCount
            };
        }

        public async Task<ResultData<TApiEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var ddos = await _repository.GetAllAsync(predicate);
            var dtos = _mapper.Mapper.Map<List<TApiEntity>>(ddos.Datas);
            return new ResultData<TApiEntity>()
            {
                Datas = dtos,
                TotalCount = ddos.TotalCount
            };
        }

        public async Task<TApiEntity> GetOneByIdAsync(T id) => _mapper.Mapper.Map<TApiEntity>(await _repository.GetAsync(id));
        public async Task<TApiOther> GetOtherAsync<Tid,Other, TApiOther>(Tid id) where Other : class, IData<Tid>, new() where TApiOther : class, IData<Tid>, new() => _mapper.Mapper.Map<TApiOther>(await _repository.GetOtherByIdAsync<Tid,Other>(id));
        public async Task<TApiOther> GetOtherAsync<Tid, Other, TApiOther>(Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new() where TApiOther : class, IData<Tid>, new()
        {
            return _mapper.Mapper.Map<TApiOther>(await _repository.GetOtherAsync<Tid,Other>(predicate));
        }
        public async Task<TApiEntity> GetAsync(Expression<Func<TEntity, bool>> predicate) => _mapper.Mapper.Map<TApiEntity>(await _repository.GetAsync(predicate));

        public async Task<ResultData<TApiEntity>> GetBySelectAsync(int pageNum, int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            var ddos = await _repository.GetBySelectAsync(pageNum, pageSize, predicate);
            var dtos = _mapper.Mapper.Map<List<TApiEntity>>(ddos.Datas);
            return new ResultData<TApiEntity>()
            {
                Datas = dtos,
                TotalCount = ddos.TotalCount
            };
        }
        public async Task<ResultData<TApiOther>> GetOtherBySelectAsync<Tid, Other, TApiOther>(int pageNum, int pageSize, Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new() where TApiOther : class, IData<Tid>, new()
        {
            var ddos = await _repository.GetOtherBySelectAsync<Tid,Other>(pageNum, pageSize, predicate);
            var dtos = _mapper.Mapper.Map<List<TApiOther>>(ddos.Datas);
            return new ResultData<TApiOther>()
            {
                Datas = dtos,
                TotalCount = ddos.TotalCount
            };
        }
        public async Task<ResultData<TApiOther>> GetAllOtherAsync<Tid,Other, TApiOther>(Expression<Func<Other, bool>> predicate) where Other : class, IData<Tid>, new() where TApiOther : class, IData<Tid>, new()
        {
            var ddos = await _repository.GetAllOther<Tid,Other>(predicate);
            var dtos = _mapper.Mapper.Map<List<TApiOther>>(ddos.Datas);
            return new ResultData<TApiOther>()
            {
                Datas = dtos,
                TotalCount = ddos.TotalCount
            };
        }

        public async Task<Result> UpdateAsync(TApiEntity request, params Expression<Func<TEntity, object>>[] expressions)
        {
            var bo = await _mapper.MapToEntityAsync(request, expressions);
            bo.Id = request.Id;
            return await _repository.UpdateAsync(bo);
        }
        public async Task<Result> UpdateTOherAsync<Tid,TOher, TApiOher>(TApiOher request, params Expression<Func<TOher, object>>[] expressions) where TOher : class, IData<Tid>, new() where TApiOher : class, IData<Tid>, new()
        {
            var bo = await _mapper.MapToOtherEntityAsync<Tid, TOher, TApiOher>(request, expressions);
            bo.Id = request.Id;
            return await _repository.UpdateTOherAsync<Tid,TOher>(bo);
        }

        public async Task<bool> HasAsync(T id)
        {
            return await _repository.HasAsync(id);
        }


        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await _repository.CountAsync(predicate);
        public async Task<int> CountAsync() => await _repository.CountAsync();
    }
}
