using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Model;

namespace Yis.Designer.Data
{
    public class YisDesignerInitializer : DropCreateDatabaseAlways<YisDesignerDbContext>
    {
        protected override void Seed(YisDesignerDbContext context)
        {
            Guid IdWorkSpace = Guid.NewGuid();

            var WorkSpace = new List<WorkSpace>
            {
                new WorkSpace{Id= IdWorkSpace, Name="TestMaster", AspectSemantic = new AspectSemantic { Id = Guid.NewGuid() }},
            };
            
            WorkSpace.ForEach(s => context.WorkSpace.Add(s));
            context.SaveChanges();






            //var Groupe = new List<Groupe>
            //{
            //new Groupe{Id= Guid.NewGuid(), Libelle="durudu"},
            //};
            //Groupe.ForEach(s => context.Groupe.Add(s));
            //context.SaveChanges();
        }

    }
}
