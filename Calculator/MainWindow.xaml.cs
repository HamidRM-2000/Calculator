using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      double num1 = 0, num2 = 0;
      bool cleartext;
      string operation;
      List<Button> Buttons;
      public MainWindow()
      {
         Buttons = new List<Button>();
         InitializeComponent();
         Buttons.Add(btnDivide);
         Buttons.Add(btnMinus);
         Buttons.Add(btnPlus);
         Buttons.Add(btnMulti);
      }

      private void btnAc_Click(object sender, RoutedEventArgs e)
      {
         if (CheckLableValue())
            lblResult.Content = "0";
         else
         {
            var text = lblResult.Content.ToString();
            var len = text.Length;
            lblResult.Content = text.Substring(0, len - 1);
         }
      }

      private void NumberBtn_Click(object sender, RoutedEventArgs e)
      {
         Button button = (Button)sender;
         if (cleartext)
         {
            lblResult.Content = "";
            cleartext = false;
         }
         if (CheckLableValue())
         {
            lblResult.Content = button.Content.ToString();
         }
         else
         {
            lblResult.Content += button.Content.ToString();
         }
      }


      private void btnPercent_Click(object sender, RoutedEventArgs e)
      {
         var num = double.Parse(lblResult.Content.ToString());
         lblResult.Content = (double)num / 100;
      }

      private void btnOperation_Click(object sender, RoutedEventArgs e)
      {
         ChangeColor(Buttons);
         Button button = (Button)sender;
         num1 = double.Parse(lblResult.Content.ToString());
         operation = button.Content.ToString();
         cleartext = true;
         button.Background = Brushes.DarkRed;
      }

      private void btnEqual_Click(object sender, RoutedEventArgs e)
      {
         ChangeColor(Buttons);
         num2 = double.Parse(lblResult.Content.ToString());
         switch (operation)
         {
            case "+":
               lblResult.Content = MyMath.Add(num1, num2);
               break;
            case "-":
               lblResult.Content = MyMath.Substract(num1, num2);
               break;
            case "*":
               lblResult.Content = MyMath.Multiple(num1, num2);
               break;
            case "/":
               lblResult.Content = MyMath.Divide(num1, num2);
               break;
            default: break;
         }
         operation = "";
      }

      private void btnDot_Click(object sender, RoutedEventArgs e)
      {
         if (!lblResult.Content.ToString().Contains('.'))
            lblResult.Content += ".";
      }

      private void btnNegetive_Click(object sender, RoutedEventArgs e)
      {
         if (!CheckLableValue())
         {
            var num = int.Parse(lblResult.Content.ToString());
            lblResult.Content = num * -1;
         }
      }

      #region MyMethods
      public bool CheckLableValue()
      {
         var text = lblResult.Content.ToString();
         return text == "0" || text == "";
      }
      public void ChangeColor(IEnumerable<Button> buttons)
      {
         foreach (var button in buttons)
         {
            if (button.Background == Brushes.DarkRed)
               button.Background = Brushes.Orange;
         }
      }

      #endregion
   }
   public class MyMath
   {
      public static double Add(double num1 = 0, double num2 = 0)
      {
         return num1 + num2;
      }
      public static double Substract(double num1 = 0, double num2 = 0)
      {
         return num1 - num2;
      }
      public static double Multiple(double num1 = 1, double num2 = 1)
      {
         return num1 * num2;
      }
      public static double Divide(double num1 = 1, double num2 = 1)
      {
         if (num2 == 0)
         {
            MessageBox.Show("Division by Zero is illegal", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return 0;
         }
         else
            return num1 / num2;
      }
   }
}
