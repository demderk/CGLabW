using System;
using Xunit;
using LazyFX;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace FXTests
{
    public class Testing
    {

        public static IEnumerable<object[]> AbsoluteData()
        {
            yield return new object[] { 0, 0, 3, 3, 3000 };
            yield return new object[] { 0, 0, 0, 3, 3000 };
            yield return new object[] { 0, 0, 3, 0, 3000 };
            yield return new object[] { 0, 0, 6, 2, 3000 };
            yield return new object[] { 0, 0, 2, 6, 3000 };
            yield return new object[] { 0, 0, 1, 6, 3000 };
            yield return new object[] { 0, 0, 1, 2, 3000 };
            yield return new object[] { 0, 0, 7, 8, 3000 };
        }
        public static IEnumerable<object[]> StarData()
        {
            yield return new object[] { 3, 3, 0, 3, 3000 };
            yield return new object[] { 3, 3, 3, 0, 3000 };
            yield return new object[] { 3, 3, 3, 6, 3000 };
            yield return new object[] { 3, 3, 6, 3, 3000 };
            yield return new object[] { 3, 3, 0, 0, 3000 };
            yield return new object[] { 3, 3, 6, 6, 3000 };
            yield return new object[] { 3, 3, 0, 6, 3000 };
            yield return new object[] { 3, 3, 6, 0, 3000 };
        }


        [Theory(DisplayName = "XY ACDA Absolute Test")]
        [MemberData(nameof(AbsoluteData))]
        public void DrawLineACDAAbsCrashTest(int x1, int y1, int x2, int y2, int timeout)
        {
            FX Fx = new FX(new Panel());
            Assert.True(Task.Run(() =>
                {
                    Fx.DrawLine(new Point(x1, y1), new Point(x2, y2));
                }).Wait(TimeSpan.FromMilliseconds(timeout)));
        }
        [Theory(DisplayName = "XY BZH Absolute Test")]
        [MemberData(nameof(AbsoluteData))]
        public void DrawLineAbsCrashTest(int x1, int y1, int x2, int y2, int timeout)
        {
            FX Fx = new FX(new Panel());
            Assert.True(Task.Run(() =>
            {
                Fx.DrawLine(new Point(x1, y1), new Point(x2, y2));
            }).Wait(TimeSpan.FromMilliseconds(timeout)));
        }
        [Theory(DisplayName = "XY ACDA Star Test")]
        [MemberData(nameof(StarData))]
        public void DrawLineACDAStarCrashTest(int x1, int y1, int x2, int y2, int timeout)
        {
            FX Fx = new FX(new Panel());
            Assert.True(Task.Run(() =>
            {
                Fx.DrawLineACDA(new Point(x1, y1), new Point(x2, y2));
            }).Wait(TimeSpan.FromMilliseconds(timeout)));
        }
        [Theory(DisplayName = "XY BZH Star Test")]
        [MemberData(nameof(StarData))]
        public void DrawLineStarCrashTest(int x1, int y1, int x2, int y2, int timeout)
        {
            FX Fx = new FX(new Panel());
            
            Assert.True(Task.Run(() =>
            {
                Fx.DrawLine(new Point(x1, y1), new Point(x2, y2));
            }).Wait(TimeSpan.FromMilliseconds(timeout)));
        }
    }
}
