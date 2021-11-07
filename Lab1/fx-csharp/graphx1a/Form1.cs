using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LazyFX;

namespace graphx1a
{
    public partial class Form1 : Form
    {
        Graphics g = null;
        FX Fx;
        public Form1()
        {
            InitializeComponent();
            Fx = new FX(panel1)
            {
                Scale = 20
            };
            this.Height = 600;
            this.Width = 510;
            Rebuild();
            //this.SizeChanged += (o, s) =>  // Игрушка дьявола. Дополнительное задание, лучше не трогать.
            //{
            //Rebuild();
            //};
        }

        private void Rebuild() 
        {
            Fx.Clear();
            int height = (this.Height - 39); // 39 Пикселей размер шапки в 10й винде
            int width = this.Width;
            var from1 = new Point(0, height / Fx.Scale - 1);
            var to1 = new Point(width / Fx.Scale / 2, 0);
            Fx.DrawLine(from1, to1, Color.Red);
            var from2 = new Point(width / Fx.Scale / 2, 0);
            var to2 = new Point(width / Fx.Scale - 1, height / Fx.Scale - 1);
            Fx.DrawLine(from2, to2, Color.Red);
            var from = new Point(0, height / Fx.Scale - 1);
            var to = new Point(width / Fx.Scale - 1, height / Fx.Scale - 1);
            Fx.DrawLineACDA(from, to, Color.Green, 10);
            Fx.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DrawPixel(Graphics graphics, Point pos)
        {
            graphics.DrawRectangle(new Pen(Color.Black, 10), pos.X, pos.Y, 10, 10);
        }
    }
}
