using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemConfigurationModule
{
    public abstract class BaseConfiguration
    {
        public BaseConfiguration(int id)
        {
            //TODO: set Id property here
            Id = id;
            // do not change the line below
            LastLog = "Configuration process";
        }

        /// <summary>
        /// TODO: change the method
        /// </summary>
        /// <returns></returns>
        public abstract string GetConfigurationInfo();

        /// <summary>
        /// TODO: change the property
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// TODO: change the property
        /// </summary>
        public string LastLog { get; protected set; }

        /// <summary>
        /// TODO: change the property
        /// </summary>
        public Guid UniqueGuid { private get; set; }

        public void Log(string log)
        {
            //TODO: set LastLog using value from log parameter
            LastLog = log;
        }
    }
}
