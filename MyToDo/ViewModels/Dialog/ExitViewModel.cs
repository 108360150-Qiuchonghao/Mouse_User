using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyToDo.Properties;

namespace MyToDo.ViewModels.Dialog
{
    public class ExitViewModel : BindableBase, IDialogHostAware  
    {
        public ExitViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel);
            ConfirmCommand = new DelegateCommand(Confirm);
        }
        private void initradiobutton()
        {
            Mychoice.Choice = (bool)Properties.Settings.Default["ExitorHide"];
            choice1 = !Mychoice.Choice;
        }
        private void Cancel() 
        {
            if(DialogHost.IsDialogOpen(DialogHostName))
                 DialogHost.Close(DialogHostName);
        }
        private void Confirm()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                if(Exitdialogshow == true)
                {
                    Properties.Settings.Default["ExitDialogShow"] = true;
                    Properties.Settings.Default.Save();
                }
                if (Mychoice.Choice == true)
                {
                    //Properties.Settings.Default["ExitorHide"]=true;
                    //Properties.Settings.Default.Save();
                    Application.Current.Shutdown();

                }
                    
                else if(Mychoice.Choice == false)
                {
                    //Properties.Settings.Default["ExitorHide"] = false;
                    //Properties.Settings.Default.Save();
                    DialogHost.Close(DialogHostName);
                    Application.Current.MainWindow.Hide();
                }
            }
        }
       
        //mychoice是判斷我選擇哪一種關閉方式
        private ChoicesDto mychoice;

        public ChoicesDto Mychoice
        {
            get { return mychoice; }
            set { mychoice = value; RaisePropertyChanged(); }
        }

        private bool exitdialogshow;

        public bool Exitdialogshow
        {
            get { return exitdialogshow; }
            set { exitdialogshow = value; }
        }

        private bool choice1;

        public bool Choice1
        {
            get { return choice1; }
            set { choice1 = value; }
        }

        public string DialogHostName { get ; set; }
        public DelegateCommand CancelCommand { get ; set ; }
        public DelegateCommand ConfirmCommand { get; set; }
        public DelegateCommand SelectRadio { get; set; }
        public void OnDialogOpend(IDialogParameters parameters)
        {
            Mychoice = new ChoicesDto();
            Choice1 = new bool();
            initradiobutton();
        }
    }
}
