using C5;

namespace MFramework.Common.Core.Collections
{
    public class GraphDictionary<TKey,TData>
    {
        #region Members

        private Graph<Rec<TKey, TData>> _graph;

        #endregion
        public  GraphDictionary(Fun<TKey, TKey, bool> isPredecessorPredicate)
                : this(isPredecessorPredicate,  GraphPredicate.Equality.Default(isPredecessorPredicate))
            {
                
            }

        public  GraphDictionary(Fun<TKey, TKey, bool> isPredecessorPredicate, Fun<TKey, TKey, bool> isEqualPredicate)
            {
                _graph = new Graph<Rec<TKey, TData>>(GraphPredecessorPredicate(isPredecessorPredicate), GraphEqualPredicate(isEqualPredicate));    
            
            }
        #region public
        public long Level()
        {
            return _graph.Level() - 1;
        }
        public long Count()
        {

            return _graph.Count();
        }
        public GraphDictionary<TKey, TData> AddNode(TKey key ,TData value)
        {
            _graph.AddNode(new Rec<TKey, TData>(key,value));
            return (this);

        }
        public IGraphTraverser<Rec<TKey,TData>> DeepFirstTraverse()
        {
            return _graph.DeepFirstTraverse();
        }
        public bool Exist(TKey key)
        {
            return _graph.Exist(new Rec<TKey, TData>(key,default(TData)));
        }
        public GraphNode<Rec<TKey,TData>> Find(TKey key)
        {
            return _graph.Find(new Rec<TKey, TData>(key, default(TData)));
        }
        public TData Value(TKey key)
        {
            return Find(key).Value.X2;
        }
        #endregion
        
        private static Fun<Rec<TKey,TData>, Rec<TKey,TData>, bool> GraphPredecessorPredicate(Fun<TKey, TKey, bool> predicate)
        {
            return ((r1,r2)=> predicate(r1.X1,r2.X1))
            ;
        }
        private static Fun<Rec<TKey, TData>, Rec<TKey, TData>, bool> GraphEqualPredicate(Fun<TKey, TKey, bool> predicate)
        {
            return ((r1, r2) => predicate(r1.X1, r2.X1))
            ;
        }
    }
}
