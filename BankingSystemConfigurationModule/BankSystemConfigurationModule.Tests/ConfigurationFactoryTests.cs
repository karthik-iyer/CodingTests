using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemConfigurationModule.Tests
{
    [TestClass]
    public class ConfigurationFactoryTests
    {
        [TestMethod]
        public void Create_Test()
        {
            var obj = ConfigurationFactory.Create(10);
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IConfiguration));
        }
    }
}
