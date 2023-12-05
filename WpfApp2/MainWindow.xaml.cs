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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        private double currentValue = 0;
        private string currentOperator = "";
        private bool isNewNumber = true;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; 
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Left || e.Key == Key.Right)
            {
                e.Handled = false; 
                return;
            }
            e.Handled = !((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9));
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            if (isNewNumber)
            {
                Display.Text = ((Button)sender).Content.ToString();
                isNewNumber = false;
            }
            else
            {
                if (Display.Text == "0")
                    Display.Text = ((Button)sender).Content.ToString();
                else
                    Display.Text += ((Button)sender).Content.ToString();
            }
        }
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            currentOperator = ((Button)sender).Content.ToString();
            currentValue = double.Parse(Display.Text);
            isNewNumber = true;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            double newValue = double.Parse(Display.Text);
            switch (currentOperator)
            {
                case "+":
                    currentValue += newValue;
                    break;
                case "-":
                    currentValue -= newValue;
                    break;
                case "X":
                    currentValue *= newValue;
                    break;
                case "/":
                    if (newValue != 0)
                        currentValue /= newValue;
                    else
                        MessageBox.Show("Cannot divide by zero!");
                    break;
            }
            Display.Text = currentValue.ToString();
            isNewNumber = true;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentValue = 0;
            currentOperator = "";
            Display.Text = "0";
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text != "0")
            {
                double value = double.Parse(Display.Text);
                value = -value;
                Display.Text = value.ToString();
            }
        }

        private void Percentage_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text != "0")
            {
                double value = double.Parse(Display.Text);
                value /= 100;
                Display.Text = value.ToString();
            }
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!Display.Text.Contains(","))
                Display.Text += ",";
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButtion = (Button)sender;
            double value = double.Parse(Display.Text);
            double result = 0;
             switch(clickedButtion.Content.ToString())
             {
                 case "x²":
                     result = Math.Pow(value, 2);
                     Display.Text = result.ToString();
                     break;
                 case "√":
                     if (value >= 0)
                     {
                         result = Math.Sqrt(value);
                         Display.Text = result.ToString();
                     }
                     else
                     {
                         Display.Text = "Error: Invalid input";
                     }
                     break;
             }      
        }

        private void Display_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    } 
}

