using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTodo.Common
{
    public class Task
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private DateTime _datetime;

        public string DateTime
        {
            get { return $"{_datetime.ToShortDateString()}  {_datetime.ToShortTimeString()}"; }
            //TODO - need to think how to implement the setting of the date time.
            //set { _datetime = value; }
        }

        public Task()
        {
            _datetime = new System.DateTime();
        }

    }
}
