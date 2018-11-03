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
    public partial class CreateForm : Form
    {
        Dictionary<string, double> goodsDic = new Dictionary<string, double>();
        Dictionary<string, uint> goodsIdDic = new Dictionary<string, uint>();
        Order order;
        public CreateForm()
        {
            InitializeComponent();
            goodsDic["milk"] = 5.8;
            goodsDic["bread"] = 4.8;
            goodsDic["apple"] = 3.8;
            goodsDic["pen"] = 6.8;
            goodsDic["orange"] = 2.8;

            goodsIdDic["milk"] = 1;
            goodsIdDic["bread"] = 2;
            goodsIdDic["apple"] = 3;
            goodsIdDic["pen"] = 4;
            goodsIdDic["orange"] = 5;
            order = new Order();
        }

        private void AddOrderDetailButton_Click(object sender, EventArgs e)
        {
            try
            {
                uint orderDetailId = uint.Parse(OrderDetailIdTextBox.Text.ToString());
                string gName = comboBox1.Text;
                uint gQuantity = uint.Parse(GoodsQuantityTextBox.Text.ToString());
                Goods goods = new Goods(goodsIdDic[gName], gName, goodsDic[gName]);
                OrderDetail orderDetail = new OrderDetail(orderDetailId, goods, gQuantity);
                order.AddDetails(orderDetail);
                bindingSource1.DataSource = null;
                bindingSource1.DataSource = order.details;
            }catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            
        }

        private void AddOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                uint Id = uint.Parse(OrderIdTextBox.Text.ToString());
                uint cId = uint.Parse(CustomerIdTextBox.Text.ToString());
                Customer customer = new Customer(cId, CustomerNameTextBox.Text.ToString());
                order.Id = Id;
                order.Customer = customer;
                OrderForm.os.AddOrder(order);                
                this.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

    }
}
