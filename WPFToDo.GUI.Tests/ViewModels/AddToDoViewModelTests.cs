using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.Database;
using Moq;
using WPFToDo.Common;
using Xunit;
using WPFToDo.GUI.ViewModels;

namespace WPFToDo.GUI.Tests.ViewModels
{
    public class AddToDoViewModelTests 
    {
        AddToDoViewModel vm = null;

        Mock<IToDoStore> toDoStoreMock=null;

        public AddToDoViewModelTests()
        {
            toDoStoreMock = new Mock<IToDoStore>();
            toDoStoreMock.Setup(x => x.AddToDo(It.IsAny<string>(), It.IsAny<string>())).
                Returns(new ToDo { Id = 1, Title = "Title 1", Description = "Description 1" });

            vm = new AddToDoViewModel(toDoStoreMock.Object);
        }

        [Fact]
        public void CheckAddNewToDo()
        {
            
            vm.Title = "Title 1";
            vm.Description = "Description 1";
            ToDo t = vm.SaveToDo();
            Assert.Equal(1, t.Id);
            Assert.Equal("Title 1", t.Title);
            Assert.Equal("Description 1",t.Description);
        }


        [Fact]
        public void CheckAddNewToDoMustHaveTitle()
        {
            vm.Title = "";
            vm.Description = "Some description";
            ArgumentException ex = Assert.Throws<ArgumentException>(() => vm.SaveToDo());
            Assert.Equal("The title cannot be blank", ex.Message);
        }
    }
}
