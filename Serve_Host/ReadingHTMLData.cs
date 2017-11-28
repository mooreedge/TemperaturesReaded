using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Threading;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.Entity;
using Serve_Host.Models;
using System.Net;
using System.Net.Mail;


namespace Serve_Host
{
    public static class ReadingHTMLData
    {
        #region Properties
        private static HtmlWeb web;
        private static HtmlDocument doc;
        private static string html;
        private static string tempSend;
        private static string ReadedTemperatureValue;
        private static object lock_i = new object();
        #endregion
        public static void ReadHTML(TemperaturePlaces DeviceDetails)
        {
            while (true)
            {
                try
                {
                    try // get esp web data
                    {
                        lock (lock_i)
                        {
                            using (TemperaturesContext TemperaturesCtx = new TemperaturesContext())
                            {
                                double TemperatureAsDouble;
                                web = new HtmlWeb();
                                doc = web.Load("http://" + DeviceDetails.DeviceIpAddress.ToString() + "/");
                                html = doc.DocumentNode.Descendants("body").Single().InnerHtml;
                                tempSend = HelpersMethods.getBetween(html, Thread.CurrentThread.Name + " '", "'"); // value to send
                                TemperatureAsDouble = Convert.ToDouble(tempSend);
                                ReadedTemperatureValue = DeviceDetails.PlaceName + "'" + tempSend + "';";
                                Console.WriteLine("Readed Value: {0} by thread: {1} at time {2}", ReadedTemperatureValue, Thread.CurrentThread.Name.ToString(), DateTime.Now);
                                if (TemperatureAsDouble > -50) //Tutaj czasem mamy sytuacje, ze dostajemy pomiary np -150 co jest bledem z punktu widzenia elektroniki
                                {
                                    switch (Thread.CurrentThread.Name)
                                    {
                                        case "STRYCH":
                                            TemperaturesCtx.TemperatureDailyRoom.Add(new TemperatureDailyRoom
                                            {
                                                TemperatureValue = TemperatureAsDouble,
                                                MeasureTime = DateTime.Now
                                            });
                                            TemperaturesCtx.SaveChanges();
                                            break;
                                        case "DWOR":
                                            TemperaturesCtx.TemperatureOutside.Add(new TemperatureOutside
                                            {
                                                TemperatureValue = TemperatureAsDouble,
                                                MeasureTime = DateTime.Now
                                            });
                                            TemperaturesCtx.SaveChanges();
                                            break;
                                        case "POKOJ":
                                            TemperaturesCtx.TemperatureFirstRoom.Add(new TemperatureFirstRoom
                                            {
                                                TemperatureValue = TemperatureAsDouble,
                                                MeasureTime = DateTime.Now
                                            });
                                            TemperaturesCtx.SaveChanges();
                                            break;
                                        case "PIETROTEMPERATURA":
                                            TemperaturesCtx.TemperatureSalon.Add(new TemperatureSalon
                                            {
                                                TemperatureValue = TemperatureAsDouble,
                                                MeasureTime = DateTime.Now
                                            });
                                            TemperaturesCtx.SaveChanges();
                                            break;
                                        case "CISNIENIE":
                                            TemperaturesCtx.AirPressure.Add(new AirPressure
                                            {
                                                AirPressureValue = TemperatureAsDouble,
                                                MeasureTime = DateTime.Now
                                            });
                                            TemperaturesCtx.SaveChanges();
                                            break;
                                        case "WILGOTNOSC":
                                            if (TemperatureAsDouble != 0)
                                            {
                                                TemperaturesCtx.Humidity.Add(new Humidity
                                                {
                                                    HumidityValue = TemperatureAsDouble,
                                                    MeasureTime = DateTime.Now
                                                });
                                                TemperaturesCtx.SaveChanges();
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        Thread.Yield();
                        Thread.Sleep(600);
                    }
                    catch(Exception ex)
                    {
                        SendErrorViaMail();
                        Thread.Sleep(50000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> Client is disconnected, I am wainting...");
                    SendErrorViaMail();
                }
            }
        }

        public static void SendErrorViaMail()
        {
            Console.WriteLine(" >> Can't acces devices from thread {0}", Thread.CurrentThread.Name.ToString());
            var fromAddress = new MailAddress("chojaraspberry@gmail.com", "Raspberry Pi");
            var toAddress = new MailAddress("krzysztof.choja@wp.pl", "krzysztof.choja");
            const string fromPassword = "xxxx";
            string subject = "Device Problem: " + Thread.CurrentThread.Name.ToString();
            string body = "Can't acces devices from thread " + Thread.CurrentThread.Name.ToString() + ". Please restart it.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
