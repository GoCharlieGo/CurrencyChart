using CurrencyChart.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CurrencyChart.DAL
{
    public class CurrencyChartContext : DbContext
    {

        public CurrencyChartContext() : base("CurrencyChart2Context")
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction>Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}