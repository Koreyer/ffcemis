using FFC01.Foundation.Data;
using FFC01.Foundation.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC05.ModelsDTO.D01.AppIdentity
{
    public class AppUserDTO : IdentityUser<Guid>, IData<Guid>
    {
        public string? Name { get; set; }
        public DateTime CreateTime { get; init; }
        public bool SoftDeleted { get; set; }
        public string? Description { get; set; }
        public string? SortCode { get; set; }

        public SexEnum Sex { get; set; }
        public string? SexName { get; set; }
        //public RoleTypEnum Role { get; set; }
        //public string RoleName { get; set; }
        public Guid Avatar { get; set; }

        public string ShoppingCarId { get; set; }




    }
}
