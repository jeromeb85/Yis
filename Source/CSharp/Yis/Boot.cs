using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Data;
using Yis.Designer.Model;
using Yis.Designer.Presentation;
using Yis.Framework.Core;
using Yis.Framework.Core.Tracer;

namespace Yis
{
    class Boot
    {       


        //--private static YisDesignerDbContext db = ;
        [STAThread]
        static void Main(string[] args)
        {
            YisSystem.Boot();
            Trace.Debug("toto");

            YisSystem.ShowConsoleWindow();
            /*
            using (var db = new YisDesignerDbContext())
            {
                var query = from it in db.WorkSpace
                            select it;

                Console.WriteLine("tentative 1");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);


                    //db.Entry(WorkSpace).Reference(WorkSpace.AspectSemantic).Load();

                    Console.WriteLine(item.AspectSemantic.Id.ToString());
                    //  Console.WriteLine(item.AspectSemantic.WorkSpace.Id.ToString());
                }

                Console.WriteLine("tentative 2");
                foreach (WorkSpace WorkSpace in db.WorkSpace.Include("AspectSemantic"))
                {
                    Console.WriteLine(WorkSpace.Name);


                    //db.Entry(WorkSpace).Reference(WorkSpace.AspectSemantic).Load();

                    Console.WriteLine(WorkSpace.AspectSemantic.Id.ToString());
                    //Console.WriteLine(WorkSpace.AspectSemantic.WorkSpace.Id.ToString());


                }

            }

            Console.ReadKey();*/

            YisSystem.HideConsoleWindow();
            RunWindows();
        }


        private static void RunWindows()
        {
            
            App.Main();
        }



    }
}
