using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace program
{
    public partial class FindForm : Form
    {
        public FindForm()
        {
            InitializeComponent();
        }

        private void IdFindButton_Click(object sender, EventArgs e)
        {
            try
            {
                uint id = uint.Parse(textBox1.Text.ToString());
                new OrderForm(id).ShowDialog();
                //this.Close();
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }

        }

        private void GoodsFindButton_Click(object sender, EventArgs e)
        {
            try
            {
                string s = textBox2.Text.ToString();
                new OrderForm(s,0).ShowDialog();
               // this.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void CustomerFindButton_Click(object sender, EventArgs e)
        {
            try
            {
                string s = textBox3.Text.ToString();
                new OrderForm(s).ShowDialog();
                this.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            
        }
    }
}
