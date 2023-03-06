using FFC02.EntityModels.E07.SystemBusiness;
using FFC05.ModelsDTO.D07.SystemBusiness;
using FFC06.DTOServices;
using FFC07.WebApi.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace FFC07.WebApi.Controllers.C07.SystemtManagementBusiness
{
    [Area("SystemtManagementBusiness")]
    [Route("[area]/[controller]/[action]")]
    public class HomePageCarouselController : BaseController<Guid, HomePageCarousel, HomePageCarouselDTO>
    {
        public HomePageCarouselController(IApiService<Guid, HomePageCarousel, HomePageCarouselDTO> apiService) : base(apiService)
        {
        }
    }
}
