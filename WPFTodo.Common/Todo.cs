using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToDo.Common
{
    /// <summary>
    /// A ToDo is the basic item representing a task to be completed.
    /// When a ToDo is created the date and time of creation is automatically saved in _dateTime
    /// </summary>
    public class ToDo
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

        private bool _complete;

        public bool Complete
        {
            get { return _complete; }
            set { _complete = value; }
        }

        public int CompleteAsNumber
        {
            get { return (Complete ? 1 : 0); }
        }


        public ToDo()
        {
            _datetime = new System.DateTime();
            _complete = false;   
        }

        /// <summary>
        /// Overrides Equals and tests that two ToDo objects have the same properties.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ToDo))  return false;

            ToDo testObj = (ToDo)obj;

            return testObj.Id == this.Id
                && testObj.Title == this.Title
                && testObj.Description == this.Description
                && testObj.Complete == this.Complete
                && testObj.DateTime == this.DateTime;
        }

        /// <summary>
        /// Define override of GetHashCode as Equals overridden.
        /// Need to do this properly.
        /// </summary>
        /// <returns></returns>
        /// <TODO>Need to implement custom version of this</TODO>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
