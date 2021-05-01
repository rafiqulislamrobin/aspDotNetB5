using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            //List<int> list1 = new List<int>() { 1, 2, 3, 4, 5 };

            List<int> list2 = new List<int>() { 6, 7, 10, 1, 2, 3, 4, 5, 8, 9 };



            IList<int> list3 = (SmartSort.Sort<int>(list2, compareMethod));

            foreach (var item in list3)
            {
                Console.WriteLine(item);
            }

        }
        public static int compareMethod(int x, int y)
        {
            int a = 0;
            if (x > y)
            {
                a = y;
            }
            else
            {
                a = x;
            }
            return a;
        }
    }
    public static class SmartSort
    {

        public static IList<int> Sort<T>(this IList<int> elements, Func<int, int,
        int> compare)

        {
            IList<int> numbers = elements;
            IList<int> finalList = new List<int>();

            int z = 0;
            for (int i = 0; i < numbers.Count;)
            {

                for (int j = 0; j < numbers.Count; j++)
                {

                    if (numbers[i] < numbers[j])
                    {
                        continue;
                    }
                    else
                    {
                        z = Program.compareMethod(numbers[i], numbers[j]);
                        i = j;

                    }

                }
                i = 0;
                numbers.Remove(z);
                finalList.Add(z);
            }



            return finalList;


        }

    }


}
