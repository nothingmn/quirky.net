using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quirky.net.Entities.Domain
{
    public class Device
    {
        public string Name { get; set; }
        public string Locale { get; set; }
        public Units Units { get; set; }
        public int Created_At { get; set; }
        public Subscription Subscription { get; set; }
        public string Manufacturer_Device_Model { get; set; }
        public string Manufacturer_Device_Id { get; set; }
        public string Hub_Id { get; set; }
        public string Local_Id { get; set; }
        public string Radio_Type { get; set; }
        public string Device_Manufacturer { get; set; }
        public float[] Lat_Lng { get; set; }
        public string Location { get; set; }
    }

}
