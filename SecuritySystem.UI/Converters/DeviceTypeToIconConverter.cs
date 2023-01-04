using SmartHome.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SecuritySystem.UI.Converters
{
    [ValueConversion(typeof(DeviceType), typeof(string))]
    class DeviceTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DeviceType deviceType = (DeviceType)value;
            switch (deviceType)
            {
                case DeviceType.Camera:
                    return "CameraSolid";
                case DeviceType.Temperature:
                    return "ThermometerHalfSolid";
                case DeviceType.Humidity:
                    return "TintSolid";
                default:
                    return "QuestionCircleSolid";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
