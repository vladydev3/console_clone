using System.Net.Mail;

namespace Tree;

public class Directory<T> : Tree<string>
{
    public new Directory<T>[] Childrens { get; protected set; }

    public Directory(string directory) : base(10, directory)
    {
        Childrens = new Directory<T>[10];
    }

    public void Attach(Directory<T> subdirectory)
    {
        for (int i = 0; i < Childrens.Length; i++)
        {
            if (Childrens[i] == null)
            {
                Childrens[i] = subdirectory;
                break;
            }
        }
    }

    public Directory<T> FindNode(string name)
    {
        if (Value.Equals(name))
        {
            return this;
        }

        foreach (var child in Childrens)
        {
            if (child == null) continue;
            var result = child.FindNode(name);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
    public void Remove(string name)
    {
        bool found = false;
        for (int i = 0; i < Childrens.Length; i++)
        {
            if (Childrens[i] == null) continue;
            if (Childrens[i].Value == name)
            {
                Childrens[i] = null;
                found = true;
                break;
            }
        }
        if (!found) System.Console.WriteLine($"'{name}' not founded in this directory");
    }

    public Directory<T> FindParent(Directory<T> tree, Directory<T> child)
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

    public void PrintTree(Directory<T> node, string indent = "")
    {
        if (node == null)
        {
            // Console.WriteLine(" ");
            return;
        }

        Console.Write(indent);

        if (!node.Childrens.All(x => x == null))
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
}