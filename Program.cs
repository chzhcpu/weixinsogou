using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinSogou
{
    class Program
    {
        static void Main(string[] args)
        {
            GzhSearcher gzhs = new GzhSearcher();
            List<Gzh> list= gzhs.Search("识林");
            Console.Read();
        }
    }
}
