using RTMS_MVVM.Models;
using RTMS_MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace RTMS_MVVM.View
{
    /// <summary>
    /// frmPassword.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class frmModel : Window
    {
        public frmModel()
        {
            InitializeComponent();
         }
        private void Window_Loaded(object sender, RoutedEventArgs e)
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
