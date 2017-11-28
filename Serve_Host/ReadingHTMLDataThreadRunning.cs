using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serve_Host
{
    public static class ReadingHTMLDataThreadRunning
    {
        private static object m_SyncObject = new object();
        //public int Thread 

        public static async void Uruchom(TemperaturePlaces DeviceItem, string ThreadName)
        {
            Thread th;
            await Task.Run(() =>
            {
                th = new Thread(item => ReadingHTMLData.ReadHTML(DeviceItem));
                th.Name = ThreadName.ToString();
                th.Start();
            });
        }
    }
}
