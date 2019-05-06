﻿using System.Collections.Generic;
using WPFToDo.Common;

namespace WPFToDo.Database
{
    public interface IToDoStore
    {
        ToDo AddToDo(ToDo t);
        bool DeleteToDo(ToDo t);
        List<ToDo> GetAllClosedToDos();
        List<ToDo> GetAllOpenToDos();
        List<ToDo> GetAllToDos();
        List<ToDo> Search(string searchTerm);
        ToDo GetToDoById(int id);
        bool UpdateToDo(ToDo t);

    }
}