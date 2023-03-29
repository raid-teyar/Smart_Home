using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartHome.Contracts.Models;

namespace Administration.WebApp.Pages.User
{
    public class AddUserModel : PageModel
    {

        private readonly IServicesGrabber _servicesGrabber;
        private readonly INotyfService _notyf;

        public AddUserModel(IServicesGrabber servicesGrabber, INotyfService notyf)
        {
            _servicesGrabber = servicesGrabber;
            _notyf = notyf;
        }

        [BindProperty]
        public SmartHome.Contracts.Models.User User { get; set; }


        public IActionResult OnPostAddUser()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _servicesGrabber.User.AddUser(User);

            
            _servicesGrabber.History.AddHistoryItem(new History
            {
                Time = DateTime.Now,
                Action = $"User {User.FullName} added",
                User = Globals.LoggedUser
            });


            _notyf.Success($"User {User.FullName} added successfully!");
            return RedirectToPage("/User/Users");
        }

        public void OnGet()
        {
        }
    }
}
