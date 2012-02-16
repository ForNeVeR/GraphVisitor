using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVisitor
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintRoutes(Test1());
            Console.WriteLine();
            PrintRoutes(Test2());
        }

        static void PrintRoutes<T>(IEnumerable<IEnumerable<T>> routes)
        {
            foreach (var route in routes)
            {
                Console.WriteLine(string.Join(" -> ", route));
            }
        }

        static IEnumerable<IEnumerable<int>> Test1()
        {
            /*
             * 1     2
             *  \   /
             *   v v
             *    3
             *    |
             *    v
             *    4
             */
            var pathDictionary = new Dictionary<int, int>
                {
                    {1, 3},
                    {2, 3},
                    {3, 4}
                };

            var routes = GraphVisitor.VisitGraph(new[] { 1, 2 }, 4, node => new[] { pathDictionary[node] });

            return routes;
            /* 
             * Output:
             * 1 -> 3 -> 4
             * 2 -> 3 -> 4
             */
        }

        static IEnumerable<IEnumerable<int>> Test2()
        {
            /*
             * 1 --> 2
             * |^   /|
             * | \ v |
             * |  3  |
             * |  |  |
             * |  v  |
             * `->4<-'
             *    |
             *    v
             *    5
             */
            var pathDictionary = new Dictionary<int, int[]>
                {
                    {1, new[]{2, 4}},
                    {2, new[]{3, 4}},
                    {3, new[]{4, 1}},
                    {4, new[]{5}}
                };

            var routes = GraphVisitor.VisitGraph(new[] { 1, 2 }, 5, node => pathDictionary[node]);

            return routes;
            /* 
             * Output:
             * 1 -> 2 -> 3 -> 4 -> 5
             * 1 -> 2 -> 4 -> 5
             * 1 -> 4 -> 5
             * 2 -> 3 -> 4 -> 5
             * 2 -> 3 -> 1 -> 4 -> 5
             * 2 -> 4 -> 5
             */
        }
    }
}
