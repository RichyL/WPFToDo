using Stylet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Common;
using WPFToDo.Database;

namespace WPFToDo.GUI.ViewModels
{
    public class ShowToDosViewModel : Screen
    {
        private IToDoStore _toDoStore;

        /// <summary>
        /// Parameterless constructor only used for design time data 
        /// </summary>
        public ShowToDosViewModel()
        {
            _toDos = new List<ToDo>();
            _toDos.Add(new ToDo() { Title = "Todo Title 1", Description = "This is the description for the first todo", Complete = false });
            _toDos.Add(new ToDo() { Title = "Todo Title 2", Description = "This is the description for the second todo", Complete = false });
            _toDos.Add(new ToDo() { Title = "Todo Title 3", Description = "This todo has been completed", Complete = true });
        }

        public ShowToDosViewModel(IToDoStore todoStore)
        {
            _toDoStore = todoStore;
            LoadAllToDos();
        }

        private List<ToDo> _toDos;

        public List<ToDo> ToDos
        {
            get { return _toDos; }
            //set { _toDos = value; }
            set { SetAndNotify(ref this._toDos, value); }
        }

        public void LoadAllToDos()
        {
            ToDos = _toDoStore.GetAllToDos();
        }

        public void LoadOpenToDos()
        {
            ToDos = _toDoStore.GetAllOpenToDos();
        }

        public void Load()
        {
            LoadAllToDos();
        }

        public void LoadClosedToDos()
        {
            ToDos = _toDoStore.GetAllClosedToDos();
        }

        public void SetToDoAsComplete(ToDo toDo)
        {
            return;
        }

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            LoadAllToDos();
        }
    }
}
