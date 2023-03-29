using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartHome.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace Administration.WebApp.Pages.Device
{
    public class EditDeviceModel : PageModel
    {
        private readonly IServicesGrabber _servicesGrabber;
        private readonly INotyfService _notyf;

        private List<Room> Rooms { get; set; }
        public string[] RoomNames { get; set; }

        [BindProperty]
        public SmartHome.Contracts.Models.Device Device { get; set; }

        [BindProperty]
        public string SelectedRoom { get; set; }

        public EditDeviceModel(IServicesGrabber servicesGrabber, INotyfService notyf)
        {
            _servicesGrabber = servicesGrabber;
            _notyf = notyf;
            initRooms();
        }

        public void OnGet()
        {
            Device = _servicesGrabber.Device.GetDeviceById(int.Parse(Request.Query["id"]));
            initRooms();
        }

        private void initRooms()
        {
            Rooms = _servicesGrabber.Room.GetAllRooms();

            RoomNames = new string[Rooms.Count];
            for (int i = 0; i < Rooms.Count; i++)
            {
                RoomNames[i] = Rooms[i].Name;
            }
        }

        public IActionResult OnPost()
        {
            initRooms();

            int deviceID = int.Parse(Request.Query["id"]);
            Device.Id = deviceID;

            Device.Room = Rooms.First(x => x.Name == SelectedRoom);

            _servicesGrabber.Device.UpdateDevice(Device);

            _notyf.Success($"Device with id: {Device.Id} updated!");
            return RedirectToPage("/Device/Devices");
        }
    }
}
