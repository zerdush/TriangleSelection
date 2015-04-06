using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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

namespace TriangleSelection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _drag;
        private Point _a;
        private Vector _v0;
        private Vector _v1;
        private double _d00;
        private double _d01;
        private double _d11;
        private double _invDenom;

        public MainWindow()
        {
            InitializeComponent();
            _a = Triangle.Points[0];
            var b = Triangle.Points[1];
            var c = Triangle.Points[2];
            _v0 = b - _a;
            _v1 = c - _a;
            _d00 = Vector.Multiply(_v0, _v0);
            _d01 = Vector.Multiply(_v0, _v1);
            _d11 = Vector.Multiply(_v1, _v1);
            _invDenom = 1.0 / (_d00 * _d11 - _d01 * _d01);

            //var points = Triangle.Points.Select(p => new System.Drawing.Point(p.X, p.Y));
            //GraphicsPath triangleGraphicsPath = new GraphicsPath(); 
            //Triangle.IsV
            //mouseTopLabel.Content = Canvas.GetTop(Ball);
            //mouseTopLabel.Content = Canvas.GetLeft(Ball);
        }

        private void Ball_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _drag = true;
        }

        private void Ball_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _drag = false;
        }

        private void Ball_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_drag) return;
            var x = e.GetPosition(Container).X;
            var y = e.GetPosition(Container).Y;
            var p = new Point(x, y);
            var v2 = p - _a;
            
            var d02 = Vector.Multiply(_v0, v2);
            var d12 = Vector.Multiply(_v1, v2);

            var u = (_d11 * d02 - _d01 * d12) * _invDenom;
            var v = (_d00 * d12 - _d01 * d02) * _invDenom;

            vLabel.Content = v;
            uLabel.Content = u;

            Ball.SetValue(Canvas.LeftProperty, x - 15);
            Ball.SetValue(Canvas.TopProperty, y - 15);
            
            mouseTopLabel.Content = y;
            mouseLeftLabel.Content = x;
        }


    

    }
}
