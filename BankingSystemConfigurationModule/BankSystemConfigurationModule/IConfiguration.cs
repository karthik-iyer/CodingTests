using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemConfigurationModule
{
    public interface IConfiguration
    {
        string GetConfigurationInfo();

        void Log(string log);
    }
}
