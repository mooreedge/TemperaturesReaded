using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using HtmlAgilityPack;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using Serve_Host.Models;
using System.Data;



namespace Serve_Host
{
    class Program
    {

        #region main method
        static void Main(string[] args)
        {
            Console.WriteLine(" >> Loading devices list...");

            var DeviceList = new List<TemperaturePlaces> {
                new TemperaturePlaces
                {
                    PlaceName = "STRYCH",
                    DeviceIpAddress = "192.168.0.106"
                },
                new TemperaturePlaces
                {
                    PlaceName = "POKOJ",
                    DeviceIpAddress = "192.168.0.101"
                },
                new TemperaturePlaces
                {
                    PlaceName = "PIETROTEMPERATURA",
                    DeviceIpAddress = "192.168.0.110"
                },
                new TemperaturePlaces
                {
                    PlaceName = "CISNIENIE",
                    DeviceIpAddress = "192.168.0.110"
                },
                new TemperaturePlaces
                {
                    PlaceName = "STRYCH",
                    DeviceIpAddress = "192.168.0.106"
                },
                new TemperaturePlaces
                {
                    PlaceName = "WILGOTNOSC",
                    DeviceIpAddress = "192.168.0.110"
                },
                new TemperaturePlaces
                {
                    PlaceName = "DWOR",
                    DeviceIpAddress = "192.168.0.101"
                },

                };

            // Initialize the product database. 
            Database.SetInitializer(new TemperaturesDatabaseInitialize());

            using (var ctx = new TemperaturesContext())
            {
                ctx.Database.CreateIfNotExists(); //this monster can be use to initialize empty database 
                //Configure Null Column
            }

            Console.WriteLine(" >> Device list loaded...");
            Console.WriteLine(" >> Server program has started, wainting for connection threads with devices...");



            foreach (var TemperatureItem in DeviceList)
            {
                ReadingHTMLDataThreadRunning.Uruchom(TemperatureItem, TemperatureItem.PlaceName);
                Thread.Sleep(500);
            }
            Console.ReadLine();
        }
        #endregion

    }
}