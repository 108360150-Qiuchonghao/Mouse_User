using MyToDo.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using MyToDo.Properties;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Shared.Dtos;
using Prism.Regions;
using Prism.Ioc;

namespace MyToDo.ViewModels
{
    public class SysSettingViewModel: NavigationViewModel
    {
        public SysSettingViewModel(IDialogHostService dialog, IContainerProvider provider) : base(provider)
        {
            Devicepidvid = Settings.Default["DeviceID"].ToString(); 
            ShowAddCommand = new DelegateCommand(ShowAdd);
            SelectRadio = new DelegateCommand(Select);
            ChangeExitDialogShow = new DelegateCommand(changeExitDialogShow);
            choice = new ChoicesDto();
            choice.Choice = (bool)Properties.Settings.Default["ExitorHide"];
            Choice1 = !choice.Choice;
            Exitdialogshow = (bool)Properties.Settings.Default["ExitDialogShow"];
             this.dialog = dialog;
        }

        private string devicepidvid;
        public string Devicepidvid
        {
            get { return devicepidvid; }
            set { devicepidvid = value; RaisePropertyChanged(); }
        }

        private string newid;
        public string NewID
        {
            get { return newid; }
            set { newid = value; RaisePropertyChanged(); }
        }
        public DelegateCommand ShowAddCommand { get; private set; }
        public DelegateCommand ChangeExitDialogShow { get; private set; }
        public DelegateCommand SelectRadio { get; set; }
        private readonly IDialogHostService dialog;
     
        void ShowAdd()
        {
            DialogParameters keys = new DialogParameters();
            keys.Add("DeviceID", Devicepidvid);
            dialog.ShowDialog("ChangeDeviceIDView", keys, DialogCallback);
        }

        private void DialogCallback(IDialogResult result)
        {
            ButtonResult  result1 = result.Result;

            if (result1 == ButtonResult.OK)
            {
               
                var param = result.Parameters.GetValue<string>("DeviceID");
                Devicepidvid = param;
                Properties.Settings.Default["DeviceID"] = Devicepidvid;
                Properties.Settings.Default.Save();
            }
            else
                return;
        }
        private void Select()
        {
            if (choice.Choice == true)
            {
                Properties.Settings.Default["ExitorHide"] = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default["ExitorHide"] = false;
                Properties.Settings.Default.Save();
            }
        }

        private bool choice1;

        public bool Choice1
        {
            get { return choice1; }
            set { choice1 = value; }
        }


        private ChoicesDto choice;

        public ChoicesDto Choice
        {
            get { return choice; }
            set { choice = value; RaisePropertyChanged(); }
        }

        private bool exitdialogshow;

        public bool Exitdialogshow
        {
            get { return exitdialogshow; }
            set { exitdialogshow = value; }
        }

        private void changeExitDialogShow()
        {
            if (Exitdialogshow == true)
            {
                Properties.Settings.Default["ExitDialogShow"] = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default["ExitDialogShow"] = false;
                Properties.Settings.Default.Save();
            }
        }


    }
}
