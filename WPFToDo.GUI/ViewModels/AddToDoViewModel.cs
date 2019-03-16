using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Common;
using WPFToDo.Database;

namespace WPFToDo.GUI.ViewModels
{
    public class AddToDoViewModel : Screen
    {

        protected IToDoStore _toDoStore;

        private string _title = string.Empty;
        private string _description = string.Empty;

        public AddToDoViewModel(IToDoStore todoStore)
        { 
            _toDoStore = todoStore;
        }
        

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }


        public ToDo SaveToDo()
        {
            if (!string.IsNullOrEmpty(_title))
            {
                return _toDoStore.AddToDo(_title, _description);
                
            }
            else
            {
                throw new ArgumentException("The title cannot be blank");
            }
            
        }

    }
}
