using FFC01.Foundation.Data;
using FFC01.Foundation.Enum;
using FFC02.EntityModels.E05.ShoppingCarBusiness;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC02.EntityModels.E01.AppIdentity
{
    public class AppUser: IdentityUser<Guid>, IData<Guid>
    {
        public string? Name { get; set; }
        public DateTime CreateTime { get; init; }
        public bool SoftDeleted { get; set; }
        public string? Description { get; set; }
        public string? SortCode { get; set; }

        public SexEnum Sex { get; set; }
        //public RoleTypEnum Role { get; set; }

        public long JWTVersion { get; set; }
        public Guid Avatar { get; set; }

        public ShoppingCar ShoppingCar { get; set; }



    }
}
