using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartHome.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace Administration.WebApp.Pages.Device
{
    public class AddDeviceModel : PageModel
    {
        private readonly IServicesGrabber _servicesGrabber;
        private readonly INotyfService _notyf;

        private List<Room> Rooms { get; set; }
        public string[] RoomNames { get; set; }

        [BindProperty]
        public SmartHome.Contracts.Models.Device Device { get; set; }

        [BindProperty]
        public string SelectedRoom { get; set; }

        public AddDeviceModel(IServicesGrabber servicesGrabber, INotyfService notyf)
        {
            _servicesGrabber = servicesGrabber;
            _notyf = notyf;
        }

        public void OnGet()
        {
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

            Device.Room = Rooms.First(x => x.Name == SelectedRoom);

            _servicesGrabber.Device.AddDevice(Device);

            _servicesGrabber.History.AddHistoryItem(new History
            {
                Time = DateTime.Now,
                Action = $"Device {Device.Name} added",
                User = Globals.LoggedUser
            });

            _notyf.Success($"Device {Device.Name} added successfully!");
            return RedirectToPage("/Device/Devices");
        }
    }
}
