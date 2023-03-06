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
    public class MapperHelp<T,TEntity, TApiEntity> : IMapperHelp<T,TEntity, TApiEntity>
        where TEntity : class, IData<T>, new()
        where TApiEntity : class, IData<T>, new()
    {
        private readonly IRepository<T,TEntity> entityRepository;
        private readonly IMapper mapper;
        public IRepository<T,TEntity> EntityRepository => entityRepository;
        public IMapper Mapper => mapper;
        public MapperHelp(IRepository<T,TEntity> entityRepository, IMapper mapper)
        {
            this.entityRepository = entityRepository;
            this.mapper = mapper;
        }



        /// <summary>
        /// 对于没有关联关系的实体类的转换
        /// </summary>
        /// <param name="apiBo"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> MapToEntity(TApiEntity apiBo)
        {
            if (apiBo == null) return default(TEntity);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TApiEntity, TEntity>());
            return await Task.Run(() => config.CreateMapper().Map<TEntity>(apiBo));
        }

        public virtual TApiEntity MapToApiEntity(TEntity bo)
        {
            if (bo == null) return default(TApiEntity);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TApiEntity>());
            return config.CreateMapper().Map<TApiEntity>(bo);
        }

        public static object CreateObject(Type type)
        {
            var newExpression = Expression.Lambda(Expression.New(type));
            var propertyDelegate = newExpression.Compile();//编译成委托
            var result = propertyDelegate.DynamicInvoke();
            return result;
        }

        public virtual async Task<TOther> MapToOtherEntityAsync<Tid,TOther, TApiOther>(TApiOther apiBo, params Expression<Func<TOther, object>>[] args)
            where TApiOther : class, IData<Tid>, new() where TOther : class, IData<Tid>, new()
        {
            if (apiBo == null) return default(TOther);
            var MCE = new MapperConfigurationExpression();
            var cfg = (MCE.CreateMap<TApiOther, TOther>());
            if (args != null)
            {
                foreach (var arg in args)
                {
                    dynamic result;
                    var propertyName = arg.Body.Type.Name + "Id";
                    var boId = Guid.Parse(apiBo.GetType().GetProperty(propertyName).GetValue(apiBo).ToString());
                    if (boId != Guid.Empty)
                    {
                        //获取EntityRepository的GetOtherBoAsync方法
                        var getOtherBoAsync = EntityRepository.GetType().GetMethod("GetOtherByIdAsync");
                        //设置泛型类型
                        var generic = getOtherBoAsync.MakeGenericMethod(new Type[] { typeof(Guid), arg.Body.Type });
                        //调用方法并使用dynanic来接受Task<TOther>的返回
                        dynamic task = generic.Invoke(EntityRepository, new object[] { boId });
                        result = await task;
                    }// 数据库没有该条数据，用意可能是同时添加该条数据，所以创建一个新的对象
                    else
                    {
                        result = CreateObject(arg.Body.Type);
                        result.Id = Guid.NewGuid();
                    }
                    if (result == null)
                    {
                        result = CreateObject(arg.Body.Type);
                        result.Id = boId;
                    }



                    cfg.ForMember(arg, ops => ops.MapFrom(x => result));
                }
            }
            var config = new MapperConfiguration(MCE);
            var a = config.CreateMapper().Map<TOther>(apiBo);
            return a;
        }

        public virtual async Task<List<TEntity>> MapToEntityListAsync(List<TApiEntity> apiBos, params Expression<Func<TEntity, object>>[] args)
        {
            var result = new List<TEntity>();
            foreach (var apiBo in apiBos)
            {
                result.Add(await MapToEntityAsync(apiBo, args));
            }
            return result;
        }


        public virtual async Task<TEntity> MapToEntityAsync(TApiEntity apiBo, params Expression<Func<TEntity, object>>[] args)
        {
            if (apiBo == null) return default(TEntity);
            var MCE = new MapperConfigurationExpression();
            var cfg = (MCE.CreateMap<TApiEntity, TEntity>());
            if (args != null)
            {
                foreach (var arg in args)
                {
                    dynamic result;
                    var propertyName = arg.Body.Type.Name + "Id";
                    if (arg.Body.ToString().Contains("Parent"))
                    {
                        propertyName = "ParentId";
                    }
                    var boId = Guid.Parse(apiBo.GetType().GetProperty(propertyName).GetValue(apiBo).ToString());
                    if (boId != Guid.Empty)
                    {
                        //获取EntityRepository的GetOtherBoAsync方
                        var getOtherBoAsync = EntityRepository.GetType().GetMethod("GetOtherByIdAsync");
                        //设置泛型类型
                        var generic = getOtherBoAsync.MakeGenericMethod(new Type[] { typeof(Guid), arg.Body.Type });
                        //调用方法并使用dynanic来接受Task<TOther>的返回
                        dynamic task = generic.Invoke(EntityRepository, new object[] { boId });
                        result = await task;

                    }// 数据库没有该条数据，用意可能是同时添加该条数据，所以创建一个新的对象
                    else
                    {
                        result = CreateObject(arg.Body.Type);
                        result.Id = Guid.NewGuid();
                    }
                    if (result == null)
                    {
                        result = CreateObject(arg.Body.Type);
                        result.Id = boId;
                    }



                    cfg.ForMember(arg, ops => ops.MapFrom(x => result));
                }
            }
            var config = new MapperConfiguration(MCE);
            var a = config.CreateMapper().Map<TEntity>(apiBo);
            return a;
        }







    }
}
