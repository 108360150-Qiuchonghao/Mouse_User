using MyToDo.Extensions;
using MyToDo.Views.Dialog;
using MyToDo.Views.Icon;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyToDo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IEventAggregator aggregator)
        {
            InitializeComponent();
            //註冊提示消息
            aggregator.Resgiter(arg =>
            {
                MainSnackbar.MessageQueue.Enqueue(arg);
            });
            //注册等待消息窗口
            aggregator.Resgiter(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;

                if (DialogHost.IsOpen)
                    DialogHost.DialogContent = new ProgressView();
            });

            //btnMin.Click += (s, e) =>
            //{
            //    this.WindowState = WindowState.Minimized;
            //};

            btnMax.Click += (s, e) =>
            {
                if(this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            };


            btnclose.Click += (s, e) =>
            {
                //this.Close();
            };
            Topcolorzone.MouseMove += (s, e) =>
              {
                  if (e.LeftButton == MouseButtonState.Pressed)
                  {
                      
                      this.DragMove(); 
                  }
              };
            Topcolorzone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            };

            menuBar.SelectionChanged += (s, e) =>
             {
                 drawHost.IsLeftDrawerOpen = false;
             };

            
        }


        

    }
}
