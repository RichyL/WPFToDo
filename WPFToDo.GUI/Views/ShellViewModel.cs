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

            
            /*Worth mentioning that had a devil of a job tryng to connect to a database in a folder (wanted to make a connection string ar runtime)
             * so if the app was run from different machines then it would work. Originally had ToDo.db in a folder called DatabaseFile. Took it out of there
             * and had it in the same folder as the executable and it works alright.
            */
            IDbConnection connection = new SQLiteConnection("Data Source=ToDo.db;Version=3;");

            IToDoStore todoStore = new DapperRespository(connection);


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
