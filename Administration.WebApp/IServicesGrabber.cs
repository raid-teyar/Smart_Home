using SmartHome.Contracts.Contracts;

namespace Administration.WebApp
{
    public interface IServicesGrabber
    {
        public IAdministrationUser User { get; set; }
        public IAdministrationAuthorization Authorization { get; set; }
        public IAdministrationDevice Device { get; set; }
        public IAdministrationHistory History { get; set; }
        public IAdministrationRoom Room { get; set; }
    }
}