using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class UserDataViewModel : BindableBase
    {
        public UserDataViewModel() 
        {
        
        }
        
        private ObservableCollection<TimeBar> timeBars;

        public ObservableCollection<TimeBar> TimeBars
        {
            get { return timeBars; }
            set { timeBars = value; RaisePropertyChanged(); }
        }

        void CreateTimeBar()
        {
            TimeBars.Add(new TimeBar(){Date="",Time="" });
            
        }

    }
}
