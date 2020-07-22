using BankApplicationClientModule.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace BankApplicationClientModule.Tests
{
    [TestClass]
    public class DBTests
    {
        [TestInitialize]
        public void Initialization()
        {
            // Create the schema in the database
            using (var context = new ClientModuleDBContext("DBContextConnString"))
            {
                context.ClearAllAData();
                // test data
                context.Database.ExecuteSqlCommand($"insert into BankClient(FirstName, LastName) values('Paul', 'Junior')");
                context.Database.ExecuteSqlCommand($"insert into BankClient(FirstName, LastName) values('Anna', 'Morning')");
                context.Database.ExecuteSqlCommand($"insert into BankClient(FirstName, LastName) values('Tom', 'Second')");

                context.Database.ExecuteSqlCommand($"insert into BankAccount(AccountNumber, ClientId) select '1000000000000000000', Id from BankClient where FirstName = 'Paul'");
                context.Database.ExecuteSqlCommand($"insert into BankAccount(AccountNumber, ClientId) select '1000000000000000001', Id from BankClient where FirstName = 'Paul'");
                context.Database.ExecuteSqlCommand($"insert into BankAccount(AccountNumber, ClientId) select '1000000000000000002', Id from BankClient where FirstName = 'Paul'");
                context.Database.ExecuteSqlCommand($"insert into BankAccount(AccountNumber, ClientId) select '1000000000000000003', Id from BankClient where FirstName = 'Anna'");
                context.Database.ExecuteSqlCommand($"insert into BankAccount(AccountNumber, ClientId) select '1000000000000000004', Id from BankClient where FirstName = 'Anna'");
            }
        }

        private ClientModuleDataAccess CreateClientModuleDataAccess()
        {
            return new ClientModuleDataAccess("DBContextConnString");
        }

        [TestMethod]
        public void CreateDB_Test()
        {
            using (var dao = CreateClientModuleDataAccess())
            {
                var list = dao.GetAllClients();
                Assert.IsNotNull(list);
            }
        }

        [TestMethod]
        public void ClientModuleDBdao_NewClient_Test()
        {
            int cnt = 0;
            using (var dao = CreateClientModuleDataAccess())
            {
                cnt = dao.GetAllClients().Count();
                Assert.IsTrue(dao.SaveNewClient(new Model.BankClient() { Address = "Address", FirstName = "John", LastName = "Tester" }));
                Assert.AreEqual(cnt + 1, dao.GetAllClients().Count());
            }

            using (var dao = CreateClientModuleDataAccess())
            {
                Assert.AreEqual(cnt + 1, dao.GetAllClients().Count());
            }
        }

        [TestMethod]
        public void ClientModuleDBdao_StartTracking_Test()
        {
            Model.BankClient client;
            using (var dao = CreateClientModuleDataAccess())
            {
                Assert.AreEqual(3, dao.GetAllClients().Count());
                client = dao.GetAllClients().FirstOrDefault(); // tracked client
                client.FirstName = "ABC";

                var detached = new Model.BankClient() { Id = client.Id };
                detached = dao.StartTracking(detached);

                Assert.AreNotEqual(client, detached);
                detached.FirstName = "XYZ";

                dao.DBContext.SaveChanges();
            }

            using (var dao = CreateClientModuleDataAccess())
            {
                var detached = dao.GetAllClients().Where(w => w.Id == client.Id).FirstOrDefault();
                Assert.AreEqual("XYZ", detached.FirstName); // XYZ overwrite data from DB
            }
        }


        [TestMethod]
        public void ClientModuleDBdao_StartTrackingNew_Test()
        {
            Model.BankClient client;
            using (var dao = CreateClientModuleDataAccess())
            {
                Assert.AreEqual(3, dao.GetAllClients().Count());
                client = new BankClient()
                {
                    FirstName = "DEF"
                };

                client = dao.StartTracking(client);

                dao.DBContext.SaveChanges();
            }

            using (var dao = CreateClientModuleDataAccess())
            {
                var detached = dao.GetAllClients().Where(w => w.Id == client.Id).FirstOrDefault();
                Assert.AreEqual("DEF", detached.FirstName);
            }
        }


        [TestMethod]
        public void ClientModuleDBdao_GetAllClientsThatHaveAtLeastOneAccount_Test()
        {
            using (var dao = CreateClientModuleDataAccess())
            {
                Assert.AreEqual(3, dao.GetAllClients().Count);
                Assert.AreEqual(5, dao.GetAllAccounts().Count);

                var clients = dao.GetAllClientsThatHaveAtLeastOneAccountDetached();
                Assert.AreEqual(2, clients.Count);
                Assert.AreEqual(3, clients.Where(w => w.FirstName == "Paul").Select(s => s.ClientAccounts.Count).FirstOrDefault());
                Assert.AreEqual(2, clients.Where(w => w.FirstName == "Anna").Select(s => s.ClientAccounts.Count).FirstOrDefault());
            }
        }

        [TestMethod]
        public void ClientModuleDBdao_IsClientTrackedByEF_Test()
        {
            BankClient client = null;
            using (var dao = CreateClientModuleDataAccess())
            {
                client = dao.GetAllClients().FirstOrDefault();
                Assert.IsTrue(dao.IsClientTrackedByEF(client));
            }

            using (var dao = CreateClientModuleDataAccess())
            {
                Assert.IsFalse(dao.IsClientTrackedByEF(client));
            }
        }

        [TestMethod]
        public void ClientModuleDBdao_IsDelayingLoadingDisabled_Test()
        {
            using (var dao = CreateClientModuleDataAccess())
            {
                var client = dao.GetAllClients().Where(w => w.FirstName == "Paul").FirstOrDefault();
                Assert.IsTrue(client.ClientAccounts == null || client.ClientAccounts.Count == 0);
            }
        }
    }
}
