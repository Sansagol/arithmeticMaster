using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Collections.ObjectModel;
using Sansagol.ArithmeticMaster.Log;

namespace Sansagol.ArithmeticMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Local variables
        /// <summary>Random generator</summary>
        Random _RndGenerator = new Random();

        /// <summary>Time of last action</summary>
        private DateTime _LastDate = DateTime.Now;

        /// <summary>Current accuracy</summary>
        private int _Accuracy = 0;

        /// <summary>First operand dimension</summary>
        private int FirstOperandDim = 1;

        /// <summary>Second operand dimension</summary>
        private int SecondOperandDim = 1;
        #endregion



        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }
        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register("IsRunning", typeof(bool), typeof(MainWindow),
            new UIPropertyMetadata(false, new PropertyChangedCallback(OnRunningChanged)));


        /// <summary>Selected operation</summary>
        public OperationType SelectedOperType
        {
            get { return (OperationType)GetValue(SelectedOperTypeProperty); }
            set { SetValue(SelectedOperTypeProperty, value); }
        }
        public static readonly DependencyProperty SelectedOperTypeProperty =
            DependencyProperty.Register("SelectedOperType", typeof(OperationType), typeof(MainWindow),
            new UIPropertyMetadata(OperationType.Addition));



        public OperationResultMessage CalcStatus
        {
            get { return (OperationResultMessage)GetValue(CalcStatusProperty); }
            set { SetValue(CalcStatusProperty, value); }
        }
        public static readonly DependencyProperty CalcStatusProperty =
            DependencyProperty.Register("CalcStatus", typeof(OperationResultMessage), typeof(MainWindow),
            new UIPropertyMetadata(new OperationResultMessage("Введите значение", Brushes.Black)));


        public decimal OldValue
        {
            get { return (decimal)GetValue(OldValueProperty); }
            set { SetValue(OldValueProperty, value); }
        }
        public static readonly DependencyProperty OldValueProperty =
            DependencyProperty.Register("OldValue", typeof(decimal), typeof(MainWindow), new UIPropertyMetadata((decimal)0,
             ((d, e) =>
             {
                 MainWindow win = d as MainWindow;
                 if (win == null)
                     return;
                 win.OldValue = decimal.Round((decimal)e.NewValue, (int)win._Accuracy);
             })));

        public decimal NewValue
        {
            get { return (decimal)GetValue(NewValueProperty); }
            set { SetValue(NewValueProperty, value); }
        }
        public static readonly DependencyProperty NewValueProperty =
            DependencyProperty.Register("NewValue", typeof(decimal), typeof(MainWindow), new UIPropertyMetadata((decimal)0,
             ((d, e) =>
             {
                 MainWindow win = d as MainWindow;
                 if (win == null)
                     return;
                 win.NewValue = decimal.Round((decimal)e.NewValue, (int)win._Accuracy);
             })));

        /// <summary>
        /// Value entered of user
        /// </summary>
        public decimal UserValue
        {
            get { return (decimal)GetValue(UserValueProperty); }
            set { SetValue(UserValueProperty, value); }
        }
        public static readonly DependencyProperty UserValueProperty =
            DependencyProperty.Register("UserValue", typeof(decimal), typeof(MainWindow), new UIPropertyMetadata((decimal)0));


        public bool IllegalUserValue
        {
            get { return (bool)GetValue(IllegalUserValueProperty); }
            set { SetValue(IllegalUserValueProperty, value); }
        }
        public static readonly DependencyProperty IllegalUserValueProperty =
            DependencyProperty.Register("IllegalUserValue", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(false));

        


        public MainWindow()
        {
            InitializeComponent();
        }

        private static void OnRunningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow win = d as MainWindow;
            if (win != null)
            {
                if (((bool)e.NewValue) == true)
                {
                    win._LastDate = DateTime.Now;
                    win.SetOldValue();
                    win.SetNewValue();
                }
                else
                {
                    win.OldValue = 0;
                    win.NewValue = 0;
                }
            }
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (txtInput.GetBindingExpression(TextBox.TextProperty).HasError)
                return;

            decimal res = OperationExecuter.ExecOperation(OldValue, NewValue, SelectedOperType);

            if (res == UserValue)
            {
                CalcStatus = new OperationResultMessage(UserValue.ToString() + " Верно", Brushes.Green);
                LogStorage.AddLog(new SimpleEvent(_LastDate,
                    DateTime.Now,
                    OldValue.ToString(),
                    NewValue.ToString(),
                    SelectedOperType,
                    res.ToString(),
                    true));
                _LastDate = DateTime.Now;

                SetOldValue();
                SetNewValue();
            }
            else
            {
                CalcStatus = new OperationResultMessage(UserValue.ToString() + " Неверно", Brushes.Red);

                LogStorage.AddLog(new SimpleEvent(_LastDate,
                    DateTime.Now,
                    OldValue.ToString(),
                    NewValue.ToString(),
                    SelectedOperType,
                    UserValue.ToString(),
                    false));
                _LastDate = DateTime.Now;
            }
            txtInput.Clear();
        }

        private void SetOldValue()
        {
            TextOutputConverter.Accuracy = _Accuracy;
            OldValue = GetNewRandom(9 * (int)Math.Pow(10, (FirstOperandDim - 1)));
        }

        private void SetNewValue()
        {
            TextOutputConverter.Accuracy = _Accuracy;

            NewValue = GetNewRandom(9 * (int)Math.Pow(10, (SecondOperandDim - 1)));
        }

        private decimal GetNewRandom(int maxVal)
        {
            if (maxVal < 1)
                maxVal = 9;


            decimal value = 0;
            if (_Accuracy == 0)
                value = _RndGenerator.Next(1, maxVal);
            else
            {
                double newDouble = _RndGenerator.NextDouble();
                value = (decimal)((newDouble * Math.Abs(maxVal - 1)) + 1);
            }
            return value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MaxHeight = this.MinHeight = this.ActualHeight;
        }

        private void cmbOperationType_Loaded(object sender, RoutedEventArgs e)
        {
            _LastDate = DateTime.Now;
            if (((ComboBox)sender).Items.Count > 0)
            {
                ((ComboBox)sender).SelectedIndex = 0;
            }
        }

        private void btnState_Click(object sender, RoutedEventArgs e)
        {
            LogShowerWindow win = new LogShowerWindow();
            win.Owner = this;
            win.ShowDialog();
        }

        private void bntSettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsWindow win = new SettingsWindow();
            win.Owner = this;

            win.Accuracy = this._Accuracy;
            win.SelectedOperType = this.SelectedOperType;
            win.FirstOperandDim = this.FirstOperandDim;
            win.SecondOperandDim = this.SecondOperandDim;
            win.ShowDialog();

            this._Accuracy = win.Accuracy;
            this.SelectedOperType = win.SelectedOperType;
            this.FirstOperandDim = (int)win.FirstOperandDim;
            this.SecondOperandDim = (int)win.SecondOperandDim;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            IsRunning = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            IsRunning = false;
        }
    }
}
