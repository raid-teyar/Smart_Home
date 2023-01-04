using SmartHome.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystem.UI.Helpers
{
    public class ServicesGrabber
    {
        public ISecuritySystemAuthorization Authorization { get; set; }
        public ISecuritySystemHistory History { get; set; }
        public ISecuritySystemDevice Device { get; set; }
        public ISecuritySystemDoor Door { get; set; }
    }
}
