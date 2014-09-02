﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yis.Designer.Business;
using Yis.Designer.Data;
using Yis.Designer.Model;
using Yis.Designer.Presentation;
using Yis.Designer.Presentation.View;
using Yis.Framework;
using Yis.Framework.Core;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Logging;
using Yis.Framework.Data;
using Yis.Framework.Presentation.Locator;
using Yis.Framework.Core.Rule;
using Yis.Framework.Helper;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Logging;
using Yis.Framework.Presentation.Locator.Contract;

namespace Yis
{
    class Program
    {


        //--private static YisDesignerDbContext db = ;
        [STAThread]
        static void Main(string[] args)
        {
            Boot.Start();
            //RunConsole();
            //RunWindows();
        }


        private static void RunWindows()
        {

            App.Main();
        }

        private static string _toto;
        public static string Toto { get { return _toto; } set { _toto = value; } }



        private static void tata(Expression<Func<object>> func, Func<WorkSpace, bool> func2)
        {

        }
        private static void RunConsole()
        {
            LogManager.Default.Debug("toto");

            ConsoleHelper.ShowConsoleWindow();

            IViewModelLocator vml = new ViewModelLocator();

            //Console.WriteLine(vml.ResolveViewModel(typeof(MainWindow)));

            ServiceLocator loc = new ServiceLocator();

            loc.Build();

            foreach(Type item in loc.ResolveType(typeof(IDependencyResolver)))
            {
                Console.WriteLine(item.Name);
            }

            //WorkSpace ws = new WorkSpace();
            //ws.Name = "12345";

            //WorkSpace ws2 = new WorkSpace();
            //ws2.Name = "123888888";

            // RuleValidator<WorkSpace> rv = new RuleValidator<WorkSpace>();
            // rv.AddRuleAnnotation();
            // rv.AddRule((t) => t.Name, (t) => { return t.Name == "12345"; }, "C moche");
            //// tata(() => Toto, (w) => { return w.Name =="rr" } );
            // ws.Name = "12345";
            //// ws = null;


            // foreach (var item in rv.Validate(ws2,(t) => t.Name))
            // {
            //     Console.WriteLine(item.ErrorMessage);
            // }


            //DependencyResolver.Register<IDataContext>("YisDataContext", new YisDesignerDataContext());

            //WorkSpaceManager wsm = new WorkSpaceManager();

            //foreach (var item in wsm.GetAll())
            //{
            //    Console.WriteLine(item.Name);
            //}


            //using (UnitOfWork uow = new UnitOfWork("YisDataContext"))
            //{

            //    IWorkSpaceProvider _workSpaceProvider = uow.GetRepository<IWorkSpaceProvider>();
            //    WorkSpace newWS = _workSpaceProvider.Create();

            //    newWS.Id = Guid.NewGuid();
            //    newWS.Name = "toto to to";

            //    _workSpaceProvider.Add(newWS);
            //    uow.SaveChanges();

            //    foreach (var item in _workSpaceProvider.GetAll())
            //    {
            //        Console.WriteLine(item.Name);


            //        //db.Entry(WorkSpace).Reference(WorkSpace.AspectSemantic).Load();

            //        //Console.WriteLine(item.AspectSemantic.Id.ToString());
            //        //  Console.WriteLine(item.AspectSemantic.WorkSpace.Id.ToString());
            //    }
            //}



            Console.ReadLine();
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

            ConsoleHelper.HideConsoleWindow();
        }






    }

    
}