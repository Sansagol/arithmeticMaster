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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Sansagol.ArithmeticMaster
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        ObservableCollection<OperationType> Operations = new ObservableCollection<OperationType>();

        public int Accuracy
        {
            get { return (int)GetValue(AccuracyProperty); }
            set { SetValue(AccuracyProperty, value); }
        }
        public static readonly DependencyProperty AccuracyProperty =
            DependencyProperty.Register("Accuracy", typeof(int), typeof(SettingsWindow), new UIPropertyMetadata(0));

        public OperationType SelectedOperType
        {
            get { return (OperationType)GetValue(SelectedOperTypeProperty); }
            set { SetValue(SelectedOperTypeProperty, value); }
        }
        public static readonly DependencyProperty SelectedOperTypeProperty =
            DependencyProperty.Register("SelectedOperType", typeof(OperationType), typeof(SettingsWindow),
            new UIPropertyMetadata(null));

        public decimal FirstOperandDim
        {
            get { return (decimal)GetValue(FirstOperandDimProperty); }
            set { SetValue(FirstOperandDimProperty, value); }
        }
        public static readonly DependencyProperty FirstOperandDimProperty =
            DependencyProperty.Register("FirstOperandDim", typeof(decimal), typeof(SettingsWindow), new UIPropertyMetadata((decimal)1));

        public decimal SecondOperandDim
        {
            get { return (decimal)GetValue(SecondOperandDimProperty); }
            set { SetValue(SecondOperandDimProperty, value); }
        }
        public static readonly DependencyProperty SecondOperandDimProperty =
            DependencyProperty.Register("SecondOperandDim", typeof(decimal), typeof(SettingsWindow), new UIPropertyMetadata((decimal)1));

        public SettingsWindow()
        {
            InitializeComponent();

            Operations.Add(OperationType.Addition);
            Operations.Add(OperationType.Subtraction);
            Operations.Add(OperationType.Multiplication);
            //   Operations.Add(OperationType.Division);
            cmbOperationType.ItemsSource = Operations;
        }

        private void bntClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
