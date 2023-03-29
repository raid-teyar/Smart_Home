using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Administration.WebApp.Pages.User
{
    public class EditUserModel : PageModel
    {

        private readonly IServicesGrabber _servicesGrabber;
        private readonly INotyfService _notyf;

        [BindProperty]
        public SmartHome.Contracts.Models.User User { get; set; }

        public EditUserModel(IServicesGrabber servicesGrabber, INotyfService notyf)
        {
            _servicesGrabber = servicesGrabber;
            _notyf = notyf;
        }


        public void OnGet()
        {
            User = _servicesGrabber.User.GetUserById(int.Parse(Request.Query["id"]));
        }

        public IActionResult OnPost()
        {
            int userID = int.Parse(Request.Query["id"]);
            User.Id = userID;
            _servicesGrabber.User.UpdateUser(User);
            _notyf.Success($"User with id: {User.Id} updated!");
            return RedirectToPage("/User/Users");
        }
    }
}
