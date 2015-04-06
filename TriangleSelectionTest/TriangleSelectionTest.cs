using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NUnit.Framework;

namespace TriangleSelectionTest
{
    [TestFixture]
    public class TriangleSelectionTest
    {
        [Test]
        public void Points()
        {
            var a = new Point(200, 100);
            var b = new Point(100, 225);
            var c = new Point(300, 225);
            var p = new Point(200, 150);

            var v0 = b - a;
            var v1 = c - a;
            var v2 = p - a;

            var d00 = Vector.Multiply(v0, v0);
            var d01 = Vector.Multiply(v0, v1);
            var d11 = Vector.Multiply(v1, v1);
            var d20 = Vector.Multiply(v2, v0);
            var d21 = Vector.Multiply(v2, v1);

            var invDenom = 1.0 / (d00 * d11 - d01 * d01);
            var v = (d11 * d20 - d01 * d21) * invDenom;
            var w = (d00 * d21 - d01 * d20) * invDenom;
            var u = 1.0f - v - w;

            Debug.WriteLine("v:{0}, w:{1}, u:{2}", v, w, u);

        }
    }
}
