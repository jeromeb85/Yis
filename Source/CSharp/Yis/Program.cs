using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Yis.Designer.Model;
using Yis.Designer.Presentation;
using Yis.Designer.Software.Business;
using Yis.Designer.Technic.Internal;
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
    internal class Program
    {
        #region Fields

        private static string _toto;

        #endregion Fields

        #region Properties

        public static string Toto { get { return _toto; } set { _toto = value; } }

        #endregion Properties

        #region Methods

        //--private static YisDesignerDbContext db = ;
        [STAThread]
        private static void Main(string[] args)
        {
            Boot.Start();
            RunGenerator();
            //RunConsole();
            //RunWindows();
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

        private static void RunGenerator()
        {
            ConsoleHelper.ShowConsoleWindow();

            InitializeData.Run();

            //NameSpaceCollection manag = new NameSpaceCollection();
            //NameSpace ns = manag.AddNew();

            //ns.Id = Guid.NewGuid();
            //ns.Name = "Yis : " + ns.Id.ToString();

            //manag.Save();

            /* NameSpaceCollection manag = new NameSpaceCollection();

             NameSpace root = manag.Create();

             root.Id = Guid.NewGuid();
             root.Name = "Yis 2";

             manag.Add(root);*/

            foreach (var item in NameSpaceCollection.GetAll())
            {
                Console.WriteLine(item.Name);
            }

            Generator gen = new Generator();
            gen.Generate(NameSpace.GetByName("Yis"), @"D:\TestGen\");

            //
            //NameSpace root = new NameSpace() { Id = Guid.NewGuid(), Name = "Yis", ChrildrenNameSpace = new List<NameSpace>() };
            //NameSpace child = new NameSpace() { Id = Guid.NewGuid(), Name = "Sample", Class = new List<Class>() };
            //Class test = new Class() { Id = Guid.NewGuid(), Name = "testclass", NameSpaceId = child.Id };

            //root.ChrildrenNameSpace.Add(child);
            //child.Class.Add(test);

            //gen.Generate(root, @"C:\TestGen");

            Console.ReadKey();
        }

        private static void RunWindows()
        {
            App.Main();
        }

        private static void tata(Expression<Func<object>> func, Func<WorkSpace, bool> func2)
        {
        }

        #endregion Methods
    }

    internal class test
    {
        #region Constructors

        public test()
        {
            IBus bus = BusManager.Default;
            bus.Publish<NotificationMessage>(new NotificationMessage(this, "supppper"));
        }

        #endregion Constructors
    }

    internal class test2
    {
        #region Constructors

        public test2()
        {
            IBus bus = BusManager.Default;
            bus.Subscribe<NotificationMessage>((e) => { OnMessage(e.Message); });
        }

        #endregion Constructors

        #region Methods

        private void OnMessage(string message)
        {
            Console.WriteLine(message);
        }

        #endregion Methods
    }
}