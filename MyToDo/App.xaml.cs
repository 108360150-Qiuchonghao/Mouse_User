using DryIoc;
using MyToDo.Common;
using MyToDo.Service;
using MyToDo.ViewModels;
using MyToDo.ViewModels.Dialog;
using MyToDo.ViewModels.TestViewModels;
using MyToDo.Views;
using MyToDo.Views.Dialog;
using MyToDo.Views.TestViews;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;
using MyToDo.Extensions;
using MyToDo.Service.TestService;
using MyToDo.Service.GetData;
using MyToDo.Service.PushData;
using SciChart.Charting.Visuals;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>(); 
        }

        private TaskbarIcon _taskbar;

        

        protected override void OnInitialized()
        {
            //var Service = App.Current.MainWindow.DataContext as IConfigureService;
            //if (Service != null)
            //{
            //    Service.Configure();
            //}
            //base.OnInitialized();
            //WindowsTaskbarIcon.Open();

            // Set this code once in App.xaml.cs or application startup
            // Set this code once in App.xaml.cs or application startup
            // Set this code once in App.xaml.cs or application startup
            //SciChartSurface.SetRuntimeLicenseKey("XKbib/Ou6IZL/Vrdt9ulmY1mbi8O5dPczxkuHlDMHdRn1kyNl5t5PnGdpBLBcaGh5qI0AQDw5dOi9VJg+ErPMaFEDezXT0XTTgyrkJRlhREqVmzHABUZc/Swum6n/1Bazy1I3oLJdT69BezHY1yaajlNcp9rX04RX85LukAYMgBNTwhi6h2PTGCcGg3O4EOm2uY26iX/MWmifubFLOX3YWeej++/R/mYUmMdUND9/9WBzIWdpHQ8nnJG45KoPCPyzNxQ2IFp3+a71Ouk/MkkhK0szuHHU3mukce1oPtOQOTiGhoY8Lf7GK7lyPKC7VU1oNRLRk9GOZG0ulT5qylF4GPE7qTuUxq1H9HuTbDYCE9Eo22Z5QOxZNK02I0eIToB5bjlxPUMdHfg1MYe5Dpt+ZHRXS2OhLx0k3iVwyMP31Ka9M0TKC5UNbwsaH/2fsHpCAMV7LzG6dtdfA5jjqtBj2FlENXtHEHavQT1Y6F8QjCz6VVVN7gGhehFl6wQ4pxudbymi3zwOxUm0JIIU6evdQly92mcJ3nwPCfl8amGpnwaP/hpHomOPZ8e");

            //縮小的icon
            _taskbar = (TaskbarIcon)FindResource("Taskbar");
            
            var dialog = Container.Resolve<IDialogService>();
            
            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Application.Current.Shutdown();
                    return;
                }
                else if(callback.Result == ButtonResult.OK) { 
                    
                    var service = App.Current.MainWindow.DataContext as IConfigureService;
                    if (service != null)
                        service.Configure();
                    base.OnInitialized();
                }
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer()
                .Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));

            //containerRegistry.GetContainer().RegisterInstance(@"http://127.0.0.1:3000/", serviceKey: "webUrl");
            containerRegistry.GetContainer().RegisterInstance(@"http://031a-2001-b011-400a-1d98-701d-aa98-a64f-f1c4.ngrok.io/", serviceKey: "webUrl");

            containerRegistry.Register<ILoginService,LoginService>();
            containerRegistry.Register<ITestLoginService, TestLoginService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();
            containerRegistry.Register<IModifyNameService, ModifyNameService>();
            containerRegistry.Register<IConnectService, ConnectService>();
            containerRegistry.Register<IPushDataService, PushDataService>();
            containerRegistry.Register<IPullDataService, PullService>();

            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
            containerRegistry.RegisterDialog<MiniwindowView, MiniwindowViewModel>();
            containerRegistry.RegisterForNavigation<ExitView, ExitViewModel>();
            containerRegistry.RegisterForNavigation<AddMouseUser, AddMouseUserModel>();
            containerRegistry.RegisterForNavigation<ChangeDeviceIDView, ChangeDeviceIDViewModel>();

            
            containerRegistry.RegisterForNavigation<SysSettingView, SysSettingViewModel>();
            containerRegistry.RegisterForNavigation<AboutUsView, SysSettingViewModel>();

            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<UserDataView, UserDataViewModel>();
            containerRegistry.RegisterForNavigation<SettingView, SettingViewModel>();
            containerRegistry.RegisterForNavigation<UserDataTestView, UserDataTestViewModel>();
        }

        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();

            var dialog = containerProvider.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                Current.MainWindow.Show();
            });
        }


        public static void Miniwindow(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();

            var dialog = containerProvider.Resolve<IDialogService>();

            dialog.ShowDialog("MiniwindowView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                Current.MainWindow.Show();
            });
        }
    }
}
