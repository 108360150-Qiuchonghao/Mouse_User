using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyToDo.Extensions
{
    public static class WindowsTaskbarIcon
    {
        static TaskbarIcon WindowsNotifyIcon { get; set; }

        public static void Open()
        {
            if (WindowsNotifyIcon is null)
            {
                InitNotifyIcon();
            }
        }

        public static void Exit()
        {
            if (WindowsNotifyIcon is null) return;
            WindowsNotifyIcon.Visibility = System.Windows.Visibility.Collapsed;
            WindowsNotifyIcon.Dispose();
        }
        ///初始化托盘控件
        static void InitNotifyIcon()
        {
            WindowsNotifyIcon = new TaskbarIcon
            {
                Icon = new System.Drawing.Icon("heart.ico")
            };
            ContextMenu context = new ContextMenu();
            MenuItem show = new MenuItem();
            show.Header = "主页";
            show.Click += delegate (object sender, RoutedEventArgs e)
            {
                //Application.Current.MainWindow.Show();
                //Application.Current.MainWindow.Topmost = true;
                //Application.Current.MainWindow.Topmost = false;
            };
            context.Items.Add(show);

            MenuItem exit = new MenuItem();
            exit.Header = "退出";
            exit.Click += delegate (object sender, RoutedEventArgs e)
            {
                Environment.Exit(0);
            };
            context.Items.Add(exit);

            WindowsNotifyIcon.ContextMenu = context;
        }

    }
}
