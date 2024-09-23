using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    internal class Point
    {
        /*double x;
        double y; 
        
        public double X { get { return x; } set {  x = value; } }
        // ключ. слово value - открывает доступ к переданному значению
        public double Y { get { return y; } set {  y = value; } }

        public double GetX() 
        { 
            return x; 
        }
        public double GetY()
        {
            return y;
        }
        public void SetX(double x)
        {
            this.x = x; // в шарпе отсутствуют указатели, а следовательно и оператор ->
        }
        public void SetY(double y)
        {
            this.y = y;
        }*/

        public double X {  get; set; } 
        public double Y {  get; set; }

        //     Constructors:
        public Point(double x = 0, double y = 0) // благодаря значениям по умолчанию, этот конструктор с параметрами может явл.конструктором по умолчанию
        {  
            X = x; 
            Y = y;
            Console.WriteLine($"Constructor:\t {this.GetHashCode()}");
        }

        public Point(Point other) // в функ. копир. тоже ссылка
        {
            this.X = other.X;
            this.Y = other.Y;
            Console.WriteLine($"CopyConstructor: {this.GetHashCode()}");
        }

        // деструктор всегда приватный
        ~Point() { Console.WriteLine($"Destructor:\t {this.GetHashCode()}"); }

        //      Operators:
        // в статические обяз. нужно передать параметр
        public static Point operator+(Point left, Point right)
        {
            Point res = new Point();
            res.X = left.X + right.X;
            res.Y = left.Y + right.Y;
            return res;
        }
        public static Point operator ++(Point obj)
        {
            obj.X++;
            obj.Y++;
            return obj;
        }
        public static bool operator == (Point left, Point right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
            //return left.X != right.X && left.Y != right.Y;
        }

        //      Methods:
        public void Print()
        {
            Console.WriteLine($"X = {X}, Y = {Y}:");
        }
    }
}
