using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using XmlManager.Model;

namespace XmlManager.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("XmlStorage")
        {
        }

        public DbSet<XmlObject> Xmls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}