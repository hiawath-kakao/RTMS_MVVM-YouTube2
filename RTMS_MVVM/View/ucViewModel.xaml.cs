using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RTMS_MVVM.ViewModel;

namespace RTMS_MVVM.View
{
    /// <summary>
    /// View.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucViewModel : UserControl
    {
        public ucViewModel()
        {
            InitializeComponent();
        }
        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            bool designTime = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
            if (designTime)
            {
            }
            else
            {
                foreach (CommandBinding cmd in LogicViewModel.Command)
                {
                    this.CommandBindings.Add(cmd);
                }
            }
        }
    }
}
