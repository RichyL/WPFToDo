using System;
using Stylet;

namespace WPFToDo.GUI.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public TopBarViewModel TopBar { get; private set; }

        public MenuViewModel Menu { get; private set; }

        
        public ShellViewModel(TopBarViewModel tBarVM, MenuViewModel menuVM)
        {
            TopBar = tBarVM;
            Menu=menuVM;
            TopBar.PageTitle = "Text set from the ShellViewModel";

            this.Items.Add(new Page1ViewModel());
            this.Items.Add(new Page2ViewModel());
            this.Items.Add(new Page3ViewModel());

            this.ActivateItem(this.Items[2]);
        }
    }
}
