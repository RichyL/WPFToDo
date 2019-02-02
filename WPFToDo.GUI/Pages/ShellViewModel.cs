using System;
using Stylet;
using WPFToDo.GUI.Events;

namespace WPFToDo.GUI.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<PageChangedEvent>
    {
        public TopBarViewModel TopBar { get; private set; }

        public MenuViewModel Menu { get; private set; }


        public ShellViewModel(TopBarViewModel tBarVM, MenuViewModel menuVM, IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            TopBar = tBarVM;
            Menu=menuVM;
            TopBar.PageTitle = "Text set from the ShellViewModel";

            IScreen page = new Page1ViewModel();


            AddPage("Page 1", new Page1ViewModel());
            AddPage("Page 2", new Page2ViewModel());
            AddPage("Page 3", new Page3ViewModel());
  
        }

        public void Handle(PageChangedEvent pge)
        {
            this.ActiveItem = pge.NewPage;
        }

        protected void AddPage(string pageName, IScreen page)
        {
            this.Items.Add(page);
            Menu.AddPage(pageName, page);
        }


    }
}
