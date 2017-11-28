using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Serve_Host.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))] //this attribute is important for mysql db
    public class TemperaturesContext : DbContext
    {
        public TemperaturesContext() : base("TemperatureConnectionString")
        {
        }
        //this part is for table creation
        public DbSet<TemperatureFirstRoom> TemperatureFirstRoom { get; set; }

        public DbSet<TemperatureOutside> TemperatureOutside { get; set; }

        public DbSet<TemperatureDailyRoom> TemperatureDailyRoom { get; set; }

        public DbSet<TemperatureSalon> TemperatureSalon { get; set; }

        public DbSet<AirPressure> AirPressure { get; set; }

        public DbSet<Humidity> Humidity { get; set; }

    }
}
