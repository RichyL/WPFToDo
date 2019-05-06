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

        private ToDo _toDo;

        public ToDo ToDo
        {
            get { return _toDo; }
            set
            {
                if (value == null)
                {
                    _toDo = new ToDo();
                }
                else
                {
                    _toDo = value;
                }
            }
        }

        
        public string Title
        {
            get { return _toDo.Title; }
            set { _toDo.Title = value; }
        }


        public String Description
        {
            get { return _toDo.Description;  }
            set { _toDo.Description = value; }
        }


        public ToDo SaveToDo()
        {
            if (!string.IsNullOrEmpty(_title))
            {
                return _toDoStore.AddToDo(ToDo);
                
            }
            else
            {
                throw new ArgumentException("The title cannot be blank");
            }
            
        }

    }
}
