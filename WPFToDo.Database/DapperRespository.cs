using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Common;
using Dapper;

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

        public override ToDo AddToDo(string title, string description)
        {
            ToDo toDo = new ToDo();
            toDo.Title = title;
            toDo.Description = description;

            var sql = "INSERT INTO ToDo(Title, Description, DateTime, Complete) VALUES(@Title, @Description, @DateTime, @Complete); " +
                 "SELECT Id FROM ToDo WHERE Title=@Title AND Description=@Description;";
            var id = this.connection.Query<int>(sql, toDo).Single();
            toDo.Id = id;
            return toDo;
        }

        public override bool DeleteToDo(ToDo t)
        {
            return this.connection.Execute("DELETE FROM ToDo WHERE Id = @Id", t)==1;
        }

        public override List<ToDo> GetAllClosedToDos()
        {
            return this.connection.Query<ToDo>("SELECT * FROM ToDo WHERE Complete=1").ToList();
        }

        public override List<ToDo> GetAllOpenToDos()
        {
            return this.connection.Query<ToDo>("SELECT * FROM ToDo WHERE Complete=0").ToList();
        }

        public override List<ToDo> GetAllToDos()
        {
            return this.connection.Query<ToDo>("SELECT * FROM ToDo").ToList();
        }

        public override ToDo GetToDoById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateToDo(ToDo t)
        {
            var sql ="UPDATE ToDo SET Title = @Title, Description = @Description, Complete= @Complete WHERE Id = @Id";
            return this.connection.Execute(sql, t)==1;
        }
    }
}
