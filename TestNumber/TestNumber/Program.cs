using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var collectionNumber = new List<decimal>() { 1, 6, 2, 3, 4, 13, 10, 5, 110, 7, 2, 2.5m, 0.5m, 2.999m, 0.001m, -1 };

            const int X = 3;

            if (!collectionNumber.Any())
            {
                Console.WriteLine("Коллекция пуста");
                Thread.Sleep(5000);
                return;
            }


            try
            {
                collectionNumber.Sort();

                for (var i = 0; i < collectionNumber.Count() - 1; i++)
                {
                    if (collectionNumber[i] > X)
                        break;

                    for (var j = i + 1; j < collectionNumber.Count(); j++)
                    {
                        if (collectionNumber[i] + collectionNumber[j] == X)
                        {
                            Console.WriteLine(X + " = " + collectionNumber[i].ToString() + " + " + collectionNumber[j].ToString());
                        }
                        else if (collectionNumber[i] + collectionNumber[j] > X)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Исключение", e);
            }


            Thread.Sleep(10000);
        }
    }
}
