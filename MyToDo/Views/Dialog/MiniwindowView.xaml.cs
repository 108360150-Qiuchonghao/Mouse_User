using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.Events;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Input;
using MyToDo.Common.Events;

namespace MyToDo.Views.Dialog
{
    /// <summary>
    /// MiniwindowView.xaml 的交互逻辑
    /// </summary>
    public partial class MiniwindowView : UserControl
    {
        public MiniwindowView()
        {
            InitializeComponent();
            

        }
        #region Commands
        public BaseCommand<Window> DragWindowCommand
        {
            get => new BaseCommand<Window>(/*async*/(window) => {
                window.DragMove();
            });
        }
        #endregion


    }
}
