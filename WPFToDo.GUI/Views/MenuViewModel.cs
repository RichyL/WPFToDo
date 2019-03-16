using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToDo.GUI.Events;

namespace WPFToDo.GUI.Pages
{
    public class MenuViewModel : Screen
    {
        private IDictionary<string,IScreen> _pages=new Dictionary<string,IScreen>();

        private IScreen _requestedPage;

        private IEventAggregator _eventAggregator;

        public MenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public IScreen RequestedPage
        {
            get { return _requestedPage; }
        }


        public IDictionary<string,IScreen> Pages
        {
            get { return _pages; }
        }

        public void AddPage(string pageName,IScreen page)
        {
            _pages.Add(pageName, page);
        }


        public void SelectActivePage(string pageName)
        {
            if (_pages.ContainsKey(pageName))
            {
                this._eventAggregator.Publish(new PageChangedEvent() { NewPage = _pages[pageName] });
            }
        }

      

    }
}
