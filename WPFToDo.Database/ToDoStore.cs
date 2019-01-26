using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Common;
using System.Data;

namespace WPFToDo.Database
{
    /// <summary>
    /// Any store of ToDos must implement this interface. It contains the contract of operations that any ToDo store 
    /// must implement.
    /// </summary>
    public abstract class ToDoStore
    {
        protected IDbConnection connection;

        public ToDoStore(IDbConnection con)
        {
            connection = con;
        }

        /// <summary>
        /// Get all ToDos regardless of completion status
        /// </summary>
        /// <returns></returns>
        public abstract List<ToDo> GetAllToDos();

        public abstract List<ToDo> GetAllOpenTodos();

        public abstract List<ToDo> GetAllClosedTodos();

        public abstract ToDo GetToDoById(int id);

        /// <summary>
        /// Creates a new ToDo in the store and returns the id of the newly created ToDo
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public abstract int AddTodo(ToDo t);

        public abstract bool UpdateTodo(ToDo t);

        public abstract bool DeleteTodo(int id);
        
    }
}
