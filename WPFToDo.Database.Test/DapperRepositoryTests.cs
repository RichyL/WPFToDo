using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFToDo.Common;
using System.Data;
using System.Data.SQLite;

namespace WPFToDo.Database.Test
{
    [TestClass]
    public class DapperRepositoryTests
    {
        public IDbConnection connection;

        public void MakeTestConnection()
        {
            if (connection != null) return;
            SQLiteConnectionStringBuilder conStringBuilder = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };
            connection = new SQLiteConnection(conStringBuilder.ToString());
            connection.Open();
        }

        public string setUpSQL = @"BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `Todo` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`Title`	TEXT NOT NULL,
	`Description`	TEXT,
	`DateTime`	NUMERIC NOT NULL,
	`Complete`	NUMERIC NOT NULL
);
COMMIT;";

        public string tearDownSQL = @"BEGIN TRANSACTION; DELETE FROM ToDo; COMMIT;";

        public  void CloseConnection()
        {
            connection.Close();
        }

        [TestMethod]
        public void Test_Connection_Opened()
        { 
            Assert.IsTrue(connection.State == ConnectionState.Open);
        }

        [TestMethod]
        public void Test_Connection_Closed()
        {
            CloseConnection();
            Assert.IsTrue(connection.State == ConnectionState.Closed);
        }

        /// <summary>
        /// 
        /// </summary>
        public  DapperRepositoryTests()
        {
            MakeTestConnection();

            IDbCommand command = connection.CreateCommand();
            command.CommandText= setUpSQL;
            command.ExecuteNonQuery();
        }

       // [TestCleanup]
        public void Tear_Down_ToDo_Table()
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = tearDownSQL;
            command.ExecuteNonQuery();
        }

        //[TestMethod]
        public void Add_A_ToDo()
        {
            ToDo toDo = new ToDo();
            toDo.Title = "ToDo1 Title";
            toDo.Description = "ToDo1 Description";
            ToDoStore toDoStore= new DapperRespository(connection);
            int newId=toDoStore.AddTodo(toDo);

            ToDo toDoFromDb = toDoStore.GetToDoById(newId);

            Assert.AreEqual(toDo, toDoFromDb);
        }

        //[TestMethod]
        public void Add_A_ToDo1()
        {
            ToDo toDo = new ToDo();
        }

    }
}
