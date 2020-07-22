using BankApplicationClientModule.Model;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace BankApplicationClientModule
{
    /// <summary>
    /// You cannot change this class, methods and properties in it!
    /// </summary>
    public class ClientModuleDBContext: DbContext
    {
        public ClientModuleDBContext(string nameOfConnectionString) : base(nameOfConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                .HasRequired<BankClient>(s => s.Client)
                .WithMany(g => g.ClientAccounts)
                .HasForeignKey<int>(s => s.ClientId);

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ClientModuleDBContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankClient> BankClients { get; set; }


        public void ClearAllAData()
        {
            this.Database.ExecuteSqlCommand($"delete from {nameof(BankAccount)}");
            this.Database.ExecuteSqlCommand($"delete from {nameof(BankClient)}");
        }
    }
}
