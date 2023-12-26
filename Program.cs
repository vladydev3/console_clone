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

            var lexer = new Lexer(input);

            switch (lexer.Command)
            {
                case CommandType.Ls:
                    directorioActual.PrintTree(directorioActual);
                    break;
                case CommandType.Mkdir:
                    directorioActual.Attach(new Directory<string>(lexer.Folder1));
                    break;
                case CommandType.Rm:
                    directorioActual.Remove(lexer.Folder1);
                    break;
                case CommandType.Cd:
                    if (lexer.Folder1 == "..")
                    {
                        directorioActual = root.FindParent(root, directorioActual);
                        break;
                    }
                    var folder = directorioActual.FindNode(lexer.Folder1);
                    if (folder == null) Console.WriteLine($"Folder {lexer.Folder1} not founded!");
                    else directorioActual = folder;
                    break;
                case CommandType.Mv:
                    var folder1 = root.FindNode(lexer.Folder1);
                    var folder2 = root.FindNode(lexer.Folder2);
                    if (folder1 == null || folder2 == null) Console.WriteLine($"Folder {lexer.Folder1} or {lexer.Folder2} not founded!");
                    else
                    {
                        root.FindParent(root, folder1).Remove(folder1.Value);
                        folder2.Attach(folder1);
                    }
                    break;
                case CommandType.Cp:
                    var folder3 = root.FindNode(lexer.Folder1);
                    var folder4 = root.FindNode(lexer.Folder2);
                    if (folder3 == null || folder4 == null) Console.WriteLine($"Folder {lexer.Folder1} or {lexer.Folder2} not founded!");
                    else
                    {
                        folder4.Attach(folder3);
                    }
                    break;
                default:
                    Console.WriteLine("Command {token} not found!");
                    break;
            }
        }
    }
}