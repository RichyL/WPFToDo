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
    public class ShowToDosViewModel : BaseViewModel, IContentViewModel
    {
        private IToDoStore _toDoStore;

        public ShowToDosViewModel(IToDoStore todoStore)
        {
            _toDoStore = todoStore;
        }

        private List<ToDo> _toDos;

        public List<ToDo> ToDos
        {
            get { return _toDos; }
            set { _toDos = value; }
        }

        public void LoadAllToDos()
        {
            _toDos = _toDoStore.GetAllToDos();
        }

        public void LoadOpenToDos()
        {
            _toDos = _toDoStore.GetAllOpenToDos();
        }

        public void Load()
        {
            
        }
    }
}
