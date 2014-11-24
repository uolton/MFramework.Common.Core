using System.Collections.Generic;

namespace MFramework.Common.Core.Collections
{
    public class GraphCollector<T>
    {
        #region Private members

        private Graph<T> _graph;

        #endregion
        #region Constructor
        public GraphCollector(Graph<T> g)
        {
            _graph = g;
        }
        public List<T> Collect()
        {
            List<T> l=new List<T>();
            _graph.DeepFirstTraverse().Iterate(x1 =>
                {
                    l.Add(x1);
                    return (true);
                });
            return l;
        }
        #endregion
    }
}
