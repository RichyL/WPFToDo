using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stylet;
using WPFToDo.GUI.Pages;
using WPFToDo.Common;

namespace WPFToDo.GUI.Events
{
    public class EditToDoEvent : EventArgs
    {
        public EditToDoEvent(ToDo t)
        {
            ToDo = t;
        }

        public ToDo ToDo { get; set; }
    }
}
