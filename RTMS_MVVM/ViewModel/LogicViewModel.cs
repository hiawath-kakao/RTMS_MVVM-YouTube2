using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using RTMS_MVVM.Models;
using RTMS_MVVM.View;
namespace RTMS_MVVM.ViewModel
{
    class LogicViewModel : ModelBase
    {
        // 2021-08-06
        [XmlIgnore]
        public static CommandBindingCollection Command = new CommandBindingCollection();
        // 2021-08-06
        public static RoutedCommand PlusCommand = new RoutedCommand("PlusCommand", typeof(ApplicationCommands));
        public static RoutedCommand MinusCommand = new RoutedCommand("MinusCommand", typeof(ApplicationCommands));
        public static RoutedCommand AddCommand = new RoutedCommand("AddCommand", typeof(ApplicationCommands));
        public static RoutedCommand DelCommand = new RoutedCommand("DelCommand", typeof(ApplicationCommands));
        public static RoutedCommand ModelCommand = new RoutedCommand("ModelCommand", typeof(ApplicationCommands));
        public static RoutedCommand AddModelCommand = new RoutedCommand("AddModelCommand", typeof(ApplicationCommands));
        public static RoutedCommand ModelCloseCommand = new RoutedCommand("ModelCloseCommand", typeof(ApplicationCommands));
        public static RoutedCommand LoginCommand = new RoutedCommand("LoginCommand", typeof(ApplicationCommands));
        public static RoutedCommand LoginConfirmCommand = new RoutedCommand("LoginConfirmCommand", typeof(ApplicationCommands));
        public ObservableCollection<Model> MODELS { get; set; }
        public ObservableCollection<SalesModel> SALES_MODEL { get; set; }        
        public Model MODEL { get; set; }
        private int TOTAL_ = 0;
        public int TOTAL
        {
            get { return this.TOTAL_; }
            set
            {
                this.TOTAL_ = value;
                this.OnPropertyChanged("TOTAL");
            }
        }
        public LogicViewModel()
        {
            // 2021-08-09
            this.MODELS = clsUtil.ReadData<ObservableCollection<Model>>("TEST.xml");
            this.SALES_MODEL = new ObservableCollection<SalesModel>();
            // 2021-08-06                        
            LogicViewModel.Command.Add(new CommandBinding(LogicViewModel.MinusCommand, Executed, CanExecuted));
            LogicViewModel.Command.Add(new CommandBinding(LogicViewModel.PlusCommand, Executed, CanExecuted));
            LogicViewModel.Command.Add(new CommandBinding(LogicViewModel.AddCommand, Executed, CanExecuted));
            LogicViewModel.Command.Add(new CommandBinding(LogicViewModel.DelCommand, Executed, CanExecuted));
            LogicViewModel.Command.Add(new CommandBinding(LogicViewModel.ModelCommand, Executed, CanExecuted));
            LogicViewModel.Command.Add(new CommandBinding(LogicViewModel.AddModelCommand, Executed, CanExecuted));
            LogicViewModel.Command.Add(new CommandBinding(LogicViewModel.LoginCommand, Executed, CanExecuted));
            LogicViewModel.Command.Add(new CommandBinding(LogicViewModel.LoginConfirmCommand, Executed, CanExecuted));            
        }
        // 2021-08-06
        private void CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        // 2021-08-06
        private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is SalesModel)
            {
                SalesModel sales = e.Parameter as SalesModel;
                if ((e.Command as RoutedCommand).Name == "MinusCommand")
                {
                    sales.EA--;
                }
                else if ((e.Command as RoutedCommand).Name == "PlusCommand")
                {
                    sales.EA++;
                }
                else if ((e.Command as RoutedCommand).Name == "DelCommand")
                {
                    if (e.Parameter is SalesModel)
                    {
                        this.SALES_MODEL.Remove(e.Parameter as SalesModel);
                    }
                }
                sales.EA_PRICE = sales.EA * sales.PRICE;
                this.TOTAL = this.CalcFunction();
            }
            else
            {
                if ((e.Command as RoutedCommand).Name == "AddCommand")
                {
                    if (e.Parameter is Model)
                    {
                        Model model = e.Parameter as Model;
                        this.SALES_MODEL.Add(new SalesModel { NAME = model.NAME, EA = 1, PRICE = model.PRICE, EA_PRICE = model.PRICE });
                        this.TOTAL = this.CalcFunction();
                    }
                }
                else if ((e.Command as RoutedCommand).Name == "ModelCommand")
                {
                    frmModel frmModel = new frmModel();
                    this.MODEL = new Model { NAME = "TEST", PRICE = 0 };
                    frmModel.DataContext = this;
                    frmModel.ShowDialog();
                    if (frmModel.DialogResult == true)
                    {
                        this.MODELS.Add(this.MODEL);
                        // 2021-08-09
                        clsUtil.SaveData<ObservableCollection<Model>>("TEST.xml", this.MODELS);
                    }
                }
                else if ((e.Command as RoutedCommand).Name == "AddModelCommand")
                {
                    if (sender is frmModel)
                    {
                        frmModel frmmodel = sender as frmModel;
                        frmmodel.DialogResult = true;
                        frmmodel.Close();
                    }
                }
                else if ((e.Command as RoutedCommand).Name == "LoginCommand")
                {
                    frmLogin frmLogin = new frmLogin();
                    frmLogin.DataContext = this;
                    frmLogin.ShowDialog();
                    if (frmLogin.DialogResult == true)
                    {
                    }
                }
                else if ((e.Command as RoutedCommand).Name == "LoginConfirmCommand")
                {
                    if ( sender is frmLogin)
                    {
                        (sender as frmLogin).Close();
                    }
                }
            }
        }
        private int CalcFunction()
        {
            return this.SALES_MODEL.Sum(p => p.EA * p.PRICE);
        }
    }
}