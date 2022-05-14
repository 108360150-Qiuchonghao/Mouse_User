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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyToDo.Views.TestViews
{
    /// <summary>
    /// UserDataTestView.xaml 的交互逻辑
    /// </summary>
    public partial class UserDataTestView : UserControl
    {
        public UserDataTestView()
        {
            InitializeComponent();
        }

        private void ResetZoomOnClick(object sender, RoutedEventArgs e)
        {
            //Use the axis MinValue/MaxValue properties to specify the values to display.
            //use double.Nan to clear it.
            X.MinValue = double.NaN;
            X.MaxValue = double.NaN;
            X1.MinValue = double.NaN;
            X1.MaxValue = double.NaN;
            X3.MinValue = double.NaN;
            X3.MaxValue = double.NaN;
        }
    }
}
