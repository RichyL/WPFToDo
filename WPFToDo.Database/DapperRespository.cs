using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Common;

namespace WPFToDo.Database
{
    /// <summary>
    /// A store for ToDos implemented by a SQLite database and
    /// using Dapper as the MicroORM
    /// </summary>
    public class DapperRespository : ToDoStore
    {

        public DapperRespository(IDbConnection con) : base (con)
        {

        }

        public override int AddTodo(ToDo t)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteTodo(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ToDo> GetAllClosedTodos()
        {
            throw new NotImplementedException();
        }

        public override List<ToDo> GetAllOpenTodos()
        {
            throw new NotImplementedException();
        }

        public override List<ToDo> GetAllToDos()
        {
            throw new NotImplementedException();
        }

        public override ToDo GetToDoById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateTodo(ToDo t)
        {
            throw new NotImplementedException();
        }
    }
}
