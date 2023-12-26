# Console Clone

This is a console clone project written in C#. It simulates a basic file system where you can navigate through directories, create new directories, and remove directories.

## Project Structure

- `Program.cs`: This is the main entry point of the application.
- `Directories.cs`: This file contains the `Directory` class which extends the `Tree` class. It provides methods for attaching subdirectories, finding nodes, removing nodes, finding parent directories, and printing the directory tree.
- `Tree.cs`: This file contains the `Tree` class which provides methods for attaching subtrees, removing nodes, and traversing the tree.

## How to Run

To run this project, you need to have .NET 8.0 installed on your machine. Once you have that, you can use the following command to run the project:

```sh
dotnet run
```

## Commands

- `ls` List the contents of the current directory.
- `mkdir <directory_name>`: Create a new directory in the current directory.
- `rm <directory_name>`: Remove a directory from the current directory.
- `cd <directory_name>`: Change the current directory to the specified directory.
- `cd ..`: Move up one directory level.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
