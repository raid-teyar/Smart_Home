using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Administration.WebApp.Pages
{
    public class HomeModel : PageModel
    {

        private readonly IServicesGrabber _servicesGrabber;

        public HomeModel(IServicesGrabber servicesGrabber)
        {
            _servicesGrabber = servicesGrabber;
        }

        public int DeviceCount { get; set; }

        public void OnGet()
        {
           DeviceCount = _servicesGrabber.Device.GetAllDevices().Count;
        }
    }
}
