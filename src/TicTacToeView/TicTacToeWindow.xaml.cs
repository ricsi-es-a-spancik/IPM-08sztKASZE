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

namespace TicTacToeView
{
    /// <summary>
    /// Interaction logic for TicTacToeWindow.xaml
    /// </summary>
    public partial class TicTacToeWindow : Window
    {
        public TicTacToeWindow()
        {
            InitializeComponent();
        }
    }

    public static class Constants
    {
        public const Double ButtonSize = 100;
        public const Double Margin = 30;
    }

    public class SizeToPixelSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value * Constants.ButtonSize + Constants.Margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value - Constants.Margin) / Constants.ButtonSize;
        }
    }

    public class SizeToFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value * 2;
        }
    }

    public class WindowHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value * Constants.ButtonSize + Constants.Margin) + 50 + Constants.Margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (((int)value - Constants.Margin - 50) - Constants.Margin) / Constants.ButtonSize;
        }
    }

    public class WindowWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value * Constants.ButtonSize + Constants.Margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value - Constants.Margin) / Constants.ButtonSize;
        }
    }

}
