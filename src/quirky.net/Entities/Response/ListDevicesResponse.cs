using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quirky.net.Entities.Domain;

namespace quirky.net.Entities.Response
{
    public class ListDevicesResponse : BaseResponse
    {
        public List<Device> Devices { get; set; }
    }
}