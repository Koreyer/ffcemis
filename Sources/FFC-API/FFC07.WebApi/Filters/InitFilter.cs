using FFC02.EntityModels.E01.AppIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace FFC07.WebApi.Filters
{
    public class InitFilter : IAsyncActionFilter
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<InitFilter> _logger;

        public InitFilter(UserManager<AppUser> userManager, ILogger<InitFilter> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var id = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUserProp = context.Controller.GetType().GetProperty("AppUserId");
            appUserProp.SetValue(context.Controller, id);

            //var a = context.RouteData.Values.Keys.First(x=>x == "controller");
            //var shoppingCarId = context.HttpContext.User.FindFirstValue("ShoppingCarId");
            //var shopProp = context.Controller.GetType().GetProperty("ShoppingCarId");
            //shopProp.SetValue(context.Controller, shoppingCarId);
            await next();




        }
    }
}
