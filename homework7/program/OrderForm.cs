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
    public partial class OrderForm : Form
    {
        public static OrderService os; 
        public OrderForm()
        {
            InitializeComponent();
            Customer customer1 = new Customer(1, "Customer1");
            Customer customer2 = new Customer(2, "Customer2");

            Goods milk = new Goods(1, "Milk", 69.9);
            Goods eggs = new Goods(2, "eggs", 4.99);
            Goods apple = new Goods(3, "apple", 5.59);

            OrderDetail orderDetails1 = new OrderDetail(1, apple, 800);
            OrderDetail orderDetails2 = new OrderDetail(2, eggs, 2);
            OrderDetail orderDetails3 = new OrderDetail(3, milk, 1);

            Order order1 = new Order(1, customer1);
            Order order2 = new Order(2, customer2);
            Order order3 = new Order(3, customer2);
            order1.AddDetails(orderDetails1);
            order1.AddDetails(orderDetails2);
            order1.AddDetails(orderDetails3);

            order2.AddDetails(orderDetails2);
            order2.AddDetails(orderDetails3);
            order3.AddDetails(orderDetails3);

            os = new OrderService();
            os.AddOrder(order1);
            os.AddOrder(order2);
            os.AddOrder(order3);

            bindingSource1.DataSource = os.orderDict.Values.ToList();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            new CreateForm().ShowDialog();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                uint i = uint.Parse(s);
                os.RemoveOrder(i);  
                bindingSource1.DataSource = os.orderDict.Values.ToList();
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            new FindForm().ShowDialog();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            try
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                uint i = uint.Parse(s);
                new ChangeForm(i).ShowDialog();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ShowDetailButton_Click(object sender, EventArgs e)
        {
            try
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                uint i = uint.Parse(s);
                new DetailForm(i).ShowDialog();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}
