using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
      class mkf //If a class is static all member must be static, else members can be selectively static
    {

        public static int num;
        public static void count()
        {num++;}

        public  int getNum() //This functions only hold at class,never pass to objects.
        {return num;}

        public static void ml(String message)
        {Console.WriteLine(message);}

        public static void m(String message)
        {Console.Write(message);}

        public static void cmp(int a,int b) {
            if (a > b) {ml("A is bigger!");}
            if (a < b) {ml("B is bigger!");}
        }


        public static void loop(int a)
        {for (int i = 0; i < a; i++) { ml("Looping de msg...."); };}

        //a is for the number of loop
        public static void loop(int a, int b, int c) {
            if (b > c)
            {
                for (int i = 0; i < a; i++) { ml("Looping de msg...."); };
            }
            else { ml("Sorry, c is bigger than b"); }
        }

    }
    class b {
       static void Main(string[] args)
            {
            mkf.m("Hello Alex");
            mkf.m("Hello Alex");
            mkf.m("Hello Alex");
            mkf.ml("Hello Alex");
            mkf.m("Hello Alex");
            mkf.m("Hello Alex");
            mkf.m("Hello Alex");

            
            //StaticVar s = new StaticVar();
            //s.count();
            //s.count();
            //s.count();
            //var a = StaticVar.num;
            //StaticVar.count();
            //StaticVar.getNum(); // getNum() function only accessible via Class
            //Console.WriteLine("Variable num: {0}", StaticVar.num);
            //mkf.m("hello");
            mkf newrectangle = new mkf();
            String x = "Test";
            newrectangle.getNum();
            
            


            mkf.ml("-------------------------");
            mkf.ml("           MENU          ");
            mkf.ml("-------------------------");
            mkf.ml("1. Option 1");
            mkf.ml("1. Option 2");
            mkf.ml("1. Option 3");
            mkf.ml("1. Option 4");
            mkf.cmp(1, 2);
            mkf.cmp(20,10);

            mkf.loop(1000);
            mkf.loop(20,5,3);



            //mkf.m("hello");
            
            Console.ReadKey();
            }

    }

        
}

