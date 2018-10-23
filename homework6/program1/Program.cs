using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace program1
{
    public abstract class Things
    {
        private string name;
        private uint id;
        public string Name { get { return name; } set { name = value; } }
        public uint ID { get { return id; } set { id = value; } }
        public abstract string Info { get; }
    }
    //顾客类
    public class Customer : Things
    {
        public Customer() { }
        public Customer(uint id, string name)
        {
            Name = name;
            ID = id;
        }
        public override string Info { get { return $"name: {Name}, ID: {ID}"; } }
    }
    //货物类
    public class Goods : Things
    {
        public Goods() { }
        private double price;
        public Goods(uint id, string name, double price)
        {
            Name = name;
            ID = id;
            Price = price;
        }
        public double Price
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Invalid Price");
                else
                    price = value;
            }
            get { return price; }
        }
        public override string Info
        {
            get
            {
                return $"name: {Name}, ID: {ID}, price:{ Price}";
            }
        }
    }
    //订单明细类
    public class OrderDetails
    {
        public OrderDetails() { }
        public uint ID { get; set; }
        public Goods Goods { get; set; }
        public uint Quantity { get; set; }
        public OrderDetails(uint id, Goods goods, uint quantity)
        {
            this.ID = id;
            this.Goods = goods;
            this.Quantity = quantity;
        }
        public string Info
        {
            get
            {
                return $"ID: {ID}, Goods: {Goods.Info}, quantity: {Quantity}";
            }
        }
        public override bool Equals(object obj)
        {
            var detail = obj as OrderDetails;
            return detail != null &&
            Goods.ID == detail.Goods.ID &&
            Quantity == detail.Quantity;
        }
        public override int GetHashCode()
        {
            var hashCode = 1522631281;
            hashCode = hashCode * -1521134295 + Goods.Name.GetHashCode();
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }
    }
    //订单类
    [Serializable]
    public class Order
    {
        private double money;
        private List<OrderDetails> details = new List<OrderDetails>();
        public uint ID { get; set; }
        public Customer Customer { get; set; }
        public Order(uint orderId, Customer customer)
        {
            ID = orderId;
            Customer = customer;
            money = 0;
        }
        public Order() { }
        public double Money
        {
            get { return money; }
            set { money = value; }
        }
        public List<OrderDetails> Details
        {
           get =>this.details;
        }
        //添加订单明细
        public void AddDetails(OrderDetails orderDetail)
        {
            if (this.Details.Contains(orderDetail))
            {
                throw new Exception($"orderDetails-{orderDetail.Info} is already existed!");
            }
            details.Add(orderDetail);
            Money += orderDetail.Goods.Price * orderDetail.Quantity;
        }
        //删除订单明细
        public void RemoveDetails(uint orderDetailId)
        {
            //details.RemoveAll(d => d.ID == orderDetailId);
            foreach (OrderDetails d in details)
            {
                if (d.ID == orderDetailId)
                {
                    details.Remove(d);
                    Money -= d.Goods.Price * d.Quantity;
                }
            }


        }
        public string Info
        {
            get
            {
                string result = $"orderId:{ID}, customer:{Customer.Info}";
                details.ForEach(od => result += "\n\t" + od.Info);
                return result;
            }
        }
    }
    //订单操作类
    public class OrderService
    {
        public Dictionary<uint, Order> orderDic;

        public OrderService()
        {
            orderDic = new Dictionary<uint, Order>();
        }
        //添加订单
        public void AddOrder(Order order)
        {
            if (orderDic.ContainsKey(order.ID))
                throw new Exception($"order-{order.Info} is already existed!");
            orderDic[order.ID] = order;
        }
        //删除订单
        public void RemoveOrder(uint orderId)
        {
            if (orderDic.ContainsKey(orderId))
                orderDic.Remove(orderId);
            else
                throw new Exception($"order-{orderId} is not existed!");
        }

        //转化为List
        public List<Order> QueryAllOrders()
        {
            var query = orderDic.Values.ToList();
            if (query.Count == 0)
                throw new Exception($"No order is existed!");
            return query;
        }

        //查找⾦额⼤于⼀万的订单
        public List<Order> QueryByOrderMoney()
        {
            var query = from s in orderDic.Values
                        where s.Money > 10000
                        select s;
            if (query.Count() == 0)
                throw new Exception($"The orderMoney > 10000 is not existed!");
            return query.ToList();
        }

        //通过ID查找订单
        public Order GetById(uint orderId)
        {
            if (orderDic.ContainsKey(orderId))
                return orderDic[orderId];
            else
                throw new Exception($"order-{orderId} is not existed!");
        }
        //通过顾客名称查找订单
        public List<Order> QueryByCustomerName(string customerName)
        {
            var query = orderDic.Values.Where(order => order.Customer.Name ==
           customerName);
            if (query.Count() == 0)
                throw new Exception($"customer-{customerName} is not existed!");
            return query.ToList();
        }
        //通过商品名称查询订单
        public List<Order> QueryByGoodsName(string goodsName)
        {
            var query = from s in orderDic.Values
                        from d in s.Details
                        where d.Goods.Name == goodsName
                        select s;
            if (query.Count() == 0)
                throw new Exception($"goods-{goodsName} is not existed!");
            return query.ToList();
        }

        //XML序列化
        public void Export(String fileName, Object obj)
        {
            XmlSerializer xmlser = new XmlSerializer(obj.GetType());
            XmlSerialize(xmlser, fileName, obj);
        }
        //从XML文档载入订单
        public object Import(String fileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            object obj = XmlDeserialize(xmlser, fileName);
            return obj;
        }

        public static void XmlSerialize(XmlSerializer ser, string fileName, object obj)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            ser.Serialize(fs, obj);
            fs.Close();
        }

        public static object XmlDeserialize(XmlSerializer ser, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            object obj = ser.Deserialize(fs);
            fs.Close();
            return obj;
        }
    }


    class MainClass
    {
        public static void Main()
        {
            try
            {
                Customer customer1 = new Customer(1, "Customer1");
                Customer customer2 = new Customer(2, "Customer2");
                Goods milk = new Goods(1, "Milk", 69.9);
                Goods eggs = new Goods(2, "eggs", 4.99);
                Goods apple = new Goods(3, "apple", 5.59);
                OrderDetails orderDetails1 = new OrderDetails(1, apple, 8000);
                OrderDetails orderDetails2 = new OrderDetails(2, eggs, 2);
                OrderDetails orderDetails3 = new OrderDetails(3, milk, 10);
                Order order1 = new Order(1, customer1);
                Order order2 = new Order(2, customer2);
                Order order3 = new Order(3, customer2);
                order1.AddDetails(orderDetails1);//顾客1买苹果，订单1
                
                order1.AddDetails(orderDetails2);//顾客1买鸡蛋，订单1
                order1.AddDetails(orderDetails3);//顾客1买⽜奶，订单1
                                                 //order1.AddOrderDetails(orderDetails3);
                order2.AddDetails(orderDetails2);//顾客2买鸡蛋，订单2
                order2.AddDetails(orderDetails3);//顾客2买⽜奶，订单2
                order3.AddDetails(orderDetails3);//顾客2买⽜奶，订单3
                //添加订单
                OrderService os = new OrderService();
                os.AddOrder(order1);
                os.AddOrder(order2);
                os.AddOrder(order3);

                //显示当前所有的订单信息
                Console.WriteLine("GetAllOrders");
                List<Order> orders = os.QueryAllOrders();
                foreach (Order od in orders)
                    Console.WriteLine(od.Info);

                //XML序列化
                os.Export("orders.xml", orders);

                //显示xml文本
                string s = File.ReadAllText("orders.xml");
                Console.WriteLine(s);

                //XML反序列化
                List<Order> orders2 = os.Import("orders.xml") as List<Order>;

                //打印订单
                if (orders2 != null)
                {
                    foreach (Order od in orders2)
                        Console.WriteLine(od.Info);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
