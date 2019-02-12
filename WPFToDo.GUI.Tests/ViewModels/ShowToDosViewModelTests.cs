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
                    new ToDo { Id = 2, Title = "Title 2", Description = "Description 2", Complete = false }
                });

            vm = new ShowToDosViewModel(toDoStoreMock.Object);
        }

       [Fact]
        public void CheckCountOfToDosTest()
        {
         
           vm.Load();

            Assert.Equal(2, vm.ToDos.Count);
        }

        [Fact]
        public void CheckCallLoadDoesNotLoadToDosTwice()
        {

            vm.Load();
            vm.Load();

            Assert.Equal(2, vm.ToDos.Count);
        }

        [Theory]
        [InlineData(1,"Title 1","Description 1",false)]
        [InlineData(2, "Title 2", "Description 2", false)]
        public void CheckToDoDetails(int id, string title, string description, bool complete)
        {
            vm.Load();
            ToDo toDo = vm.ToDos.SingleOrDefault(t => t.Id == id);
            Assert.Equal( title, toDo.Title);
            Assert.Equal( description, toDo.Description);
            Assert.Equal( complete, toDo.Complete);
        }
    }
}
