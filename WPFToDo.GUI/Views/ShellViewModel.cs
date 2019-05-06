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
   

    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive , IHandle<EditToDoEvent>, IHandle<ShowToDosEvent>
    {
        

        public TopBarViewModel TopBar { get; private set; }

        protected ShowToDosViewModel showToDos = null;

        protected AddToDoViewModel addToDos = null;
       
        public ShellViewModel(TopBarViewModel tBarVM,  IEventAggregator eventAggregator
            )
        {
            eventAggregator.Subscribe(this);
            



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


            showToDos = new ShowToDosViewModel(todoStore,eventAggregator);
            addToDos = new AddToDoViewModel(todoStore,eventAggregator);
            //Items.Add(showToDos);

            ActiveItem=showToDos;
        }

        public void Handle(EditToDoEvent e)
        {
                addToDos.ToDo = e.ToDo;
                ActiveItem = addToDos;
        }

        public void Handle(ShowToDosEvent message)
        {
            ActiveItem = showToDos;
        }
    }
}
