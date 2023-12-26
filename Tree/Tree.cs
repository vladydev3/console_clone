namespace Tree;

public class Tree<T>
{
    public T Value { get; set; }
    public Tree<T>[] Childrens { get; protected set; }

    public Tree(int degree, T value)
    {
        Childrens = new Tree<T>[degree];
        Value = value;
    }

    public Tree(T value, params Tree<T>[] subtrees)
    {
        Value = value;
        Childrens = subtrees;
    }

    public bool IsSibling(Tree<T> node1, Tree<T> node2)
    {
        if (node1 == null || node2 == null)
        {
            return false;
        }

        var parent1 = FindParent(this, node1);
        var parent2 = FindParent(this, node2);

        return parent1.Equals(parent2);
    }

    public bool IsCousin(Tree<T> node1, Tree<T> node2)
    {
        if (node1 == null || node2 == null)
        {
            return false;
        }

        var parent1 = FindParent(this, node1);
        var parent2 = FindParent(this, node2);

        return !parent1.Equals(parent2) && !IsSibling(node1, node2) && IsSibling(parent1, parent2);
    }

    public Tree<T> FindParent(Tree<T> tree, Tree<T> child)
    {
        if (tree == null) return null;

        foreach (var subtree in tree.Childrens)
        {
            if (subtree == child)
            {
                return tree;
            }

            var parent = FindParent(subtree, child);
            if (parent != null)
            {
                return parent;
            }
        }

        return null;
    }

    public bool IsLeaf
    {
        get { return Childrens.All(x => x == null); }
    }

    public int Degree
    {
        get { return Childrens.Length; }
    }

    public int Width
    {
        get
        {
            int maxWidth = 0;
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                maxWidth = Math.Max(maxWidth, levelSize);

                for (int i = 0; i < levelSize; i++)
                {
                    var current = queue.Dequeue();
                    foreach (var child in current.Childrens)
                    {
                        if (child != null)
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
            }

            return maxWidth;
        }
    }

    public int Diameter
    {
        get
        {
            if (this == null)
            {
                return 0;
            }

            int[] childrens = new int[Degree];

            for (int i = 0; i < Degree; i++)
            {
                if (Childrens[i] != null) childrens[i] = Height(Childrens[i]);
            }

            return 2 + childrens.OrderByDescending(x => x).Take(2).Sum();
        }
    }

    public int Height(Tree<T> tree)
    {
        if (tree == null)
        {
            return -1;
        }

        if (tree.IsLeaf)
        {
            return 0;
        }

        int[] childrens = new int[tree.Degree];

        for (int i = 0; i < tree.Degree; i++)
        {
            if (tree.Childrens[i] != null) childrens[i] = Height(tree.Childrens[i]);
        }

        return 1 + childrens.Max();
    }

    public virtual void Attach(int position, Tree<T> subtree)
    {
        Childrens[position] = subtree;
    }

    public virtual void Attach(params Tree<T>[] subtrees)
    {
        for (int i = 0; i < Math.Min(Childrens.Length, subtrees.Length); i++)
        {
            Childrens[i] = subtrees[i];
        }
    }

    public virtual void Remove(int position)
    {
        Attach(position, null);
    }

    public bool IsAncestor(Tree<T> who, Tree<T> fromWho)
    {
        if (fromWho == null)
        {
            return false;
        }

        if (fromWho == who)
        {
            return true;
        }

        foreach (var subtree in fromWho.Childrens)
        {
            if (IsAncestor(who, subtree))
            {
                return true;
            }
        }

        return false;
    }

    public virtual IEnumerable<T> PostOrden(Tree<T> tree)
    {
        if (tree == null)
        {
            yield break;
        }

        foreach (var subtree in tree.Childrens)
        {
            foreach (var item in PostOrden(subtree))
            {
                yield return item;
            }
        }

        yield return tree.Value;
    }


    public virtual IEnumerable<T> PreOrden(Tree<T> tree)
    {
        if (tree == null)
        {
            yield break;
        }

        yield return tree.Value;

        foreach (var subtree in tree.Childrens)
        {
            foreach (var item in PreOrden(subtree))
            {
                yield return item;
            }
        }
    }

    public virtual IEnumerable<T> BFS(Tree<T> tree)
    {
        if (tree == null)
        {
            yield break;
        }

        var queue = new Queue<Tree<T>>();
        var visited = new HashSet<Tree<T>>();

        queue.Enqueue(tree);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (!visited.Add(current)) continue;

            yield return current.Value;

            foreach (var subtree in current.Childrens)
            {
                if (subtree != null) queue.Enqueue(subtree);
            }
        }
    }

    public virtual IEnumerable<T> DFS(Tree<T> tree)
    {
        if (tree == null)
        {
            yield break;
        }

        var stack = new Stack<Tree<T>>();
        var visited = new HashSet<Tree<T>>();

        stack.Push(tree);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (!visited.Add(current)) continue;

            yield return current.Value;

            foreach (var subtree in current.Childrens)
            {
                if (subtree != null) stack.Push(subtree);
            }
        }
    }

    // public Tree<T> LCA(Tree<T> treeA, Tree<T> treeB)
    // {
    //     var parentA = FindParent(this, treeA);
    //     var parentB = FindParent(this, treeB);

    //     if (parentA == null || parentB == null) return null;

    //     if (parentA.Equals(parentB)) return parentA;


    // }

    public void PrintTree(Tree<T> node, string indent = "")
    {
        if (node == null)
        {
            // Console.WriteLine(" ");
            return;
        }

        Console.Write(indent);

        if (node.Childrens != null)
        {
            Console.Write("└─");
            indent += "  ";
        }
        else
        {
            Console.Write("├─");
            indent += "| ";
        }

        Console.WriteLine(node.Value);

        if (node.Childrens.Length > 0)
        {
            foreach (var child in node.Childrens)
            {
                PrintTree(child, indent);
            }
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Tree<T> other = (Tree<T>)obj;
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Value);
    }
}