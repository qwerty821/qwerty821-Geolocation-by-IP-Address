using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Components.NavBar
{
    public class NavBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("NavBar");
           
        }
    }
}
