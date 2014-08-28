using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Model;
using Yis.Framework.Core.IoC;
using Yis.Framework.Data.EntityFramework;

namespace Yis.Designer.Data
{
    public class YisDesignerDataContext : DataContextBase
    {

        public YisDesignerDataContext()
            : base("Yis")
        {
            DependencyResolver.Register<IWorkSpaceProvider>(new WorkSpaceProvider(this));
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
