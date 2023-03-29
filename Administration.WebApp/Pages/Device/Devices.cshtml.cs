using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartHome.Contracts.Models;

namespace Administration.WebApp.Pages.Device
{
    public class DevicesModel : PageModel
    {

        private readonly IServicesGrabber _servicesGrabber;
        private readonly INotyfService _notyf;

        public List<SmartHome.Contracts.Models.Device> Devices { get; set; }

        public DevicesModel(IServicesGrabber servicesGrabber, INotyfService notyf)
        {
            _servicesGrabber = servicesGrabber;
            _notyf = notyf;
        }

        public void OnGet()
        {
            Devices = _servicesGrabber.Device.GetAllDevices();
        }

        public IActionResult OnPostDeleteDevice(int id)
        {
            _servicesGrabber.Device.DeleteDevice(id);


            _servicesGrabber.History.AddHistoryItem(new History
            {
                Time = DateTime.Now,
                Action = $"Device with id: {id} deleted",
                User = Globals.LoggedUser
            });


            _notyf.Success($"Device with id: {id} deleted!");
            return RedirectToPage("/Device/Devices");
        }
    }
}
