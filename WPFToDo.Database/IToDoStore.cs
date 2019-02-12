using System.Collections.Generic;
using WPFToDo.Common;

namespace WPFToDo.Database
{
    public interface IToDoStore
    {
        ToDo AddToDo(string title, string description);
        bool DeleteToDo(ToDo t);
        List<ToDo> GetAllClosedToDos();
        List<ToDo> GetAllOpenToDos();
        List<ToDo> GetAllToDos();
        ToDo GetToDoById(int id);
        bool UpdateToDo(ToDo t);
    }
}