using System;
using System.Collections.Generic;
using System.Data.Entity;
using Yis.Designer.Model;

namespace Yis.Designer.Data
{
    public class DesignerInitializer : DropCreateDatabaseAlways<DesignerDataContext>
    {
        #region Methods

        protected override void Seed(DesignerDataContext context)
        {
            //            Guid IdWorkSpace = Guid.NewGuid();

            //            var WorkSpace = new List<WorkSpace>
            //            {
            ////                new WorkSpace{Id= IdWorkSpace, Name="TestMaster", AspectSemantic = new AspectSemantic { Id = Guid.NewGuid() }},
            //new WorkSpace{Id= IdWorkSpace, Name="TestMaster"},
            //            };

            //            WorkSpace.ForEach(s => context.WorkSpace.Add(s));
            //            context.SaveChanges();

            //var Groupe = new List<Groupe>
            //{
            //new Groupe{Id= Guid.NewGuid(), Libelle="durudu"},
            //};
            //Groupe.ForEach(s => context.Groupe.Add(s));
            //context.SaveChanges();
        }

        #endregion Methods
    }
}