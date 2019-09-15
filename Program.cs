using System;
using System.Collections.Generic;
using System.Linq;

namespace delivery_algorithms
{
    class Program
    {
        static void Main(string[] args)
        {           
            Console.WriteLine(MinimumTime(6, new int[] { 1,2,5,10,35,89 }));
            Console.ReadKey();
            Console.WriteLine();

            /*
            Console.Write(optimalUtilization(20,
                new List<List<int>> { new List<int> { 1, 8 }, new List<int> { 2, 7 }, new List<int> { 3, 14 } },
                new List<List<int>> { new List<int> { 1, 5 }, new List<int> { 2, 10 }, new List<int> { 3, 14 } }));
            */
            /*
            Console.Write(optimalUtilization(20,
                new List<List<int>> { new List<int> { 1, 8 }, new List<int> { 2, 15 }, new List<int> { 3, 9 } },
                new List<List<int>> { new List<int> { 1, 8 }, new List<int> { 2, 11 }, new List<int> { 3, 12 } }));
            */
            /*
            Console.Write(optimalUtilization(10,
                new List<List<int>> { new List<int> { 1, 3 }, new List<int> { 2, 5 }, new List<int> { 3, 7 }, new List<int> { 4, 10 } },
                new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 2, 3 }, new List<int> { 3, 4 }, new List<int> { 4, 5 } }));
            */
            /*
            Console.Write(optimalUtilization(7,
                new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 2, 4 }, new List<int> { 3, 6 } },
                new List<List<int>> { new List<int> { 1, 2 } }));
            */

            var response = OptimalUtilization(7,
                new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 2, 4 }, new List<int> { 3, 6 } },
                new List<List<int>> { new List<int> { 1, 0 } });

            foreach(var item in response)
            {
                foreach(var element in item)
                {
                    Console.WriteLine(element);
                }
            }

            Console.ReadKey();
        }
        public static int MinimumTime(int numOfParts, int[] parts)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                var x = parts[i];
                var j = i;
                while (j > 0 && parts[j - 1].CompareTo(x) > 0)
                {
                    parts[j] = parts[j - 1];
                    j = j - 1;
                }
                parts[j] = x;
            }
            var result = 0;
            var list = new List<int>();
            list.AddRange(parts);
            var total = 0;

            for (int i = 0; i < parts.Length - 1; i++)
            {
                result = list[0] + list[1];
                total += result;
                list.Remove(list[1]);
                list.Remove(list[0]);
                if (list.Count == 0) break;
                list.Add(result);
                list.Sort();
            }

            return total;
        }

        public static List<List<int>> OptimalUtilization(int maxTravelDist,
                                    List<List<int>> forwardRouteList,
                                    List<List<int>> returnRouteList)
        {
            var response = new List<List<int>>();
            int fId = 0, rId = 0;

            foreach(var f in forwardRouteList)
            {
                foreach(var r in returnRouteList)
                {
                    if (f[1] + r[1] == maxTravelDist)
                    {
                        fId = f[0];
                        rId = r[0];
                        if (response.Select(x => x.Where(y => y.Equals(fId) || y.Equals(rId))).Count() == 0)
                            response.Add(new List<int> { fId, rId });
                    }
                    if (f[1]+r[1]<= maxTravelDist)
                    {
                        if (fId != f[0]) fId = f[0];
                        if (rId != r[0]) rId = r[0];
                    }
                }
            }

            if (fId != 0 && rId != 0)
            {
                response.Add(new List<int> { fId, rId });
            }

            return response;
        }
    }
}
