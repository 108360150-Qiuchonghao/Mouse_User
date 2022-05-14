using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class TimeBar
    {
        public string time;
        public string date;
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }




        //private string icon;
        ///*菜单图标*/
        //public string Icon
        //{
        //    get { return icon; }
        //    set { icon = value; }
        //}


    }
}
