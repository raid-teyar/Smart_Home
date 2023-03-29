using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartHome.Contracts.Models;

namespace Administration.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IServicesGrabber _servicesGrabber;
        private readonly INotyfService _notyf;

        public IndexModel(ILogger<IndexModel> logger, IServicesGrabber servicesGrabber, INotyfService notyf)
        {
            _logger = logger;
            _servicesGrabber = servicesGrabber;
            _notyf = notyf;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            SmartHome.Contracts.Models.User? user = _servicesGrabber.Authorization.Login(Username, Password);

            if (user is null || !ModelState.IsValid)
            {
                _notyf.Error("Invalid username or password");
                return Page();
            }

            _notyf.Success("Login successful");
            Globals.LoggedUser = user;
            return RedirectToPage("/Home", new { user });
        }
    }
}