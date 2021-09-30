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

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FX f = new FX(panel1)
            {
                Scale = 20
            };
            var from = new Point(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            var to = new Point(int.Parse(textBox3.Text), int.Parse(textBox4.Text));
            f.DrawLineACDA(from,to);
            panel1.Refresh();

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
