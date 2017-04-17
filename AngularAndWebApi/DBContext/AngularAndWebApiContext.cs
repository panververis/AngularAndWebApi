using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AngularAndWebApi.Models
{
    public class AngularAndWebApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AngularAndWebApiContext() : base("name=AngularAndWebApiContext"){}

        public DbSet<Area>          Areas    { get; set; }

        public DbSet<Dealer>        Dealers  { get; set; }

        public DbSet<Region>        Regions  { get; set; }

        public DbSet<Sale>          Sales    { get; set; }

        public DbSet<Staff>         Staffs   { get; set; }

        public DbSet<Vehicle>       Vehicles { get; set; }


        /// <summary>
        /// OnModelCreating override, used to remove the EF's default "OnDeleteCascade" conventions.
        /// That means that no "OnDeleteCascade" option will be enabled on the DB, as the Parent - Child related Entities' deletion will be handled manually
        /// ==> Best Practice, for both performance and Entity handling clarity reasons
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);

            // Disabling all OnDeleteCascade options
            ModelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            ModelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

        }

    }

}
