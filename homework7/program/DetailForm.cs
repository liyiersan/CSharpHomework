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
    public partial class DetailForm : Form
    {
        public DetailForm()
        {
            InitializeComponent();
        }

        public DetailForm(uint i)
        {
            InitializeComponent();
            this.Text = $"OrderId:{i}";
            bindingSource1.DataSource = OrderForm.os.orderDict[i].details;

        }
    }
}
