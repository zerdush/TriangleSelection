using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly Point _a;
        private Vector _v0;
        private Vector _v1;
        private readonly double _d00;
        private readonly double _d01;
        private readonly double _d11;
        private readonly double _invDenom;
        private Point _b;
        private Point _c;

        public MainWindow()
        {
            InitializeComponent();
            _a = Triangle.Points[0];
            _b = Triangle.Points[1];
            _c = Triangle.Points[2];
            _v0 = _b - _a;
            _v1 = _c - _a;
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
            //_drag = false;
        }

        private void Container_OnMouseMove(object sender, MouseEventArgs e)
        {
            //DragIt(e);
            //e.Handled = true;
        }

        private void DragIt(MouseEventArgs e)
        {
            if (!_drag) return;
            var x = e.GetPosition(Container).X;
            var y = e.GetPosition(Container).Y;
            var ballX = Canvas.GetLeft(Ball) + 15;
            var ballY = Canvas.GetTop(Ball) + 15;
            var p = new Point(x, y);
            var ballPoint = new Point(ballX, ballY);
            double u;
            double v;
            double ballU;
            double ballV;
            
            GetUV(p, out u, out v);
            GetUV(ballPoint, out ballU, out ballV);

            vLabel.Content = ballV;
            uLabel.Content = ballU;

            if (u < 0 || v < 0 || u + v > 1)
            {
                if ((u + v) > 1)
                {
                    Ball.SetValue(Canvas.LeftProperty, x - 15);
                }
                else if (u<0 && v<0)
                {
                    return;
                }
                else if (u < 0)
                {
                    if (ballV < Double.Epsilon) return;
                    Ball.SetValue(Canvas.TopProperty, y - 15);
                    var x1 = y*_v1.X/_v1.Y + _b.X + 4.999;
                    Ball.SetValue(Canvas.LeftProperty, x1);
                }
                else if (v < 0)
                {
                    if (ballU < Double.Epsilon) return;
                    Ball.SetValue(Canvas.TopProperty, y - 15);
                    var x1 = y * _v0.X / _v0.Y +_c.X - 34.99;
                    Ball.SetValue(Canvas.LeftProperty, x1);
                }
            }
            else
            {
                Ball.SetValue(Canvas.LeftProperty, x - 15);
                Ball.SetValue(Canvas.TopProperty, y - 15);
            }
            mouseTopLabel.Content = y;
            mouseLeftLabel.Content = x;
        }

        private void GetUV(Point p, out double u, out double v)
        {
            var v2 = p - _a;


            var d02 = Vector.Multiply(_v0, v2);
            var d12 = Vector.Multiply(_v1, v2);

            u = (_d11*d02 - _d01*d12)*_invDenom;
            v = (_d00*d12 - _d01*d02)*_invDenom;
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            DragIt(e);
        }

        private void MainWindow_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _drag = false;
        }
    }
}
