using AngularAndWebApi.DBContext;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AngularAndWebApi.Models {

    public class AngularAndWebApiContext : DbContext {

        #region Fields (All of the DBContext's available DBSets)

        public DbSet<Area>          Areas    { get; set; }
        public DbSet<Dealer>        Dealers  { get; set; }
        public DbSet<Region>        Regions  { get; set; }
        public DbSet<Sale>          Sales    { get; set; }
        public DbSet<Staff>         Staffs   { get; set; }
        public DbSet<Vehicle>       Vehicles { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// DBContext constructor. Also registers the DBCommand Interceptor
        /// </summary>
        public AngularAndWebApiContext() : base("name=AngularAndWebApiContext") {
            DbInterception.Add(new MyDBCommandInterceptor());
        }

        #endregion

        #region Event Handlers

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

        #endregion

    }

}
