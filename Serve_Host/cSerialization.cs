using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Serve_Host
{
    public class cSerialization<T2> 
    {
        public List<T2> Item = new List<T2>(); // serialization will be with elements in list

        public void Add(T2 value)
        {
            Item.Add(value);
        }
    }
}
