using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemConfigurationModule.Tests
{
    [TestClass]
    public class YourClassTests
    {
        [TestMethod]
        public void YorClassImplementsIUserPasswordConfiguration_Test()
        {
            var obj = ConfigurationFactory.Create(10);
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IUserPasswordConfiguration));
        }

        /// <summary>
        /// Your class should implement IUserPasswordConfiguration interafce and inherit from BaseConfiguration class
        /// </summary>
        [TestMethod]
        public void YorClassInheritFromBaseConfiguration_Test()
        {
            var obj = ConfigurationFactory.Create(11);
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(BaseConfiguration));
        }

        /// <summary>
        /// the constructor should set Id property using value from id parameter
        /// </summary>
        [TestMethod]
        public void Create_WithParams_Test()
        {
            var obj = ConfigurationFactory.Create(20);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is BaseConfiguration);
            Assert.AreEqual(20, (obj as BaseConfiguration).Id);
        }

        /// <summary>
        /// the GetConfigurationInfo method return proper information.
        /// </summary>
        [TestMethod]
        public void GetConfigurationInfo_Text_Test()
        {
            var obj = ConfigurationFactory.Create(33);
            Assert.IsNotNull(obj);

            Assert.AreEqual("Configuration id: 33; Log: Configuration process", obj.GetConfigurationInfo());
        }

        /// <summary>
        /// the GetConfigurationInfo method return proper information.
        /// </summary>
        [TestMethod]
        public void GetConfigurationInfo_Log_Test()
        {
            var obj = ConfigurationFactory.Create(33);
            Assert.IsNotNull(obj);
            obj.Log("my log");

            Assert.AreEqual("Configuration id: 33; Log: my log", obj.GetConfigurationInfo());
        }

        /// <summary>
        /// the GetConfigurationInfo method return proper information.
        /// </summary>
        [TestMethod]
        public void GetConfigurationInfo_Empty_LastLog_Test()
        {
            var obj = ConfigurationFactory.Create(44);
            Assert.IsNotNull(obj);
            obj.Log("");

            Assert.AreEqual("Configuration id: 44; Log: no log", obj.GetConfigurationInfo());
        }

        /// <summary>
        /// the GetConfigurationInfo method in BaseConfiguration should has no default implementation (cannot have body). 
        /// All classes that inherit from BaseConfiguration should implement it. 
        /// </summary>
        [TestMethod]
        public void GetConfigurationInfo_Implementation_Test()
        {
            var obj = ConfigurationFactory.Create(44);
            Assert.IsNotNull(obj);

            Assert.AreNotEqual("No configuration info", obj.GetConfigurationInfo());
        }

        /// <summary>
        /// access to read UniqueGuid property is allowed only from BaseConfiguration. Access to write is allowed from any places.  
        /// </summary>
        [TestMethod]
        public void CanSet_UniqueGuid_Test()
        {
            var obj = ConfigurationFactory.Create(44);
            Assert.IsNotNull(obj);
            var guid = Guid.NewGuid();
            (obj as BaseConfiguration).UniqueGuid = guid; // can set and there is no errors
        }

        /// <summary>
        /// the LastLog property can be modified only from BaseConfiguration and child classes or by calling Log method from BaseConfiguration.
        /// </summary>
        [TestMethod]
        public void CanSet_LastLog_From_Log_Method_Test()
        {
            var obj = ConfigurationFactory.Create(44);
            Assert.IsNotNull(obj);
            (obj as BaseConfiguration).Log("my test log"); // can set and there is no errors
            Assert.AreEqual("my test log", (obj as BaseConfiguration).LastLog);
        }
    }
}
