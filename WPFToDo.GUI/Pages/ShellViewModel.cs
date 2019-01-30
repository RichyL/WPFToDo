using System;
using Stylet;

namespace WPFToDo.GUI.Pages
{
    public class ShellViewModel : Screen
    {
        public TopBarViewModel TopBar { get; private set; }

        public MenuViewModel Menu { get; private set; }

        public ShellViewModel(TopBarViewModel tBarVM, MenuViewModel menuVM)
        {
            TopBar = tBarVM;
            Menu=menuVM;
            TopBar.PageTitle = "Text set from the ShellViewModel";
        }
    }
}
