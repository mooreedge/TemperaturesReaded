using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Serve_Host.Models
{

    public class TemperaturesDatabaseInitialize : CreateDatabaseIfNotExists<TemperaturesContext>
    {
        protected override void Seed(TemperaturesContext context)
        {

            GetTemperatures().ForEach(c => context.TemperatureFirstRoom.Add(c));
        }


        public static List<TemperatureFirstRoom> GetTemperatures()
        {
            var categories = new List<TemperatureFirstRoom> {
                new TemperatureFirstRoom
                {
                    TemperatureValue = 1,
                    MeasureTime = DateTime.Now
                },
            };
            return categories;
        }
    }
}


