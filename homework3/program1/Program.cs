using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program1
{
    interface Shape
    {
        void show();
    }
    
    class Triangle :Shape
    {
        private double a,b,c;//三角形的三边长
        public Triangle()
        {
            Console.WriteLine("创建三角形，请输入三边的长度：");
            Console.Write("a = ");
            a = double.Parse(Console.ReadLine());
            Console.Write("b = ");
            b = double.Parse(Console.ReadLine());
            Console.Write("c = ");
            c = double.Parse(Console.ReadLine());
        }

        public void show()
        {
            double p = (a + b + c) / 2;
            double s = Math.Pow(p*(p-a)*(p-b)*(p-c),0.5);//海伦公式
            Console.WriteLine("面积为：{0}",s);
        }
    }

    class Round : Shape
    {
        private double r;//三角形的三边长
        public Round()
        {    
            Console.WriteLine("创建圆形,请输入半径：");
            r = double.Parse(Console.ReadLine());
        }

        public void show()
        {
            const double PI = 3.14159;
            double s = PI*r*r;
            Console.WriteLine("面积为：{0}", s);
        }
    }

    class Square : Shape
    {
        private double a;
        public Square()
        {
            Console.WriteLine("创建正方形,请输入边长：");
            a = double.Parse(Console.ReadLine());
        }

        public void show()
        {
            double s = a*a;
            Console.WriteLine("面积为：{0}", s);
        }
    }

    class Rectangle: Shape
    {
        private double a,b;
        public Rectangle()
        {
            Console.WriteLine("创建矩形,请输入边长：");
            Console.Write("a = ");
            a = double.Parse(Console.ReadLine());
            Console.Write("b = ");
            b = double.Parse(Console.ReadLine());
        }

        public void show()
        {
            double s = a * b;
            Console.WriteLine("面积为：{0}", s);
        }
    }

    class ShapeFactory
    {
        public static Shape GetShape(String type)
        {
            Shape shape = null;
            if (type.Equals("三角形"))
            {
                shape = new Triangle();
                Console.WriteLine("初始化三角形！");
            }
            if (type.Equals("圆形"))
            {
                shape = new Round();
                Console.WriteLine("初始化圆形！");
            }
            if (type.Equals("正方形"))
            {
                shape = new Square();
                Console.WriteLine("初始化正方形！");
            }
            if (type.Equals("矩形"))
            {
                shape = new Rectangle();
                Console.WriteLine("初始化矩形！");
            }
            return shape;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Shape shape;
            shape = ShapeFactory.GetShape("三角形");
            shape.show();
            shape = ShapeFactory.GetShape("圆形");
            shape.show();
            shape = ShapeFactory.GetShape("正方形");
            shape.show();
            shape = ShapeFactory.GetShape("矩形");
            shape.show();
        }
    }
}
