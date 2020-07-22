using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemConfigurationModule
{
    public static class ConfigurationFactory
    {
        public static IConfiguration Create(int id)
        {
            //TODO: create instance of IConfiguration (instance of your class)
            return new UserPasswordConfiguration(id);
        }
    }
}
