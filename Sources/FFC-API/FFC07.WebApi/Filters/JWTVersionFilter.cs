using FFC07.WebApi.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FFC02.EntityModels.E01.AppIdentity;

namespace FFC07.WebApi.Filters
{
    public class JWTVersionFilter : IAsyncActionFilter
    {
        private readonly UserManager<AppUser> userManager;

        public JWTVersionFilter(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerActionDescriptor? ctrlActionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if (ctrlActionDesc == null)
            {
                await next();
                return;
            }
            if (ctrlActionDesc.MethodInfo.GetCustomAttributes(typeof(NotCheckJWTVersionAttribute), true).Any())
            {
                await next();
                return;
            }

            var claimJWTVer = context.HttpContext.User.FindFirst("JWTVersion");
            if (claimJWTVer == null)
            {
                //context.Result = new ObjectResult("payload中没有JWTVersion");
                context.Result = new ObjectResult("请重新登录") { StatusCode = 401 };
                return;
            }
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            long jwtVerFromClent = Convert.ToInt64(claimJWTVer.Value);
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                context.Result = new ObjectResult("没有当前用户");
                return;
            }
            if (user.JWTVersion > jwtVerFromClent)
            {
                context.Result = new ObjectResult("请重新登录") { StatusCode = 401 };
                return;
            }
            var id = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUserProp = context.Controller.GetType().GetProperty("AppUserId");
            appUserProp.SetValue(context.Controller, id);
            await next();

        }
    }
}
