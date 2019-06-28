using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Common;
using WPFToDo.Database;
using WPFToDo.GUI.Events;

namespace WPFToDo.GUI.ViewModels
{
    public class AddToDoViewModel : Screen
    {

        protected IToDoStore _toDoStore;

        private string _title = string.Empty;
        private string _description = string.Empty;

        IEventAggregator _eventAggregator = null;

        public AddToDoViewModel(IToDoStore todoStore,IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _toDoStore = todoStore;
        }

        public void SetToDoComplete()
        {
            _toDo.Complete = !_toDo.Complete;
        }

        //public bool ToDoComplete
        //{
        //    get { return _toDo.Complete; }
        //    set {
        //        _toDo.Complete = value;

        //        SetAndNotify(ref ToDoComplete, _toDo.Complete);
        //    }
        //}


        private ToDo _toDo;

        private bool newToDo = false;

        public ToDo ToDo
        {
            get { return _toDo; }
            set
            {
                if (value == null)
                {
                    newToDo = true;
                    _toDo = new ToDo();
                }
                else
                {
                    newToDo = false;
                    _toDo = value;
                }
                //SetAndNotify(ref this._toDo, _toDo);
               
            }
        }

        
        public string Title
        {
            get { return _toDo.Title; }
            set {
                _toDo.Title = value;
                SetAndNotify(ref this._title, _toDo.Title);
            }
        }


        public String Description
        {
            get { return _toDo.Description;  }
            set {
                _toDo.Description = value;
                SetAndNotify(ref this._description, _toDo.Description);
                }
        }


        public void SaveToDo()
        {
            if (!string.IsNullOrEmpty(Title))
            {
                if(newToDo)
                { 
                    _toDoStore.AddToDo(ToDo);
                }
                else
                {
                    _toDoStore.UpdateToDo(ToDo);
                }
            }
            else
            {
                throw new ArgumentException("The title cannot be blank");
            }

            _eventAggregator.Publish(new ShowToDosEvent());
            
        }

        public void CancelToDo()
        {
            _eventAggregator.Publish(new ShowToDosEvent());
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            Title = _toDo.Title;
            Description = _toDo.Description;
        
        }
    }
}
