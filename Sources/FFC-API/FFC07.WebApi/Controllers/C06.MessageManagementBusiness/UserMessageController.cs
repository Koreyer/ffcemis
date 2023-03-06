using FFC01.Foundation.DataHelpers;
using FFC01.Foundation.Tools;
using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E06.MessageManagementBusiness;
using FFC05.ModelsDTO.D01.AppIdentity;
using FFC05.ModelsDTO.D06.MessageManagementBusiness;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace FFC07.WebApi.Controllers.C06.MessageManagementBusiness
{
    [Area("MessageManagementBusiness")]
    [Route("[area]/[controller]/[action]")]
    public class UserMessageController:BaseController<Guid,UserMessage,UserMessageDTO>
    {

        public UserMessageController(IApiService<Guid, UserMessage, UserMessageDTO> apiService):base(apiService)
        {
        }
        /// <summary>
        /// 获取聊天消息
        /// </summary>
        /// <param name="selectParameter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultData<MessageItemDTO>> GetHistoryMessage([FromQuery] SelectParameter selectParameter)
        {
            var messageId = Guid.NewGuid();
            switch (selectParameter.extendValue1)
            {
                case "1":
                    messageId = (await ApiService.GetAsync(x => x.SendUserId == AppUserId && x.ReceiveUserId == selectParameter.SearchValue)).Id;
                    break;
                    case "2":
                    messageId = (await ApiService.GetAsync(x => x.SendUserId == selectParameter.SearchValue && x.ReceiveUserId == AppUserId)).Id;
                    break;
                default:
                    break;
            }
            return await ApiService.GetOtherBySelectAsync<Guid, MessageItem, MessageItemDTO>(selectParameter.PageNum, selectParameter.PageSize, x => x.UserMessage.Id == messageId);
        }
        /// <summary>
        /// 获取聊天列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultData<AppUserDTO>> GetChatList(bool userRole)
        {
            var result = new ResultData<AppUserDTO>();

            var messages = new List<string>();
            if (userRole)
            {
                messages = (await ApiService.GetAllAsync(x => x.SendUserId == AppUserId)).Datas.Distinct().Select(x => x.ReceiveUserId).ToList();
            }
            else
            {
                messages = (await ApiService.GetAllAsync(x => x.ReceiveUserId == AppUserId)).Datas.Distinct().Select(x => x.SendUserId).ToList();
            }
            foreach (var item in messages)
            {
                result.Datas.Add(await ApiService.GetOtherAsync<Guid, AppUser, AppUserDTO>(Guid.Parse(item)));
            }
            return result;
        }
        /// <summary>
        /// 新增对话
        /// </summary>
        /// <param name="apiEntity"></param>
        /// <returns></returns>
        public override async  Task<Result> AddAsync(UserMessageDTO apiEntity)
        {
            var messaage = await ApiService.GetAsync(x => x.SendUserId == AppUserId && x.ReceiveUserId == apiEntity.ReceiveUserId);
            if(messaage == null)
            {
                apiEntity.SendUserId = AppUserId;
                return await base.AddAsync(apiEntity);
            }
            return new Result
            {
                Message="对话已存在"
            };
            
        }

        
        
    }
}
