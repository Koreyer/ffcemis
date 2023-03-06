using AutoMapper;
using FFC01.Foundation.Data;
using FFC04.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FFC06.DTOServices.Helpers
{
    public interface IMapperHelp<T,TEntity, TApiEntity>
        where TEntity : class, IData<T>, new()
        where TApiEntity : class, IData<T>, new()
    {
        public IRepository<T,TEntity> EntityRepository { get; }
        public IMapper Mapper { get; }
        Task<TEntity> MapToEntityAsync(TApiEntity apiBo, params Expression<Func<TEntity, object>>[] expressions);
        Task<List<TEntity>> MapToEntityListAsync(List<TApiEntity> apiBos, params Expression<Func<TEntity, object>>[] args);
        Task<TOther> MapToOtherEntityAsync<Tid,TOther, TApiOther>(TApiOther bo, params Expression<Func<TOther, object>>[] expressions)
            where TOther : class, IData<Tid>, new()
            where TApiOther : class, IData<Tid>, new();


    }
}
