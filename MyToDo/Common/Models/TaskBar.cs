using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class TaskBar : BindableBase
    {
        private string icon;
        private string titile;
        private string content;
        private string color;
        private string target;


        public string Icon 
        {
            get { return icon; }
            set { icon = value; }
        }
        
        public string Title
        {
            get { return titile; }
            set { titile = value; }
        }
        
        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }
        
        public string Color 
        {
            get { return color; }
            set { color = value; }
        }
        
        /*觸發目標*/
        public string Target
        {
            get { return target; }
            set { target = value; }
        }


    }
}
