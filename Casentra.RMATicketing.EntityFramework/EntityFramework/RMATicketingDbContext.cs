using System.Data.Common;
using Abp.Zero.EntityFramework;
using Casentra.RMATicketing.Authorization.Roles;
using Casentra.RMATicketing.MultiTenancy;
using Casentra.RMATicketing.Users;
using System.Data.Entity;
using Casentra.RMATicketing.Tickets;
using Casentra.RMATicketing.Customers;
using Casentra.RMATicketing.Brands;
using Casentra.RMATicketing.Products;
using Casentra.RMATicketing.Colors;
using Casentra.RMATicketing.Capacities;
using Casentra.RMATicketing.Accessories;
using Casentra.RMATicketing.PhoneConditions;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.BoughtAts;
using Casentra.RMATicketing.Spares;
using Casentra.RMATicketing.BatchItems;
using Casentra.RMATicketing.BatchTickets;
using Casentra.RMATicketing.Notes;
using Casentra.RMATicketing.IMEI;

namespace Casentra.RMATicketing.EntityFramework
{
    public class RMATicketingDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public RMATicketingDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in RMATicketingDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of RMATicketingDbContext since ABP automatically handles it.
         */
        public RMATicketingDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public RMATicketingDbContext(DbConnection connection)
            : base(connection, true)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Capacity> Capacities { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<PhoneCondition> PhoneConditions { get; set; }
        public DbSet<PhoneProblem> PhoneProblems { get; set; }
        public DbSet<BoughtAt> BoughtAts { get; set; }
        public DbSet<Spare> Spares { get; set; }

        public DbSet<BatchTicket> BatchTickets { get; set; }
        public DbSet<BatchItem> BatchItems { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<IMEINumber> IMEINumbers { get; set; }

        
    }
}
