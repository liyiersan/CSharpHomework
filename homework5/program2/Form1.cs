using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace program2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "30";
            textBox2.Text = "20";
            textBox3.Text = "0.6";
            textBox4.Text = "0.7";
            textBox5.Text = "1";
            comboBox1.SelectedIndex = 0;
            pen = new Pen(Color.Blue);
            pen.Width = 1;
            textBox7.Text = "1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getColor();
            getNums();
            if (graphics == null) graphics = this.CreateGraphics();
            else graphics.Clear(BackColor);
            drawCayleyTree(10, 200, 310, 100, -Math.PI / 2);
        }

        private Graphics graphics;
        double th1 = 30 * Math.PI / 180;
        double th2 = 20 * Math.PI / 180;
        double per1 = 0.6;
        double per2 = 0.7;
        double k = 1;
        private Pen pen;//画笔
        


        void drawCayleyTree(int n, double x0, double y0, double leng, double th)
        {
            if (n == 0) return;

            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            double x2 = x0 + leng * k * Math.Cos(th);
            double y2 = y0 + leng * k * Math.Sin(th);
            drawLine(x0, y0, x1, y1);
            drawLine(x0, y0, x2, y2);

            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayleyTree(n - 1, x2, y2, per2 * leng, th - th2);
        }
        void drawLine(double x0, double y0, double x1, double y1)
        {
            graphics.DrawLine(pen,(int)x0, (int)y0, (int)x1, (int)y1);
        }
        public void getColor()
        {
            switch (comboBox1.Text)
            {
                case "Blue":
                    pen.Color = Color.Blue;
                    break;
                case "Black":
                    pen.Color = Color.Black;
                    break;
                case "Yellow":
                    pen.Color = Color.Yellow;
                    break;
                case "Red":
                    pen.Color = Color.Red;
                    break;
                case "Green":
                    pen.Color = Color.Green;
                    break;
            }
        }
        public void getNums()
        {
            try
            {
                th1 = Convert.ToDouble(textBox1.Text) * Math.PI / 180;
                th2 = Convert.ToDouble(textBox2.Text) * Math.PI / 180;
                per1 = Convert.ToDouble(textBox3.Text);
                per2 = Convert.ToDouble(textBox4.Text);
                k = Convert.ToDouble(textBox5.Text);
                pen.Width = (float)Convert.ToDouble(textBox7.Text);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
