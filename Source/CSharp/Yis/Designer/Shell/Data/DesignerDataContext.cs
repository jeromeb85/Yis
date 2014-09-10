using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Yis.Designer.Data.Contract;
using Yis.Designer.Model;
using Yis.Framework.Data.EntityFramework;

namespace Yis.Designer.Data
{
    public class DesignerDataContext : DataContextBase, IDesignerDataContext
    {
        public DesignerDataContext()
            : base("Yis")
        {
            //DependencyResolverManager.Default.Register<IWorkSpaceProvider>(new WorkSpaceProvider(this));
            // Database.SetInitializer<YisDbContext>(new CreateDatabaseIfNotExists<YisDbContext>());

            //Database.SetInitializer<YisDbContext>(new DropCreateDatabaseIfModelChanges<YisDbContext>());
            //Database.SetInitializer<YisDbContext>(new DropCreateDatabaseAlways<YisDbContext>());
            //Database.SetInitializer<YisDbContext>(new SchoolDBInitializer());
        }

        public DbSet<WorkSpace> WorkSpace { get; set; }

        //public DbSet<AspectSemantic> AspectSemantic { get; set; }
        //public DbSet<DomainSemantic> DomainSemantic { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<WorkSpace>().HasKey<Guid>(t => t.Id);

            //modelBuilder.Entity().HasRequired(d => d.Administrator).WithMany().WillCascadeOnDelete(false);
            /*         modelBuilder.Entity<Course>()
             .HasMany(c => c.Instructors).WithMany(i => i.Courses)
             .Map(t => t.MapLeftKey("CourseID")
                 .MapRightKey("InstructorID")
                 .ToTable("CourseInstructor"));*/

            //modelBuilder.Entity<Instructor>()
            //    .HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);
        }
    }
}