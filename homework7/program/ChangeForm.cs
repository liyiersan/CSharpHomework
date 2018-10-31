using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ordertest;

namespace program
{
    public partial class ChangeForm : Form
    {
        private uint Id;
        public ChangeForm()
        {
            InitializeComponent();
        }

        public ChangeForm(uint id)
        {
            InitializeComponent();
            Id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string s = textBox1.Text.ToString();
                uint i = uint.Parse(textBox2.Text.ToString());
                Customer customer = new Customer(i, s);
                OrderForm.os.UpdateCustomer(Id, customer);
                OrderForm.bindingSource1.DataSource = OrderForm.os.orderDict.Values.ToList();
                this.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }       
        }
    }
}
