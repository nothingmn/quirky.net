using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using quirky.net.Entities.Domain;

namespace quirky.net.Entities.Response
{
    public class LoginResponse : BaseResponse
    {
        //[JsonProperty(propertyName:"Data")]
        public LoginData data { get; set; }
    }

}
