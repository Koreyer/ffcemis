using FFC01.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E06.MessageManagementBusiness
{
    public class MessageItem:Data<Guid>
    {
        public UserMessage UserMessage { get; set; }
        public string Message { get; set; }
    }
}
