using FFC01.Foundation.DataHelpers;
using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E05.ShoppingCarBusiness;
using FFC05.ModelsDTO.D00.Common;
using FFC05.ModelsDTO.D01.AppIdentity;
using FFC07.WebApi.Attributes;
using FFC07.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FFC07.WebApi.Controllers.C01.IdentityManager
{
    
    [Area("IdentityManager")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class IdentityManagerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        public string AppUserId { get; set; }

        public IdentityManagerController(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            _configuration = configuration;
            this.roleManager = roleManager;
        }
        #region 认证
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [NotCheckJWTVersion]
        [AllowAnonymous]
        public async Task<LoginResult> Login(CheckPassword account)
        {
            var a = userManager.Users;
            var user = await userManager.FindByNameAsync(account.UserName);
            if (user != null)
            {
                if (await userManager.CheckPasswordAsync(user, account.Password))
                {
                    //user.JWTVersion++;
                    //await userManager.UpdateAsync(user);
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim("JWTVersion",user.JWTVersion.ToString())

                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    DateTime exprire = DateTime.Now.AddSeconds(Convert.ToDouble(_configuration["JWT:ExpireSeconds"]));
                    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var jwt = new JwtSecurityToken
                    (
                        issuer: _configuration["JWT:Issuer"],
                        audience: _configuration["JWT:Audience"],
                        claims: claims,
                        //有效时间
                        expires: exprire,
                        signingCredentials: signingCredentials
                    );
                    string token = new JwtSecurityTokenHandler().WriteToken(jwt);
                    var result = new LoginResult
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Token = token
                    };
                    return result;
                }

            }
            return new LoginResult();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [NotCheckJWTVersion]
        [AllowAnonymous]
        public async Task<Result> Register(CheckPassword account)
        {
            var status = new Result
            {
                Message = "账号创建失败"
            };
            var user = new AppUser { UserName = account.UserName,Name = "FFC_" + account.UserName,CreateTime = DateTime.Now };
            var a = await userManager.FindByNameAsync(account.UserName);
            if ( await userManager.FindByNameAsync(account.UserName) != null)
            {
                status.Message = "账号已存在";
            }
            else
            {
                user.ShoppingCar = new ShoppingCar
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now
                };
                var result = await userManager.CreateAsync(user,account.Password);
                if (!result.Succeeded)
                {
                    status.Message = "账号创建失败";
                }
                else
                {
                    status.Code = 200;
                    status.Message = "账号创建成功";
                }
            }
            
            return status;
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [TypeFilter(typeof(JWTVersionFilter))]
        [HttpPost]
        public async Task<Result> Logout()
        {
            var user = await userManager.FindByIdAsync(AppUserId);
            user.JWTVersion++;
            await userManager.UpdateAsync(user);
            return new Result();
        }
        #endregion

        #region 权限

        [HttpPost]
        public async Task<Result> AddToRoleAsync(UserAddRoleDTO userAddRole)
        {
            var result = new Result { Message="操作失败"};
            var user = await userManager.FindByNameAsync(userAddRole.UserName);
            if(user != null)
            {
                try
                {
                    if(! await userManager.IsInRoleAsync(user, userAddRole.RoleName))
                    {
                        var addResult = await userManager.AddToRoleAsync(user, userAddRole.RoleName);
                        if (addResult.Succeeded)
                        {
                            result.Message = "操作成功";
                        }
                    }
                    else
                    {
                        result.Message = "用户已存在当前Role";
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("does not exist."))
                    {
                        result.Message = "Role不存在";
                    }
                    return result;
                }
            }
            return result;

        }

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="newRole"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> CreateRoleAsync(AppRole newRole)
        {
            var result = new Result();
            try
            {
                var role = await roleManager.CreateAsync(newRole);
                if (role.Succeeded)
                {
                    result.Message = "创建成功";
                }
                else
                {
                    result.Message = "创建失败";
                }
                return result;
            }
            catch (Exception ex)
            {

                if (ex.InnerException.Message.Contains("插入重复键的行"))
                {
                    result.Message = "当前权限已存在";
                }
                return result;
            }
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultData<AppRole>> GetRolesAsync()
        {
            var result = new ResultData<AppRole> { Datas = roleManager.Roles.ToList() };
            result.TotalCount = result.Datas.Count;
            return result;
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="newRole"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> UpdateRoleAsync(AppRole newRole)
        {
            var result = new Result();
            try
            {
                var role = await roleManager.UpdateAsync(newRole);
                if (role.Succeeded)
                {
                    result.Message = "编辑成功";
                }
                else
                {
                    result.Message = "编辑失败";
                }
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<Result> DeleteRoleAsync(string id)
        {
            var result = new Result { Message="当前Id不存在"};
            try
            {
                var role = await roleManager.FindByIdAsync(id);
                if(role != null)
                {
                    var r = await roleManager.DeleteAsync(role);
                    result.Message = "删除成功";
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"))
                {
                    result.Message = "id不是完整的Guid";
                }

                return result;
            }
        }
        #endregion
        [HttpGet]
        public ResultData<AdminRoutesDTO> TestRoute()
        {
            return new ResultData<AdminRoutesDTO>
            {
                Code = 200,
                Datas = new List<AdminRoutesDTO> { new AdminRoutesDTO
                {
                    AlwaysShow = true,
                    Component="Layout",
                    Name="System",
                    Redirect="noRedirect",
                    Path="/system",
                    Meta = new RouteMetaDTO
                    {
                        Icon="system",
                        Link = null,
                        NoCache = false,
                        Title="系统管理"
                    },
                    Hidden = false,
                    Children=new List<AdminRoutesDTO>
                    {
                        new AdminRoutesDTO
                        {
                            Component="system/menu/index",
                            Name="Menu",
                            Path="menu",
                            Meta = new RouteMetaDTO
                            {
                                Icon="tree-table",
                                Title="菜单管理"
                            }
                        }
                    }
                } }
            };
        }
    }
}
