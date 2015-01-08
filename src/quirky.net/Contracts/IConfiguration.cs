using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quirky.net.Contracts
{
    public interface IConfiguration
    {
        string Username { get; }
        string Password { get; }
        string Client_Secret { get; }
        string Client_Id { get; }
        string GrantType { get; }

        string BaseUrl { get; }
        string User_Agent { get; }
    }
}
