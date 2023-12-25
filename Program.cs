using System.Net.WebSockets;
using Tree;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("FILE EXPLORER");
        Console.WriteLine("----------------------------------------");
        var root = new Directory<string>("This PC");

        var directorioActual = root;

        while (true)
        {
            // root.PrintTree(directorioActual);

            Console.Write($"~{directorioActual.Value}> ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) && input.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) break;

            string[] tokens = input.Split(' ');

            switch (tokens[0])
            {
                case "ls":
                    directorioActual.PrintTree(directorioActual);
                    break;
                case "mkdir":
                    directorioActual.Attach(new Directory<string>(tokens[1]));
                    break;
                case "rm":
                    directorioActual.Remove(tokens[1]);
                    break;
                case "cd":
                    if (tokens[1] == "..")
                    {
                        directorioActual = root.FindParent(root, directorioActual);
                        break;
                    }
                    var folder = directorioActual.FindNode(tokens[1]);
                    if (folder == null) Console.WriteLine($"Folder {tokens[1]} not founded!");
                    else directorioActual = folder;
                    break;
            }
        }
    }
}