namespace Tree;

public enum CommandType
{
    Ls,
    Mkdir,
    Rm,
    Cd,
    Exit,
    Mv,
    Cp,
}

public class Lexer
{
    public CommandType Command { get; private set; }
    public string Folder1 { get; private set; }
    public string Folder2 { get; private set; }

    public Lexer(string input)
    {
        char current = input[0];
        int index = 0;
        string token = string.Empty;
        Folder1 = string.Empty;
        Folder2 = string.Empty;

        while (current != ' ' && index < input.Length)
        {
            token += current;
            index++;
            if (index >= input.Length) break;
            current = input[index];
        }

        if (current == ' ')
        {
            index++;
            switch (token)
            {
                case "ls":
                    Command = CommandType.Ls;
                    break;
                case "mkdir":
                    Command = CommandType.Mkdir;
                    break;
                case "rm":
                    Command = CommandType.Rm;
                    break;
                case "cd":
                    Command = CommandType.Cd;
                    break;
                case "exit":
                    Command = CommandType.Exit;
                    break;
                case "mv":
                    Command = CommandType.Mv;
                    break;
                case "cp":
                    Command = CommandType.Cp;
                    break;
                default:
                    Console.WriteLine("Command {token} not found!");
                    break;
            }
        }


        if (index < input.Length) current = input[index];
        token = string.Empty;

        while (current != ' ' && index < input.Length)
        {
            token += current;
            index++;
            if (index >= input.Length) break;
            current = input[index];
        }

        Folder1 = token;

        if (current == ' ')
        {
            current = input[++index];
            Folder1 = token;
            token = string.Empty;
        }

        if (Command == CommandType.Mv || Command == CommandType.Cp)
        {
            while (current != ' ' && index < input.Length)
            {
                token += current;
                index++;
                if (index >= input.Length) break;

                current = input[index];
            }

            // index++;
            Folder2 = token;
            // token = string.Empty;
        }
    }
}