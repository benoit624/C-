using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{  
    class Program
    {
        static void Main(string[] args)
        {
#if false
            Node<int> tree = new Node<int>(comp, 17);
            tree.insert(9);
            tree.insert(29);
            tree.insert(3);
            tree.insert(13);
            tree.insert(23);
            tree.insert(40);
            tree.insert(1);
            tree.insert(8);
            tree.insert(11);
            tree.insert(42);
            //Console.WriteLine(tree.find(23));
            //tree.delete(23);
#elif false
            int[] value = {};
            Node<int> tree = new Node<int>(comp,value);
#elif true
            int[] value = { 17, 9, 29, 3, 13, 23, 40, 1, 8, 11, 42 };//,50,12,48,27,65,48,21,42,15,35,84,72
            Tree<int> tree = new Tree<int>(compint,value);
#endif
            tree.node.print();
#if false
            int test = 0;
            Console.WriteLine("find {0} : {1}",test,tree.find(test));
            Console.WriteLine("insert {0} : {1}", test,tree.insert(test));
            Console.WriteLine("find {0} : {1}", test,tree.find(test));
            Console.WriteLine("delete {0} : {1}", test,tree.delete(test));
            Console.WriteLine("find {0} : {1}", test,tree.find(test));
#elif true
            tree.RightRotation(ref tree.node);
            tree.LeftRotation(ref tree.node);
#elif true
            tree.LeftRightRotation(ref tree.node);
            tree.RightLeftRotation(ref tree.node);
#endif
            Console.WriteLine();
            tree.AVLInsert(6);
            tree.node.print();
            Console.WriteLine(tree.node.height());
            Console.ReadLine();
        }
        #region delegate
        static int compint(int a, int b)
        {
            return a - b;
        }
        static int compstring(string a, string b)
        {
            for (int i = 0; i < a.Length && i < b.Length; i++)
                if ((a[i] - b[i]) != 0)
                    return a[i] - b[i];
            return a.Length - b.Length;
        }
        #endregion
    }
    class Tree<T>
    {
        #region VARIABLE
        public Node<T> node;
        public delegate int CompType(T a, T b);
        public delegate void MapFunction(ref T elt);
        public CompType CompFunction;
        private delegate bool BinFunc(ref Node<T> node, T val);
        #endregion
        #region CONSTRUCTOR
        public Tree(CompType comp)
        {
            CompFunction = comp;
        }
        public Tree(CompType comp, T value):this(comp)
        {
            node = new Node<T>(value);
        }
        public Tree(CompType comp, T[] value):this(comp)
        {
            for (int i = 0; i < value.Length; i++)
            {
                insert(value[i]);
            }
        }
        #endregion
        #region TRAVERSAL
        public void depth_first_traversal(MapFunction function, order order = order.preorder)
        {
            dft(function, node, order);
        }
        private void dft(MapFunction function,Node<T> n , order order = order.preorder)
        {
            if (n.value == null)
                function(ref n.value);
            else
            {
                if (order == order.preorder)
                    function(ref n.value);
                if (n.childs[0] != null)
                    dft(function,n.childs[0], order);
                if (order == order.inorder)
                    function(ref n.value);
                if (n.childs[1] != null)
                    dft(function,n.childs[1], order);
                if (order == order.postorder)
                    function(ref n.value);

            }
        }
        public void breadth_first_traversal(MapFunction function)
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            Node<T> n;
            queue.Enqueue(node);
            do
            {
                n = queue.Dequeue();
                function(ref n.value);
                if (n.childs[0] != null)
                    queue.Enqueue(n.childs[0]);
                if (n.childs[1] != null)
                    queue.Enqueue(n.childs[1]);
            } while (queue.Count > 0);

        }
        private bool binary_traversal(ref Node<T> node, T val, BinFunc null_case, BinFunc equal_case)
        {
            if (node == null)
                return null_case(ref node, val);
            else
            {
                if (CompFunction(node.value, val) == 0)
                    return equal_case(ref node, val);
                else if (CompFunction(node.value, val) > 0)
                {
                    return binary_traversal(ref node.childs[0], val, null_case, equal_case);
                }
                else
                {
                    return binary_traversal(ref node.childs[1], val, null_case, equal_case); ;
                }
            }
        }
        #endregion
        #region FUNCTION
        public bool insert(T value)
        {
            BinFunc nul = delegate(ref Node<T> node, T val)
            {
                node = new Node<T>(val);
                return true;
            };
            BinFunc equal = (ref Node<T> node, T val) => false;
            return binary_traversal(ref this.node, value, nul, equal);
        }
        public bool find(T value)
        {
            BinFunc nul = (ref Node<T> node, T val) => false;
            BinFunc equal = (ref Node<T> node, T val) => true;
            return binary_traversal(ref this.node, value, nul, equal);
        }
        public bool delete(T value)
        {
            BinFunc nul = (ref Node<T> node, T val) => false;
            BinFunc equal = delegate(ref Node<T> node, T val)
            {
                Node<T> n;
                Node<T> supp;
                if (node.childs[0] != null)
                {
                    if (node.childs[0].childs[1] == null)
                    {
                        n = node.childs[0];
                        n.childs = node.childs;
                        n.childs[0] = null;
                        node = n;
                    }
                    else
                    {
                        supp = node;
                        n = node.childs[0];
                        while (n.childs[1] != null)
                        {
                            supp = n;
                            n = n.childs[1];
                        }
                        supp.childs[1] = n.childs[0];
                        n.childs = node.childs;
                        node = n;
                    }
                    
                }
                else
                {
                    node = node.childs[1];
                }   
                
                    
                return true;
            };
            return binary_traversal(ref this.node, value, nul, equal);
        }
        #endregion
        #region AVL
        public void LeftRotation(ref Node<T> node)
        {
            try
            {
                Node<T> n;
                n = node.childs[1];
                node.childs[1] = n.childs[0];
                n.childs[0] = node;
                node = n;
            }
            catch
            {
                throw new Exception("rotation Impossible");
            }

        }
        public void RightRotation(ref Node<T> node)
        {
            try
            {
                Node<T> n;
                n = node.childs[0];
                node.childs[0] = n.childs[1];
                n.childs[1] = node;
                node = n;
            }
            catch
            {
                throw new Exception("rotation Impossible");
            }

        }
        public void LeftRightRotation(ref Node<T> node)
        {
            try
            {
                Node<T> n;
                n = node.childs[0].childs[1];
                node.childs[0].childs[1] = n.childs[0];
                n.childs[0] = node.childs[0];
                node.childs[0] = n.childs[1];
                n.childs[1] = node;
                node = n;
                node = n;
            }
            catch
            {
                throw new Exception("rotation Impossible");
            }

        }
        public void RightLeftRotation(ref Node<T> node)
        {
            try
            {
                Node<T> n;
                n = node.childs[1].childs[0];
                node.childs[1].childs[0] = n.childs[1];
                n.childs[1] = node.childs[1];
                node.childs[1] = n;
                LeftRotation(ref node);
            }
            catch
            {
                throw new Exception("rotation Impossible");
            }

        }
        public bool AVLInsert(T value)
        {
            BinFunc nul = delegate(ref Node<T> node, T val)
            {
                node = new Node<T>(val);
                rebalance(ref this.node);
                return true;
            };
            BinFunc equal = (ref Node<T> node, T val) => false;
            return binary_traversal(ref this.node, value, nul, equal);
        }
        public void rebalance(ref Node<T> node)
        {
            if (node.childs[0] != null && node.childs[1] != null)
            {
                if ((node.childs[0].height() - node.childs[1].height()) < 1)
                    LeftRotation(ref node);
                else if ((node.childs[1].height() - node.childs[0].height()) < 1)
                    RightRotation(ref node);
                rebalance(ref node.childs[0]);
                rebalance(ref node.childs[1]);
            }
        }

        #endregion
        
    }
    enum order { preorder,postorder,inorder}
    class Node<T>
    {
        #region VARIABLE
        public T value;
        public Node<T>[] childs;
        #endregion
        public Node( T value)
        {
            this.value = value;
            childs = new Node<T>[2];
        } 
        public void print()
        {
            if(value == null)
                Console.Write("Ø");
            else
            {
                Console.Write("(" + value + ",");
                for (int i = 0; i < childs.Length; i++)
                {
                    try
                    {
                        childs[i].print();
                    }
                    catch
                    {
                        Console.Write("Ø");
                    }
                    if (i == 0)
                        Console.Write(",");
                    
                }
                Console.Write(")");
            }
            
        }
        public int height()
        {
            if (childs[0] == null && childs[1] == null)
                return 1;
            if (childs[0] == null)
                return 1 + childs[1].height();
            else if (childs[1] == null)
                return 1 + childs[0].height();
            else
                return 1 + Math.Max(childs[0].height(), childs[1].height());
        }

    }
}
