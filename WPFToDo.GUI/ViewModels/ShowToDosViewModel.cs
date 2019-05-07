using Stylet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToDo.Common;
using WPFToDo.Database;
using WPFToDo.GUI.Events;

namespace WPFToDo.GUI.ViewModels
{
    public enum ShowingToDos { All, Open, Closed, No_Selection, Found };

    public class ShowToDosViewModel : Screen
    {
        private IToDoStore _toDoStore;

        IEventAggregator _eventAggregator = null;

        

        protected string _searchText = string.Empty;
        /// <summary>
        /// Current search string text
        /// </summary>
        public string SearchText{ get { return _searchText; }
                                  set { SetAndNotify(ref this._searchText, value); }
                                    }

        /// <summary>
        /// Parameterless constructor only used for design time data 
        /// </summary>
        public ShowToDosViewModel()
        {
            _toDos = new BindableCollection<ToDo>();
            _toDos.Add(new ToDo() { Title = "Todo Title 1", Description = "This is the description for the first todo", Complete = false });
            _toDos.Add(new ToDo() { Title = "Todo Title 2", Description = "This is the description for the second todo", Complete = false });
            _toDos.Add(new ToDo() { Title = "Todo Title 3", Description = "This todo has been completed", Complete = true });
        }

        public ShowToDosViewModel(IToDoStore todoStore, IEventAggregator eventAggregator)
        {
            _toDoStore = todoStore;
            _eventAggregator = eventAggregator;
            ShowToDosBasedOnSelection();
        }

        protected string _currentToDoSelection;
        public string CurrentToDoSelection
        {
            get { return $"Current selection:{_currentToDoSelection}"; }
            set { SetAndNotify(ref _currentToDoSelection, value);  }
           
        }

        protected ShowingToDos _viewState = ShowingToDos.Open;

        public ShowingToDos ToDoDisplayState
        {
            get { return _viewState; }
            set {
                SetAndNotify(ref this._viewState, value);
                }
        }


        private BindableCollection<ToDo> _toDos;

        public BindableCollection<ToDo> ToDos
        {
            get { return _toDos; }
            //set { _toDos = value; }
            set { SetAndNotify(ref this._toDos, value); }
        }

        public void LoadAllToDos()
        {
            ToDos = new BindableCollection<ToDo>(_toDoStore.GetAllToDos());
            ToDoDisplayState = ShowingToDos.All;
            CurrentToDoSelection = "All";
        }

        public void LoadOpenToDos()
        {
            ToDos = new BindableCollection<ToDo>(_toDoStore.GetAllOpenToDos());
            ToDoDisplayState = ShowingToDos.Open;
            CurrentToDoSelection = "Open";
        }

        public void LoadClosedToDos()
        {
            ToDos = new BindableCollection<ToDo>(_toDoStore.GetAllClosedToDos());
            ToDoDisplayState = ShowingToDos.Closed;
            CurrentToDoSelection = "Closed";
        }

        public void SetToDoCompletionState(ToDo toDo)
        {
            Debug.WriteLine("SetToDoAsComplete clicked");

            _toDoStore.UpdateToDo(toDo);

            ShowToDosBasedOnSelection();
        }

        public void Search()
        {
            if (SearchText == string.Empty)
            {
                LoadAllToDos();
            }
            else
            {
                ToDos = new BindableCollection<ToDo>(_toDoStore.Search(SearchText));
            }
        }

        public void ClearSearch()
        {
            SearchText = "";

            ShowToDosBasedOnSelection();
        }

        protected void ShowToDosBasedOnSelection()
        {
            switch (ToDoDisplayState)
            {
                case ShowingToDos.Open:
                    LoadOpenToDos();
                    break;
                case ShowingToDos.Closed:
                    LoadClosedToDos();
                    break;
                case ShowingToDos.All:
                    LoadAllToDos();
                    break;
                case ShowingToDos.Found:
                    Search();
                    break;
                default:
                    LoadAllToDos();
                    break;

            }
        }

        protected override void OnViewLoaded()
        {
            //base.OnViewLoaded();
            //LoadAllToDos();
        }


        public void EditToDo(ToDo t)
        {
            _eventAggregator.Publish(new EditToDoEvent(t));   
        }
    }
}
