using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Common;
using Dapper;
using System.Diagnostics;
using System.IO;
using System.Data.SQLite;

namespace WPFToDo.Database
{
    /// <summary>
    /// A store for ToDos implemented by a SQLite database and
    /// using Dapper as the MicroORM
    /// </summary>
    public class DapperRespository : IToDoStore
    {
        protected IDbConnection connection;
        

        public DapperRespository(IDbConnection con)
        {
           
            connection = con;

            //connection = new SQLiteConnection(@"Data Source=C:\Users\Rich Latham\github\WPFToDo\WPFToDo.Database\bin\Debug\DatbaseFile\ToDo.db;Version=3;");
        }

        public  ToDo AddToDo(string title, string description)
        {
            ToDo toDo = new ToDo();
            toDo.Title = title;
            toDo.Description = description;
            toDo.Complete = false;



            var sql = "INSERT INTO ToDo(Title, Description, DateTime, Complete) VALUES(@Title, @Description, @DateTime, @Complete); " +
                 "SELECT Id FROM ToDo WHERE Title=@Title AND Description=@Description;";
            var id = this.connection.Query<int>(sql, toDo).Single();
            toDo.Id = id;
            return toDo;
        }

        public  bool DeleteToDo(ToDo t)
        {
            return this.connection.Execute("DELETE FROM ToDo WHERE Id = @Id", t)==1;
        }

        public  List<ToDo> GetAllClosedToDos()
        {
            return this.connection.Query<ToDo>("SELECT * FROM ToDo WHERE Complete=1").ToList();
        }

        public  List<ToDo> GetAllOpenToDos()
        {
            return this.connection.Query<ToDo>("SELECT * FROM ToDo WHERE Complete=0").ToList();
        }

        public  List<ToDo> GetAllToDos()
        {
            return this.connection.Query<ToDo>("SELECT * FROM ToDo").ToList();
        }

        public  ToDo GetToDoById(int id)
        {
            throw new NotImplementedException();
        }

        public  bool UpdateToDo(ToDo t)
        {
            var sql ="UPDATE ToDo SET Title = @Title, Description = @Description, Complete= @CompleteAsNumber WHERE Id = @Id";
            return this.connection.Execute(sql, t)==1;
        }
    }
}
