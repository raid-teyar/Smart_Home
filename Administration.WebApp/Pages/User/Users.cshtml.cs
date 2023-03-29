using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartHome.Contracts.Contracts;
using SmartHome.Contracts.Models;

namespace Administration.WebApp.Pages.User
{
    public class UsersModel : PageModel
    {
        private readonly IServicesGrabber _servicesGrabber;
        private readonly INotyfService _notyf;

        public List<SmartHome.Contracts.Models.User> Users { get; set; }

        public UsersModel(IServicesGrabber servicesGrabber, INotyfService notyf)
        {
            _servicesGrabber = servicesGrabber;
            _notyf = notyf;
        }

        public void OnGet()
        {
            Users = _servicesGrabber.User.GetAllUsers();
        }

        public IActionResult OnPostDeleteUser(int id)
        {
            _servicesGrabber.User.DeleteUser(id);


            _servicesGrabber.History.AddHistoryItem(new History
            {
                Time = DateTime.Now,
                Action = $"User with id: {id} deleted",
                User = Globals.LoggedUser
            });


            _notyf.Success($"User with id: {id} deleted!");
            return RedirectToPage("/User/Users");
        }

    }
}
