using System;
using System.Linq.Expressions;
using Yis.Designer.Model;
using Yis.Designer.Presentation;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator;
using Yis.Framework.Core.Logging;
using Yis.Framework.Core.Messaging;
using Yis.Framework.Core.Messaging.Contract;
using Yis.Framework.Core.Messaging.Event;
using Yis.Framework.Core.Shell;
using Yis.Framework.Helper;
using Yis.Framework.Presentation.Locator;
using Yis.Framework.Presentation.Locator.Contract;

namespace Yis
{
    internal class test
    {
        public test()
        {
            IBus bus = BusManager.Default;
            bus.Publish<NotificationMessage>(new NotificationMessage(this, "supppper"));
        }
    }

    internal class test2
    {
        public test2()
        {
            IBus bus = BusManager.Default;
            bus.Subscribe<NotificationMessage>((e) => { OnMessage(e.Message); });
        }

        private void OnMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    internal class Program
    {
        //--private static YisDesignerDbContext db = ;
        [STAThread]
        private static void Main(string[] args)
        {
            Boot.Start();
            RunConsole();
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
            ConsoleHelper.ShowConsoleWindow();

            test tt = null;
            test2 uu = null;

            uu = new test2();
            tt = new test();

            Console.ReadLine();

            //LogManager.Default.Debug("toto");

            //ConsoleHelper.ShowConsoleWindow();

            //IViewModelLocator vml = new ViewModelLocator();

            ////Console.WriteLine(vml.ResolveViewModel(typeof(MainWindow)));

            //ServiceLocator loc = new ServiceLocator();

            //loc.Build();

            //foreach (Type item in loc.ResolveType(typeof(IDependencyResolver)))
            //{
            //    Console.WriteLine(item.Name);
            //}

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