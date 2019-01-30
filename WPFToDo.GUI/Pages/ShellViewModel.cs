using System;
using Stylet;

namespace WPFToDo.GUI.Pages
{
    public class ShellViewModel : Screen
    {
        public TopBarViewModel TopBar { get; private set; }

        public ShellViewModel(TopBarViewModel tBarVM)
        {
            TopBar = tBarVM;

            TopBar.PageTitle = "Text set from the ShellViewModel";
        }
    }
}
