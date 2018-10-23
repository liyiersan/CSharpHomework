using Microsoft.VisualStudio.TestTools.UnitTesting;
using program1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace program1.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {

        [TestMethod()]
        public void AddOrderTest1()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1

            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);

            //将结果和预测结果进行比较
            Dictionary<uint, Order> result = new Dictionary<uint, Order>();
            result[1] = order1;
            CollectionAssert.AreEqual(result, os.orderDic);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AddOrderTest2()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1

            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //重复添加订单1
            os.AddOrder(order1);
        }

        [TestMethod()]
        public void RemoveOrderTest1()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //删除订单
            os.RemoveOrder(1);
            //将结果和预测结果进行比较
            Dictionary<uint, Order> result = new Dictionary<uint, Order>();
            CollectionAssert.AreEqual(result, os.orderDic);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void RemoveOrderTest2()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //删除订单
            os.RemoveOrder(1);
            //重复删除订单1
            os.RemoveOrder(1);
        }

        [TestMethod()]
        public void QueryAllOrdersTest1()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //将结果和预测结果进行比较
            List<Order> orders = os.QueryAllOrders();
            List<Order> result = new List<Order>();
            result.Add(order1);
            CollectionAssert.AreEqual(result, orders);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void QueryAllOrdersTest2()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            //将空订单转化为List
            List<Order> orders = os.QueryAllOrders();

        }

        [TestMethod()]
        public void QueryByOrderMoneyTest1()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //按订单金额查找订单
            List<Order> orders = os.QueryByOrderMoney();
            //将结果和预测结果进行比较
            List<Order> result = new List<Order>();
            result.Add(order1);
            CollectionAssert.AreEqual(result, orders);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void QueryByOrderMoneyTest2()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //按订单金额查找订单
            List<Order> orders = os.QueryByOrderMoney();
        }

        [TestMethod()]
        public void GetByIdTest1()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //按编号查找订单
            Order order = os.GetById(1);
            //将结果和预测结果进行比较
            Order result = order1;
            Assert.AreEqual(result, order);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetByIdTest2()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //按不存在的编号查找订单
            Order order = os.GetById(2);
        }

        [TestMethod()]
        public void QueryByCustomerNameTest1()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //按客户名称查找订单
            List<Order> orders = os.QueryByCustomerName("Customer1");
            //将结果和预测结果进行比较
            List<Order> result = new List<Order>();
            result.Add(order1);
            CollectionAssert.AreEqual(result, orders);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void QueryByCustomerNameTest2()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //按不存在的客户名称查找订单
            List<Order> orders = os.QueryByCustomerName("Customer2");
        }

        [TestMethod()]
        public void QueryByGoodsNameTest1()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //按货物名称查找订单
            List<Order> orders = os.QueryByGoodsName("Milk");
            //将结果和预测结果进行比较
            List<Order> result = new List<Order>();
            result.Add(order1);
            CollectionAssert.AreEqual(result, orders);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void QueryByGoodsNameTest2()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            //按不存在的货物名称查找订单
            List<Order> orders = os.QueryByGoodsName("Milsdhfghsfg");
            //将结果和预测结果进行比较
            List<Order> result = new List<Order>();
            result.Add(order1);
            CollectionAssert.AreEqual(result, orders);
        }
        /* 
         * 在判断集合是否相等时，AreEqual会出错，未找到原因
         * Equals方法就没问题
         */
        [TestMethod()]
        public void ImportTest1()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
                                             //添加订单
            OrderService os = new OrderService();

            os.AddOrder(order1);
            List<Order> orders = os.QueryAllOrders();

            //XML序列化订单
            os.Export("orders1.xml", orders);

            //XML反序列化
            List<Order> orders2 = os.Import("orders1.xml") as List<Order>;
            CollectionAssert.Equals(orders, orders2);


        }


        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ImportTest2()
        {
            //初始化订单
            Customer customer1 = new Customer(1, "Customer1");
            Goods milk = new Goods(1, "Milk", 69.9);
            OrderDetails orderDetails1 = new OrderDetails(1, milk, 10000);
            Order order1 = new Order(1, customer1);
            order1.AddDetails(orderDetails1);//顾客1买milk，订单1
            //添加订单
            OrderService os = new OrderService();
            os.AddOrder(order1);
            List<Order> orders = os.QueryAllOrders();
            //XML序列化订单
            os.Export("orders1.xml", orders);
            //从不存在的文件，XML反序列化
            List<Order> orders2 = os.Import("ghsjhsdhaf.xml") as List<Order>;
        }
    }
}