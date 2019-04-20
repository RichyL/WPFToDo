using System;
using Stylet;
using WPFToDo.GUI.Events;
using WPFToDo.Database;
using WPFToDo.GUI.ViewModels;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace WPFToDo.GUI.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<PageChangedEvent>
    {
        public TopBarViewModel TopBar { get; private set; }

        public MenuViewModel Menu { get; private set; }


        public ShellViewModel(TopBarViewModel tBarVM, MenuViewModel menuVM, IEventAggregator eventAggregator
            )
        {
            eventAggregator.Subscribe(this);
            TopBar = tBarVM;
            Menu=menuVM;
            TopBar.PageTitle = "Text set from the ShellViewModel";



            //TODO - do these need to be newe'd up in here?
            //Could have them passed in via IoC container. What is the benefit?


            //IDbConnection connection = new SQLiteConnection(
            //                ConfigurationManager.AppSettings["SQLiteConnectionString"]
            //    );

            //string pathname = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "DatabaseFile", "ToDo.db");
            //pathname.Replace(@"\\",@"\");

            string pathname = @"C:\Users\Rich Latham\github\WPFToDo\WPFToDo.GUI\bin\Debug\DatbaseFile\ToDo.db";

            IDbConnection connection = new SQLiteConnection(
                
                string.Format("Data Source={0};Version=3;",pathname ));

            IToDoStore todoStore= new DapperRespository(connection);

            AddPage("Show ToDos", new ShowToDosViewModel( todoStore ));
            //AddPage("Page 2", new Page2ViewModel());
            AddPage("Add ToDo", new AddToDoViewModel(todoStore));

            
        }

        public void Handle(PageChangedEvent pge)
        {
            //RICHYL - set to null so as to prompt new OnLoad event in activated view model
            this.ActiveItem = null;
            this.ActiveItem = pge.NewPage;
        }

        protected void AddPage(string pageName, IScreen page)
        {
            this.Items.Add(page);
            Menu.AddPage(pageName, page);
        }


    }
}
