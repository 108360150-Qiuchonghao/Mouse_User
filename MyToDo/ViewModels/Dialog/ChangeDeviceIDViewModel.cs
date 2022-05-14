using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels.Dialog
{
    public class ChangeDeviceIDViewModel : BindableBase, IDialogAware
    {
        public ChangeDeviceIDViewModel(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
            CancelCommand = new DelegateCommand(Cancel);
            ConfirmCommand = new DelegateCommand(Confirm);
        }
        #region command
        private void Cancel()
        {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
                
        }
        private void Confirm()
        {
            
                IDialogParameters parameters = new DialogParameters();
                parameters.Add("DeviceID", NewDeviceID);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
                

        }
        #endregion

        public void OnDialogOpened(IDialogParameters parameters)
        {
            //打开对话框的时候解析传递进来的参数
            Message = parameters.GetValue<string>("DeviceID");
        }

        public bool CanCloseDialog()
        {
           return true;
        }

        public void OnDialogClosed()
        {
            
        }

        
        public string DialogHostName { get; set; }
        public event Action<IDialogResult> RequestClose;

        //取消，關閉彈窗通知
        public DelegateCommand CancelCommand { get; set; }
        //添加用戶
        public DelegateCommand ConfirmCommand { get; set; }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(); }
        }


        private readonly IEventAggregator aggregator;

        private string newid;
        public string NewDeviceID
        {
            get { return newid; }
            set { newid = value; RaisePropertyChanged(); }
        }

        public string Title { get; }
    }
}
