using FFC01.Foundation.Data;
using FFC02.EntityModels.E01.AppIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E06.MessageManagementBusiness
{
    public class UserMessage:Data<Guid>
    {
        public string ReceiveUserId { get; set; }
        public string SendUserId { get; set; }
        //public DateTime CreateTime { get; set; }
        //public string Message { get; set; }
    }
}
