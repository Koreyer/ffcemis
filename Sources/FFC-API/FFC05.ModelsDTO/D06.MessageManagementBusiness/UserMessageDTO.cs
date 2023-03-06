using FFC01.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D06.MessageManagementBusiness
{
    public class UserMessageDTO : Data<Guid>
    {
        public string ReceiveUserId { get; set; }
        public string? SendUserId { get; set; }
        //public DateTime CreateTime { get; set; }
        //public string Message { get; set; }
    }
}
