using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphVisitor
{
    internal static class GraphVisitor
    {
        public static IEnumerable<IEnumerable<TNode>> VisitGraph<TNode>(
            IEnumerable<TNode> startNodes, TNode endNode, Func<TNode, IEnumerable<TNode>> routeGetter)
        {
            var routes = new List<IEnumerable<TNode>>();

            foreach (var startNode in startNodes)
            {
                routes.AddRange(TraverseRoute(new[] {startNode}, endNode, routeGetter));
            }

            return routes;
        }

        private static IEnumerable<IEnumerable<TNode>> TraverseRoute<TNode>(IEnumerable<TNode> route,
            TNode endNode, Func<TNode, IEnumerable<TNode>> routeGetter)
        {
            var routeList = route.ToList();
            var lastNode = routeList.Last();

            var result = new List<IEnumerable<TNode>>();

            foreach (var path in routeGetter(lastNode))
            {
                if (routeList.Contains(path))
                {
                    continue;
                }

                var runningRoute = routeList.Concat(new[] {path});

                if (path.Equals(endNode))
                {
                    result.Add(runningRoute);
                }
                else
                {
                    result.AddRange(TraverseRoute(runningRoute, endNode, routeGetter));
                }
            }

            return result;
        }
    }
}
