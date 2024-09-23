using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
    internal class Fraction
    {
        public int Integer { get; set; }
        public int Numerator { get; set; }
        // если нет фильтрации данных - применяем автосво-ва, которые не требуют объявления переменной, поскольку сами, неявно её объявляют.

        int denominator;
        // обычные св-ва, кот. применяются к переменной 'denominator':
        public int Denominator
        {
            get => denominator;
            set => denominator = value == 0 ? 1 : value;
        }

        //      Constructors:
        public Fraction()
        {
            Console.WriteLine($"DefaultConstructor:\t {GetHashCode()}");
        }

        public Fraction(int integer) 
        {
            this.Integer = integer;
            Console.WriteLine($"1ArgConstructor: {GetHashCode()}");
        }
        public Fraction(int numerator, int denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
            Console.WriteLine($"Constructor:\t {GetHashCode()}");
        }
        public Fraction(int integer, int numerator, int denominator) 
        {
            this.Integer = integer;
            this.Numerator = numerator;
            this.Denominator = denominator;
            Console.WriteLine($"Constructor:\t {GetHashCode()}");
        }
        public Fraction(Fraction other)
        {
            this.Integer = other.Integer;
            this.Numerator = other.Numerator;
            this.Denominator = other.Denominator;
            Console.WriteLine($"CopyConstructor: {GetHashCode()}");
        }

        ~Fraction() 
        {
            Console.WriteLine($"Destructor:\t {GetHashCode()}");
        }

        //          Operators:
        public static Fraction operator *(Fraction l, Fraction r)
        {
            // больше кода, но отработает чуть быстрее
            /*Fraction left = l.Improper();
            Fraction right = r.Improper();
            Fraction res = new Fraction
                (
                left.Numerator * right.Numerator,
                left.Denominator * right.Denominator
                );
            return res;*/

            return new Fraction
                (
                    l.Improper().Numerator * r.Improper().Numerator,
                    l.Improper().Denominator * r.Improper().Denominator
                ).Reduce().Proper();
            // если функция возвр. объект, то для него можно вызвать методы
        }
        public static Fraction operator/(Fraction left, Fraction right)
        {
            return left * right.Inverted();
        }

        public static Fraction operator++(Fraction obj)
        {
            obj = obj.Proper();
            obj.Integer++;
            return obj;
        }
        public static Fraction operator --(Fraction obj)
        {
            obj = obj.Proper(); // приводим дробь к правильному виду
            obj.Integer--; // уменьшаем целую часть
            return obj; // возвращаем измененный объект
        }

        //          Comparison Operators: (всегда перегружаются попарно)
        public static bool operator ==(Fraction left, Fraction right)
        {
            // переводим дроби в неправильные и перемножаем "крестиком"
            return left.Improper().Numerator * right.Improper().Denominator ==
                right.Improper().Numerator * left.Improper().Denominator;
        }
        public static bool operator !=(Fraction left, Fraction right)
        {
            return !(left == right);   
        }
        public static bool operator >(Fraction left, Fraction right)
        {
            return left.Improper().Numerator * right.Improper().Denominator > 
                right.Improper().Numerator * left.Improper().Denominator;
        }
        public static bool operator <(Fraction left, Fraction right)
        {
            return left.Improper().Numerator * right.Improper().Denominator <
                right.Improper().Numerator * left.Improper().Denominator;
        }
        public static bool operator >=(Fraction left, Fraction right)
        {
            return left.Improper().Numerator * right.Improper().Denominator >=
                right.Improper().Numerator * left.Improper().Denominator;
        }
        public static bool operator <=(Fraction left, Fraction right)
        {
            return left.Improper().Numerator * right.Improper().Denominator <=
                right.Improper().Numerator * left.Improper().Denominator;
        }

        //          Methods:
        Fraction Proper()
        {
            Fraction copy = new Fraction(this);
            copy.Integer += copy.Numerator / copy.Denominator;
            copy.Numerator %= copy.Denominator;
            return copy;
        }

        Fraction Improper()
        {
            Fraction copy = new Fraction(this);
            copy.Numerator += copy.Integer * copy.Denominator;
            copy.Integer = 0;
            return copy;
        }
        public Fraction Inverted()
        {
            Fraction inverted = new Fraction(this).Improper(); // вызываем конструктор копирования
            (inverted.Numerator, inverted.Denominator) = (inverted.Denominator, inverted.Numerator);
            return inverted;
        }
        public Fraction Reduce() 
        {
            int more, less, rest;
            if (Numerator > Denominator)
            {
                more = Numerator;
                less = Denominator;
            }
            else
            {
                less = Numerator;
                more = Denominator;
            }
            do
            {
                rest = more % less;
                more = less; // нод остаётся здесь
                less = rest;
            } while (rest > 0);
            int GCD = more; // GCD - Greatest Common Divisor - Наибольший общий делитель

            Numerator /= GCD;
            Denominator /= GCD;

            return this;
        }
        public void Print()
        {
            if(Integer != 0) Console.Write(Integer);
            if (Numerator != 0)
            {
                if (Integer != 0) Console.Write("(");
                Console.Write($"{Numerator}/{Denominator}");
                if (Integer != 0) Console.Write(")");
            }
            else if (Integer == 0) Console.Write(0);
            Console.WriteLine();
        }

        public override string ToString()
        {
            //return base.ToString();
            string print = "";
            if (Integer != 0) print += Integer;
            if (Numerator != 0)
            {
                if (Integer != 0) print += "(";
                print += $"{Numerator}/{Denominator}";
                if (Integer != 0) print += ")";
            }
            else if (Integer == 0) print += 0;
            return print;
        }
    }
}
