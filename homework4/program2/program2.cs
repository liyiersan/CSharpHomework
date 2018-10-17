using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace program2
{
    abstract class Things
    {
        private string name;
        private uint id;
        public string Name { get { return name; } set { name = value; } }
        public uint ID { get { return id; } set { id = value; } }
        public abstract string Info { get; }
    } 
    //顾客类
    class Customer:Things
    {
        public Customer(uint id,string name )
        {
            Name = name;
            ID = id;    
        }

        public override string Info{ get { return $"name: {Name}, ID: {ID}";} }
    }
    //货物类
    class Goods:Things
    {
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
        public override string Info { get { return $"name: {Name}, ID: {ID}, price: {Price}"; } }
    }
    //订单明细类
    class OrderDetails
    {
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
   class Order
    {
        private List<OrderDetails> details = new List<OrderDetails>();
        public uint ID { get; set; }
        public Customer Customer { get; set; }

        public Order(uint orderId, Customer customer)
        {
            ID = orderId;
            Customer = customer;
        }

        public List<OrderDetails> Details
        {
            get => this.details;
        }
        //添加订单明细
        public void AddDetails(OrderDetails orderDetail)
        {
            if (this.Details.Contains(orderDetail))
            {
                throw new Exception($"orderDetails-{orderDetail.Info} is already existed!");
            }
            details.Add(orderDetail);
        }
        //删除订单明细
        public void RemoveDetails(uint orderDetailId)
        {
            details.RemoveAll(d => d.ID == orderDetailId);
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
    class OrderService
    {
        Dictionary <uint,Order> orderDic;
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
            orderDic.Remove(orderId);
        }
        //转化为List
        public List<Order> QueryAllOrders()
        {
            return orderDic.Values.ToList();
        }
        //通过ID查找订单
        public Order GetById(uint orderId)
        {
            return orderDic[orderId];
        }
        //通过货物名称查找订单
        public List<Order> QueryByGoodsName(string goodsName)
        {
            List<Order> result = new List<Order>();
            foreach (Order order in orderDic.Values)
            {
                foreach (OrderDetails detail in order.Details)
                {
                    if (detail.Goods.Name == goodsName)
                    {
                        result.Add(order);
                        break;
                    }
                }
            }
            return result;
        }
        //通过顾客名称查找订单
        public List<Order> QueryByCustomerName(string customerName)
        {
            var query = orderDic.Values.Where(order => order.Customer.Name == customerName);
            return query.ToList();
        }
        //修改订单
        public void UpdateOrder(uint orderId, Order order)
        {
            if (orderDic.ContainsKey(orderId))
            {
                orderDic[orderId] = order;
            }
            else
            {
                throw new Exception($"order-{orderId} is not existed!");
            }
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
                
                OrderDetails orderDetails1 = new OrderDetails(1, apple, 8);
                OrderDetails orderDetails2 = new OrderDetails(2, eggs, 2);
                OrderDetails orderDetails3 = new OrderDetails(3, milk, 1);

                Order order1 = new Order(1, customer1);
                Order order2 = new Order(2, customer2);
                Order order3 = new Order(3, customer2);
                order1.AddDetails(orderDetails1);//顾客1买苹果，订单1
                order1.AddDetails(orderDetails2);//顾客1买鸡蛋，订单1
                order1.AddDetails(orderDetails3);//顾客1买牛奶，订单1
                //order1.AddOrderDetails(orderDetails3);
                order2.AddDetails(orderDetails2);//顾客2买鸡蛋，订单2
                order2.AddDetails(orderDetails3);//顾客2买牛奶，订单2
                order3.AddDetails(orderDetails3);//顾客2买牛奶，订单3
                
                OrderService os = new OrderService();
                os.AddOrder(order1);
                os.AddOrder(order2);
                os.AddOrder(order3);

                Console.WriteLine("GetAllOrders");
                List<Order> orders = os.QueryAllOrders();
                foreach (Order od in orders)
                    Console.WriteLine(od.Info);//显示当前所有的订单信息

                Console.WriteLine("GetOrdersByCustomerName:'Customer2'");
                orders = os.QueryByCustomerName("Customer2");
                foreach (Order od in orders)
                    Console.WriteLine(od.Info);

                Console.WriteLine("GetOrdersByGoodsName:'apple'");
                orders = os.QueryByGoodsName("apple");
                foreach (Order od in orders)
                    Console.WriteLine(od.Info);

                Console.WriteLine("Remove order(id=2) and qurey all");
                os.RemoveOrder(2);
                os.QueryAllOrders().ForEach(
                    od => Console.WriteLine(od.Info));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}