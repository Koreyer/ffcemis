using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D01.AppIdentity
{
    public class LoginResult
    {
        public string Token { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; } = 200;
    }
}
