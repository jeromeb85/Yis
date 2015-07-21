using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Yis.Designer.Conceptual.Business;
using Yis.Designer.Model;
using Yis.Designer.Presentation;
using Yis.Designer.Software.Business;
using Yis.Designer.Technic.Internal;
using Yis.Erp.Shell.Presentation;
using Yis.Framework.Core.Extension;
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
using Yis.WebCrawler;
using Yis.WebCrawler.ExcludeFilters;

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
           // RunGenerator();
            //RunConsole();
            RunWindows();
            //RunCrawler();
        }

        private static void RunConsole()
        {
            ConsoleHelper.ShowConsoleWindow();

            
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

            Directory.Delete(@"D:\DataYis\", true);
            InitializeData.Run();

            Transformator trans = new Transformator();
            DomainCollection.GetAll().ForEach((i) => trans.Transform(i));

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

        private static void RunCrawler()
        {
            ConsoleHelper.ShowConsoleWindow();

            var crawler = new Crawler
            {
                ExcludeFilters = new IExcludeFilter[]
                {
                    new ExcludeImagesFilter(),
                    new ExcludeTrackbacks(),
                    new ExcludeMailTo(),
                    new ExcludeHostsExcept(new[] { "nyqui.st" }),
                    new ExcludeJavaScript(), 
                    new ExcludeAnchors(), 
                }
            };

            crawler.OnCompleted += () =>
            {
                Console.WriteLine("[Main] Crawl completed!");
               // Environment.Exit(0);
            };

            crawler.OnPageDownloaded += page =>
            {
                Console.WriteLine("[Main] Downloaded page {0}", page.Url);

                // Write external links
                foreach (var link in page.Links)
                {
                    if (link.TargetUrl.Host != page.Url.Host)
                    {
                        Console.WriteLine("Found outbound link from {0} to {1}", page.Url, link.TargetUrl);
                    }
                }
            };

            crawler.Enqueue(new Uri("http://www.developpez.com/"));
            crawler.Start();

            Console.WriteLine("[Main] Crawler started.");
            Console.WriteLine("[Main] Press [enter] to abort.");
            Console.ReadLine();
        }


        #endregion Methods
    }

}