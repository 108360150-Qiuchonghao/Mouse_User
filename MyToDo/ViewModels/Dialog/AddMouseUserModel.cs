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
    public class AddMouseUserModel : BindableBase, IDialogHostAware
    {
        public AddMouseUserModel(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
            CancelCommand = new DelegateCommand(Cancel);
            ConfirmCommand = new DelegateCommand(Confirm);
        }
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName);
            aggregator.SendMessage("取消");

        }

        private void Confirm()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                //step1.先去資料庫看有沒有這個滑鼠 注意，如果user id只能在這邊給，如果從未給過的話
                //在user端會顯示Mouseid
                

                //step2.有這個滑鼠的話,添加userid和admin id給這個滑鼠 進入資料庫 注意要判斷一下Mouseid是不是全是數字

                //strp3.如果沒有的話，提示已經被佔用或者查無此滑鼠
            }
        }


        public void OnDialogOpend(IDialogParameters parameters)
        {
           MouseUser =  new MouseUserDto( );
        }

        private MouseUserDto mouseuser;

        public MouseUserDto MouseUser
        {
            get { return mouseuser; }
            set { mouseuser = value; RaisePropertyChanged(); }
        }


        public string DialogHostName { get; set; }

        //取消，關閉彈窗通知
        public DelegateCommand CancelCommand { get; set; }
        //添加用戶
        public DelegateCommand ConfirmCommand { get; set; }
        private readonly IEventAggregator aggregator;

    }
}
