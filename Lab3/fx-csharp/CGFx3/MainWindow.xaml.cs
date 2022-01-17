using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace CGFx3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Control> CanvasElements = new List<Control>();

        public MainWindow()
        {
            InitializeComponent();


        }

        private void XCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Canvas canvas = XCanvas;
            XCanvas.Children.Clear();
            MovableObject movable = new MovableObject();
            if (int.Parse(nField.Text) <= 0 || int.Parse(rField.Text) <= 0 || int.Parse(hField.Text) <= 0)
            {
                return;
            }
            movable.BuildRounded(int.Parse(nField.Text), int.Parse(rField.Text), int.Parse(hField.Text));
            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            if (CheckEnabled.IsChecked.Value)
                movable.BuildScreenPrpct(int.Parse(xField.Text, format), int.Parse(yField.Text, format), int.Parse(zField.Text, format),30);
            else
                movable.BuildScreen(int.Parse(xField.Text, format), int.Parse(yField.Text, format), int.Parse(zField.Text, format));
            foreach (var item in movable.To2dlines())
            {
                Canvas.SetLeft(item, 100);
                Canvas.SetTop(item, 100);
                canvas.Children.Add(item);
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Canvas canvas = XCanvas;
            XCanvas.Children.Clear();
            MovableObject movable = new MovableObject();
            if (int.Parse(nField.Text) <= 0 || int.Parse(rField.Text) <= 0 || int.Parse(hField.Text) <= 0)
            {
                return;
            }
            movable.BuildRounded(int.Parse(nField.Text), int.Parse(rField.Text), int.Parse(hField.Text));
            if (SliderX == null || SliderY == null || SliderZ == null)
                return;
            if (CheckEnabled.IsChecked.Value)
                movable.BuildScreenPrpct((int)SliderX.Value, (int)SliderY.Value, (int)SliderZ.Value,30);
            else
                movable.BuildScreen((int)SliderX.Value, (int)SliderY.Value, (int)SliderZ.Value);
            foreach (var item in movable.To2dlines())
            {
                Canvas.SetLeft(item, 100);
                Canvas.SetTop(item, 100);
                canvas.Children.Add(item);
            }
        }

    }
}
