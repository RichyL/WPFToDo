using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFToDo.Common;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;

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
CREATE TABLE IF NOT EXISTS `ToDo` (
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

        public void Test_Connection_Closed()
        {
            CloseConnection();
            Assert.IsTrue(connection.State == ConnectionState.Closed);
        }

        public void Test_Connection_Opened()
        { 
            Assert.IsTrue(connection.State == ConnectionState.Open);
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

        [TestMethod]
        public void Test_Add_A_ToDo()
        {
            ToDo toDoFromDb= null;
            IToDoStore toDoStore= new DapperRespository(connection);
            toDoFromDb=toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" });

            Assert.AreSame("Title 1", toDoFromDb.Title);
            Assert.AreSame("Description 1", toDoFromDb.Description);
        }

        [TestMethod]
        public void Test_Update_A_ToDo()
        {
            //make a ToDo
            ToDo toDoFromTest = null;
            IToDoStore toDoStore = new DapperRespository(connection);
            toDoFromTest = toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" });

            toDoFromTest.Title = "Title 2";
            toDoFromTest.Description = "Description 2";

            toDoStore.UpdateToDo(toDoFromTest);

            List<ToDo> toDosFromDb = toDoStore.GetAllToDos();

            Assert.AreEqual(1, toDosFromDb.Count);

            Assert.AreEqual("Title 2", toDosFromDb[0].Title);
            Assert.AreEqual("Description 2", toDosFromDb[0].Description);
        }


        [TestMethod]
        public void Test_Set_ToDo_Complete()
        {
            //make a ToDo
            ToDo toDoFromTest = null;
            IToDoStore toDoStore = new DapperRespository(connection);
            toDoFromTest = toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" });

            toDoStore.UpdateToDo(toDoFromTest);

            List<ToDo> toDosFromDb = toDoStore.GetAllToDos();

            //test what was added to the database
            Assert.AreEqual("Title 1", toDosFromDb[0].Title);
            Assert.AreEqual("Description 1", toDosFromDb[0].Description);
            Assert.IsFalse(toDosFromDb[0].Complete);

            //set as complete
            toDosFromDb[0].Complete = true;

            toDoStore.UpdateToDo(toDosFromDb[0]);

            toDosFromDb = toDoStore.GetAllToDos();

            Assert.IsTrue(toDosFromDb[0].Complete);
        }

        [TestMethod]
        public void Test_Set_ToDo_Not_Complete()
        {
            //make a ToDo
            ToDo toDoFromTest = null;
            IToDoStore toDoStore = new DapperRespository(connection);
            toDoFromTest = toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" });
            toDoFromTest.Complete = true;
            toDoStore.UpdateToDo(toDoFromTest);

            List<ToDo> toDosFromDb = toDoStore.GetAllToDos();

            //test what was added to the database
            Assert.AreEqual("Title 1", toDosFromDb[0].Title);
            Assert.AreEqual("Description 1", toDosFromDb[0].Description);
            Assert.IsTrue(toDosFromDb[0].Complete);

            //set as complete
            toDosFromDb[0].Complete = false;

            toDoStore.UpdateToDo(toDosFromDb[0]);

            toDosFromDb = toDoStore.GetAllToDos();

            Assert.IsFalse(toDosFromDb[0].Complete);
        }

        [TestMethod]
        public void Delete_A_ToDo()
        {
            //make a ToDo
            ToDo toDoFromTest = null;
            IToDoStore toDoStore = new DapperRespository(connection);
            toDoFromTest = toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" });

            ///delete the todo and save to database
            Assert.IsTrue(toDoStore.DeleteToDo(toDoFromTest));

            List<ToDo> toDosFromDb = toDoStore.GetAllToDos();

            Assert.AreEqual(0, toDosFromDb.Count);
        }

        [TestMethod]
        public void Test_ToDos_Returned()
        {
            IToDoStore toDoStore = new DapperRespository(connection);
            toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" });
            toDoStore.AddToDo(new ToDo { Title = "Title 2", Description = "Description 2" });
            toDoStore.AddToDo(new ToDo { Title = "Title 3", Description = "Description 3" });
            toDoStore.AddToDo(new ToDo { Title = "Title 4", Description = "Description 4" });

            List<ToDo> toDosFromDb = toDoStore.GetAllToDos();
            Assert.AreEqual(4, toDosFromDb.Count);
        }

        [TestMethod]
        public void Test_Open_ToDos_Returned()
        {
            IToDoStore toDoStore = new DapperRespository(connection);
            List<ToDo> toDos = new List<ToDo>();

            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 2", Description = "Description 2" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 3", Description = "Description 3" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 4", Description = "Description 4" }));

            toDos[0].Complete = true;
            toDos[3].Complete = true;

            toDoStore.UpdateToDo(toDos[0]);
            toDoStore.UpdateToDo(toDos[3]);

            List<ToDo> toDosFromDb = toDoStore.GetAllOpenToDos();
            Assert.AreEqual(2, toDosFromDb.Count);
        }

        [TestMethod]
        public void Test_Closed_ToDos_Returned()
        {
            IToDoStore toDoStore = new DapperRespository(connection);
            List<ToDo> toDos = new List<ToDo>();

            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 2", Description = "Description 2" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 3", Description = "Description 3" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 4", Description = "Description 4" }));

            toDos[0].Complete = true;
            
            toDoStore.UpdateToDo(toDos[0]);

            List<ToDo> toDosFromDb = toDoStore.GetAllClosedToDos();
            Assert.AreEqual(1, toDosFromDb.Count);
            Assert.AreEqual("Title 1", toDosFromDb[0].Title);
        }

        [TestMethod]
        public void Test_Search()
        {
            IToDoStore toDoStore = new DapperRespository(connection);
            List<ToDo> toDos = new List<ToDo>();

            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 1", Description = "Description 1" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 2", Description = "Description 2" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 3", Description = "Description 3" }));
            toDos.Add(toDoStore.AddToDo(new ToDo { Title = "Title 4", Description = "Description 4" }));

            List<ToDo> foundToDos = toDoStore.Search("Description 1");
            Assert.AreEqual(1, foundToDos.Count);

            foundToDos = toDoStore.Search("Title");
            Assert.AreEqual(4, foundToDos.Count);

        }


    }
}
