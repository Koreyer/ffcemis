using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E06.MessageManagementBusiness;
using FFC05.ModelsDTO.D01.AppIdentity;
using FFC05.ModelsDTO.D06.MessageManagementBusiness;
using FFC06.DTOServices;
using Microsoft.AspNetCore.SignalR;

namespace FFC07.WebApi.Hubs
{
    /// <summary>
    /// 创建SignalR中心
    /// </summary>
    public class ChatHub : Hub
    {
        private readonly IApiService<Guid, MessageItem, MessageItemDTO> apiService;

        public ChatHub(IApiService<Guid, MessageItem, MessageItemDTO> apiService)
        {
            this.apiService = apiService;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="businessId">目标Id</param>
        /// <param name="message">消息</param>
        /// 
        /// <returns></returns>
        public async Task SendMessage(string userId,string businessId, string message,bool type)
        {
            DateTime time = DateTime.Now;
            var messageId = Guid.NewGuid();
            if (type)
            {
                messageId = (await apiService.GetOtherAsync<Guid, UserMessage, UserMessageDTO>(x => x.SendUserId == businessId && x.ReceiveUserId == userId)).Id;
            }
            else
            {
                messageId = (await apiService.GetOtherAsync<Guid, UserMessage, UserMessageDTO>(x => x.SendUserId == userId && x.ReceiveUserId == businessId)).Id;

            }
            MessageItemDTO userMessage = new MessageItemDTO
            {
                UserMessageId = messageId.ToString(),
                CreateTime = time,
                Name = (await apiService.GetOtherAsync<Guid, AppUser, AppUserDTO>(Guid.Parse(userId))).Name,
                Message = message
            };
            await apiService.AddAsync(userMessage,x=>x.UserMessage);
            await Clients.All.SendAsync("ReceiveMessage", userMessage.Name, message,time);
        }
    }
}
