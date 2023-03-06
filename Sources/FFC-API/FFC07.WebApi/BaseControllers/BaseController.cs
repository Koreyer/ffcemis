using FFC01.Foundation.Data;
using FFC01.Foundation.DataHelpers;
using FFC01.Foundation.Enum;
using FFC01.Foundation.Tools;
using FFC04.DataRepository;
using FFC06.DTOServices;
using FFC07.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FFC07.WebApi.BaseControllers
{
    [TypeFilter(typeof(JWTVersionFilter))]
    [TypeFilter(typeof(InitFilter))]
    //[Route("[area][controller]/[action]")]
    [ApiController]
    public class BaseController<T,TEntity,TApiEntity>:ControllerBase
        where TEntity : class,IData<T>,new()
        where TApiEntity : class,IData<T>, new()
    {

        private readonly IApiService<T,TEntity, TApiEntity> _apiService;
        private readonly IRepository<T,TEntity> _repository;

        private Expression<Func<TEntity, object>>[] expressions ;

        protected IApiService<T,TEntity, TApiEntity> ApiService { get => _apiService; }
        protected IRepository<T,TEntity> Repository { get => _repository; }

        public string AppUserId { get; set; }
        public string ShoppingCarId { get; set; }

        protected void SetIncludeExpressions(params Expression<Func<TEntity, object>>[] expressions) => this.expressions = expressions;

        public BaseController(IApiService<T,TEntity, TApiEntity> apiService)
        {
            _apiService = apiService;
            _repository = apiService.Repository;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ResultData<TApiEntity>> GetAsync() => await _apiService.GetAllAsync();

        /// <summary>
        /// 根据Id进行查找数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<TApiEntity> GetByIdAsync(T id) => await _apiService.GetOneByIdAsync(id);

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="selectParameter"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ResultData<TApiEntity>> GetBySelectAsync([FromQuery] SelectParameter selectParameter)
        {
            if (!string.IsNullOrEmpty(selectParameter.SearchValue))
            {
                return await _apiService.GetBySelectAsync(selectParameter.PageNum, selectParameter.PageSize, x => x.Name.Contains(selectParameter.SearchValue));
            }
            return await _apiService.GetBySelectAsync(selectParameter.PageNum, selectParameter.PageSize, null);
        }

        /// <summary>
        /// 通过id进行删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<Result> DeleteByIdAsync(T id)
        {
            return await _apiService.DeleteByIdAsync(id);
        }

        [HttpDelete]
        public virtual async Task<Result> SoftDeleteByIdAsync(T id)
        {
            var bo = await ApiService.GetOneByIdAsync(id);
            bo.SoftDeleted = true;
            var result = await ApiService.UpdateAsync(bo, expressions);
            result.Message = "删除成功";
            return result;
        }
        [HttpDelete]
        public virtual async Task<Result> DeleteRangeByIdAsync([FromBody]List<T> ids)
        {
            return await ApiService.DeleteRangeByIdAsync(ids);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="apiEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<Result> AddAsync(TApiEntity apiEntity) => await _apiService.AddAsync(apiEntity, expressions);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="apiEntity"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<Result> UpdateAsync(TApiEntity apiEntity) => await _apiService.UpdateAsync(apiEntity, expressions);
    }
}
