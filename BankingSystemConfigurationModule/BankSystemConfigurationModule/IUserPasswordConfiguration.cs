using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemConfigurationModule
{
    /// <summary>
    /// TODO: implement the required changes
    /// </summary>
    public interface IUserPasswordConfiguration : IConfiguration
    {
        string UserName { get; set; }

        string Password { get; set; }
    }
}
