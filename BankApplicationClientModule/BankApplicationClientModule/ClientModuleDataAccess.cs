using BankApplicationClientModule.Model;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace BankApplicationClientModule
{
    public class ClientModuleDataAccess : BaseClientModuleDataAccess
    {

        public ClientModuleDataAccess(string nameOfConnectionString) : base(nameOfConnectionString)
        {
        }

        /// <summary>
        /// TODO: change this function to meets requirements.
        /// </summary>
        /// <returns></returns>
        public IList<BankClient> GetAllClientsThatHaveAtLeastOneAccountDetached()
        {
            return DBContext.BankClients.Where(x => x.ClientAccounts.Count > 0)
                .Include(account => account.ClientAccounts).ToList();
        }

        /// <summary>
        /// TODO: implement this function to meets requirements.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool SaveNewClient(BankClient client)
        {
            if(client.Id > 0)
                throw new DataExistsException();

            try
            {
                foreach (var bankAccount in client.ClientAccounts)
                {
                    DBContext.BankAccounts.Add(bankAccount);
                }
                DBContext.BankClients.Add(client);

                var savedDBChanges =  DBContext.SaveChanges();

                if (savedDBChanges > 0)
                    return true;

                throw new WritingToDBException();
            }
            catch (WritingToDBException)
            {
                throw new WritingToDBException();
            }


        }

        /// <summary>
        /// TODO: implement this function to meets requirements.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public BankClient StartTracking(BankClient client)
        {
            var entity =  DBContext.ChangeTracker.Entries<BankClient>().FirstOrDefault(x => x.Entity.Id == client.Id);

            if (entity != null)
            {
                DBContext.Entry(entity.Entity).State = EntityState.Detached;
            }

            if (client.Id > 0)
                DBContext.Entry(client).State = EntityState.Added;
            else if (client.Id <= 0)
                DBContext.Entry(client).State = EntityState.Modified;

            return client;
        }

        /// <summary>
        /// TODO: implement this function to meets requirements.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool IsClientTrackedByEF(BankClient client)
        {
            var entity =  DBContext.ChangeTracker.Entries<BankClient>().FirstOrDefault(x => x.Entity.Id == client.Id);

            return entity != null;
        }
    }
}
