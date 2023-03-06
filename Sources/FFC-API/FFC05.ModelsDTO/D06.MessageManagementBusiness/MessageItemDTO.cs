using FFC01.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D06.MessageManagementBusiness
{
    public class MessageItemDTO : Data<Guid>
    {
        public string UserMessageId { get; set; }
        public string Message { get; set; }
    }
}
