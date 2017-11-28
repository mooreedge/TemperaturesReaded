using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Serve_Host.Models
{
    public class TemperatureCommon
    {
        [Key]
        public int TemperatureID { get; set; }
        public double TemperatureValue { get; set; }
        public DateTime MeasureTime { get; set; }
    }

    public class TemperatureFirstRoom : TemperatureCommon
    {
    }

    public class TemperatureOutside : TemperatureCommon
    {
    }

    public class TemperatureDailyRoom : TemperatureCommon
    {
    }

    public class TemperatureSalon : TemperatureCommon
    {
    }

    public class AirPressure
    {
        [Key]
        public int AirPressureID { get; set; }
        public double AirPressureValue { get; set; }
        public DateTime MeasureTime { get; set; }
    }

    public class Humidity
    {
        [Key]
        public int HumidityID { get; set; }
        public double HumidityValue { get; set; }
        public DateTime MeasureTime { get; set; }
    }
}
