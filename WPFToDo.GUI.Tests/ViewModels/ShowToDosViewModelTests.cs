using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Database;
using WPFToDo.GUI.ViewModels;
using Xunit;
using Moq;
using WPFToDo.Common;

namespace WPFToDo.GUI.Tests.ViewModels
{
    public class ShowToDosViewModelTests
    {

        ShowToDosViewModel vm = null;

        public ShowToDosViewModelTests()
        {
            var toDoStoreMock = new Mock<IToDoStore>();
            toDoStoreMock.Setup(x => x.GetAllToDos())
                .Returns(new List<ToDo>
                {
                    new ToDo { Id = 1, Title = "Title 1", Description = "Description 1", Complete = false },
                    new ToDo { Id = 2, Title = "Title 2", Description = "Description 2", Complete = false },
                    new ToDo { Id = 3, Title = "Title 3", Description = "Description 3", Complete = true },
                    new ToDo { Id = 4, Title = "Title 4", Description = "Description 4", Complete = true }
                });

            toDoStoreMock.Setup(x => x.GetAllOpenToDos())
                .Returns(new List<ToDo>
                {
                    new ToDo { Id = 1, Title = "Title 1", Description = "Description 1", Complete = false },
                    new ToDo { Id = 2, Title = "Title 2", Description = "Description 2", Complete = false }
                });

            toDoStoreMock.Setup(x => x.GetAllClosedToDos())
              .Returns(new List<ToDo>
              {
                    new ToDo { Id = 3, Title = "Title 3", Description = "Description 3", Complete = true },
                    new ToDo { Id = 4, Title = "Title 4", Description = "Description 4", Complete = true }
              });


            vm = new ShowToDosViewModel(toDoStoreMock.Object);
        }

        [Fact]
        public void CheckCountOfAllToDosTest()
        {
            
            vm.LoadAllToDos();

            Assert.Equal(4, vm.ToDos.Count);
        }


        [Fact]
        public void CheckCountOfAllOpenToDosTest()
        {

            vm.LoadOpenToDos();

            Assert.Equal(2, vm.ToDos.Count);
        }

        [Fact]
        public void CheckCountOfAllClosedToDosTest()
        {

            vm.LoadClosedToDos();

            Assert.Equal(2, vm.ToDos.Count);
        }

        [Fact]
        public void CheckCallLoadAllToDosDoesNotLoadToDosTwice()
        {
            vm.LoadAllToDos();
            vm.LoadAllToDos();

            Assert.Equal(4, vm.ToDos.Count);
        }

        [Theory]
        [InlineData(1,"Title 1","Description 1",false)]
        [InlineData(2, "Title 2", "Description 2", false)]
        [InlineData(3, "Title 3", "Description 3", true)]
        [InlineData(4, "Title 4", "Description 4", true)]
        public void CheckAllToDosDetails(int id, string title, string description, bool complete)
        {
            vm.LoadAllToDos();
            ToDo toDo = vm.ToDos.SingleOrDefault(t => t.Id == id);
            Assert.Equal( title, toDo.Title);
            Assert.Equal( description, toDo.Description);
            Assert.Equal( complete, toDo.Complete);
        }

        [Theory]
        [InlineData(1, "Title 1", "Description 1", false)]
        [InlineData(2, "Title 2", "Description 2", false)]
        public void CheckOpenToDoDetails(int id, string title, string description, bool complete)
        {
            vm.LoadOpenToDos();
            ToDo toDo = vm.ToDos.SingleOrDefault(t => t.Id == id);
            Assert.Equal(title, toDo.Title);
            Assert.Equal(description, toDo.Description);
            Assert.Equal(complete, toDo.Complete);
        }

        [Theory]
        [InlineData(3, "Title 3", "Description 3", true)]
        [InlineData(4, "Title 4", "Description 4", true)]
        public void CheckClosedToDoDetails(int id, string title, string description, bool complete)
        {
            vm.LoadClosedToDos();
            ToDo toDo = vm.ToDos.SingleOrDefault(t => t.Id == id);
            Assert.Equal(title, toDo.Title);
            Assert.Equal(description, toDo.Description);
            Assert.Equal(complete, toDo.Complete);
        }
    }
}
