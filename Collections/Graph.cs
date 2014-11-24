using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using C5;

namespace MFramework.Common.Core.Collections
{
    public static class GraphPredicate
    {
        public static class Predecessor
        {
            public  static Fun<Type, Type, bool> Type = (t1, t2) => t2.IsInstanceOfType(t1);
            public static Fun<TNumber, TNumber, bool> Number<TNumber>() where TNumber:IComparer
            {

                return ((TNumber n1, TNumber n2) =>  n1.Compare(n1, n2) == 1 );
            }
        }
        public static class Equality
        {
            public static Fun<T, T, bool> Default<T>(Fun<T, T, bool> predecessorPredicate)
            {
                return (n1, n2) => predecessorPredicate(n1, n2) && predecessorPredicate(n2, n1);
            }
            public static Fun<TNumber, TNumber, bool> Number<TNumber>() where TNumber : IComparer
            {
                return ((TNumber n1, TNumber n2) => n1.Compare(n1, n2) == 0);
            }
        }
    }

    public class Graph<T>:GraphNode<T>.GraphInternal<T>
    {
        public Graph(Fun<T, T, bool> isPredecessorPredicate)
            : this(isPredecessorPredicate, GraphPredicate.Equality.Default(isPredecessorPredicate))
        {
                
        }
        public Graph(Fun<T, T, bool> isPredecessorPredicate, Fun<T, T, bool> isEqualPredicate)
            : base(isPredecessorPredicate, isEqualPredicate)
        {

        }        
        
    }
    public interface IGraphNode<T>
    {
        
    }
    public class GraphNode<T>
    {
        
        
        #region Static Members
        #region Private 
        private static Fun<T1, T1, bool> DefaultEqualPredicate<T1>(Fun<T1, T1, bool> predecessorPredicate)
        {
            return (n1, n2) => predecessorPredicate(n1, n2) && predecessorPredicate(n2, n1);
        }
        #endregion
        #endregion
        #region member class attribute
            
        private T _value;
        private Fun<T, T, bool> _isPredecessorPredicate;
        private Fun<T, T, bool> _isEqualPredicate;
        private List<GraphNode<T>> _childs;

        #endregion


        #region Constructor

        

        private GraphNode(T value, Fun<T, T, bool> isPredecessorPredicate,Fun<T, T, bool> isEqualPredicate)
        {
            _value = value;
            _isPredecessorPredicate = isPredecessorPredicate;
            _isEqualPredicate = isEqualPredicate;
            _childs = new List<GraphNode<T>>();
        }

        #endregion
        
        
        #region Public
        
        
        public bool HavingChilds()
        {
            return  _childs.Count > 0;
        }
        public long Level()
        {
            long l=0;
            _childs.ForEach(n=> l = Math.Max(l,n.Level()));
            return 1 +  l ;
        }
        public bool Exist(T value)
        {
            if (IsEqualTo(value))
            {
                return true;
            }
            return (_childs.Any(n => n.IsEqualTo(value)));
        }
        public IGraphTraverser<T> DeepFirstTraverse()
        {
            return  DeepFirstTraverse(false);
        }
        public IGraphTraverser<T> DeepFirstTraverse(bool exclude)
        {
            return new DeepFirstGraphTraverser<T>(this, ! exclude);
        }
        public T Value { get { return _value; } }
        public GraphNode<T> Find(T value)
        {
            
            if (IsEqualTo(value))
            {
                return this;
            }
            return (from g in _childs 
                    select g.Find(value) into found 
                    where found != null 
                    select (found)).FirstOrDefault();
        }
        #endregion


        #region private
        private bool IsAPredecessorOf(GraphNode<T> node)
        {
            return _isPredecessorPredicate(_value, node._value);
        }
        private bool IsRelatedTo(GraphNode<T> node)
        {
            return IsAPredecessorOf(node) || node.IsAPredecessorOf(this);
        }
        private bool IsEqualTo(GraphNode<T> node)
        {
            return IsEqualTo(node._value);
        }
        private bool IsEqualTo(T value)
        {
            return _isEqualPredicate(_value, value);
        }
        
        private long ChildCount()
        {
            long childCount=1;
            _childs.ForEach((n)=> childCount +=n.ChildCount());
            return (childCount);

        }
        private void AddChild(GraphNode<T> node)
        {
            int index = _childs.FindIndex(0, (t) => t.IsRelatedTo(node));
            if (index == -1)
            {
                _childs.Add(node);
            }
            else
            {
                _childs[index] = _childs[index].AddNode(node);

            }
        }

        private GraphNode<T> AddNode(GraphNode<T> handler)
        {
            if (IsAPredecessorOf(handler))
            {
                AddChild(handler);
                return (this);
                
            }
            if (handler.IsAPredecessorOf(this))
            {
                handler.AddChild(this);
                return (handler);

            }
            return (null);



        }

        #endregion


        public abstract class GraphInternal<T>
        {


            //private List<GraphNode<T>> _roots;
            private GraphNode<T> _root;
            private Fun<T, T, bool> _isPredecessorPredicate;
            private Fun<T, T, bool> _isEqualPredicate;
            #region Constructors

            

            protected GraphInternal(Fun<T, T, bool> isPredecessorPredicate, Fun<T, T, bool> isEqualPredicate)
            {
                _root = new GraphNode<T>(default(T), (n1, n2) => true, (n1, n2) => false); // the root is always predecessor of nodes and is always different to all
                _isPredecessorPredicate = isPredecessorPredicate;
                _isEqualPredicate = isEqualPredicate;
            }

            #endregion

            #region public
            public long Level()
            {
                return _root.Level() - 1;
            }
            public long Count()
            {
                
                return _root.ChildCount()-1;
            }
            public GraphInternal<T> AddNode(T value)
            {
                _root.AddNode(new GraphNode<T>(value, _isPredecessorPredicate, _isEqualPredicate));
                return (this);
                
            }
            public IGraphTraverser<T> DeepFirstTraverse()
            {
                return _root.DeepFirstTraverse(false);
            }
            public bool Exist(T value)
            {
                return _root.Exist(value);
            }
            public GraphNode<T> Find(T value)
            {
                return _root.Find(value);
            }
            #endregion
            
        }
        
        
        

       
        private class DeepFirstGraphTraverser<T> : IGraphTraverser<T>
        {
            #region Private
            //private GraphInternal<T> _graph;
            private GraphNode<T> _root;
            private readonly bool _includeRoot;
            #endregion

            
            public DeepFirstGraphTraverser(GraphNode<T> root)
                : this(root, true)
            {

            }
            public DeepFirstGraphTraverser(GraphNode<T> root, bool includeRoot)
            {
                _root = root;
                _includeRoot = includeRoot;
            }
            private bool TraverseNode(GraphNode<T> node, Fun<T, bool> action, bool excludeNode)
            {
                if (!excludeNode && !action(node._value)) { return (false); }
                return node._childs.All(n => TraverseNode(n, action, false));

            }
            public IGraphTraverser<T> Iterate(Fun<T, bool> action)
            {
                TraverseNode(_root, action, !_includeRoot);
                return (this);
            }
        }
    }
    public interface IGraphTraverser<T>
    {
        IGraphTraverser<T> Iterate(Fun<T, bool> Action);
    }
}
