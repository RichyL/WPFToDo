﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stylet;

namespace WPFToDo.GUI.Events
{
    public class PageChangedEvent
    {
        public IScreen NewPage { get; set; }
    }
}
