﻿using Stylet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Common;
using WPFToDo.Database;

namespace WPFToDo.GUI.ViewModels
{
    public enum ShowingToDos { All, Open, Closed, No_Selection };

    public class ShowToDosViewModel : Screen
    {
        private IToDoStore _toDoStore;

        protected ShowingToDos _viewState = ShowingToDos.Open;

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

        public ShowToDosViewModel(IToDoStore todoStore)
        {
            _toDoStore = todoStore;
            ShowToDosBasedOnSelection();
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
            _viewState = ShowingToDos.All;
        }

        public void LoadOpenToDos()
        {
            ToDos = new BindableCollection<ToDo>(_toDoStore.GetAllOpenToDos());
            _viewState = ShowingToDos.Open;
        }

        public void LoadClosedToDos()
        {
            ToDos = new BindableCollection<ToDo>(_toDoStore.GetAllClosedToDos());
            _viewState = ShowingToDos.Closed;
        }

        public void SetToDoAsComplete(ToDo toDo)
        {
            Debug.WriteLine("SetToDoAsComplete clicked");
            toDo.Complete = true;
            _toDoStore.UpdateToDo(toDo);

            ShowToDosBasedOnSelection();
        }

        protected void ShowToDosBasedOnSelection()
        {
            switch (_viewState)
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
    }
}
