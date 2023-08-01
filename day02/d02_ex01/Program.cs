using System;
using System.Collections.Generic;
using System.Linq;
using Task = d02_ex01.Tasks.Task;

List<Task> taskList = new List<Task>();
var quit = false;

while (quit == false)
{
    Console.Write("Input commands: add, list, done, wontdo, quit(q)\n> ");
    string input = Console.ReadLine();
    
    switch (input)
    {
        case "add":
            Dictionary<string, string> addInput = CreateTaskInput();
            if (!Task.TryParse(addInput["title"], addInput["description"], addInput["deadline"],
                    addInput["type"], addInput["priority"], out Task newTask))
            {
                ErrorMassage("Input error. Check the input data and repeat the request.");
                break;
            }
            
            Console.WriteLine(newTask);
            taskList.Add(newTask);
            break;
        
        case "list":
            if (taskList.Any())
            {
                for (int i = 0; i < taskList.Count; i++)
                {
                    Console.WriteLine(taskList[i]);
                    if (i != taskList.Count - 1)
                        Console.WriteLine();
                }
            }
            else
                Console.WriteLine("The task list is still empty.");
            break;
        
        case "done":
            Console.Write("> Enter a title\n> ");
            string title = Console.ReadLine() ?? string.Empty;
            Task t = taskList.Find(t => t.Title.Equals(title));
            if (t is null)
                ErrorMassage("Input error. The task with this title was not found");
            else
            {
                if (t.TryMakeDone())
                    Console.WriteLine($"The task [{t.Title}] is completed!");
                else
                    ErrorMassage("Input error. Check the input data and repeat the request.");
            }
            break;
        
        case "wontdo":
            Console.Write("> Enter a title\n> ");
            title = Console.ReadLine() ?? string.Empty;
            t = taskList.Find(t => t.Title.Equals(title));
            if (t is null)
                ErrorMassage("Input error. The task with this title was not found");
            else
            {
                if (t.TryMakeIrrelevant())
                    Console.WriteLine($"The task [{t.Title}] is no longer relevant!");
                else
                    ErrorMassage("Input error. Check the input data and repeat the request.");
            }
            break;
        
        case "q":
        case "quit":
            quit = true;
            break;
        
        default:
            Console.WriteLine("Input error. Check the input data and repeat the request.");
            break;
    }
    Console.WriteLine();
}

return 1;

// local functions
int ErrorMassage(string message)
{
    Console.WriteLine(message);
    return -1;
}

Dictionary<string, string> CreateTaskInput()
{
    var input = new Dictionary<string, string>();
    
    Console.Write("> Enter a title\n> ");
    input.Add("title", Console.ReadLine() ?? string.Empty);
    Console.Write("> Enter a description\n> ");
    input.Add("description", Console.ReadLine() ?? string.Empty);
    Console.Write("> Enter the deadline\n> ");
    input.Add("deadline", Console.ReadLine() ?? string.Empty);
    Console.Write("> Enter the type\n> ");
    input.Add("type", Console.ReadLine() ?? string.Empty);
    Console.Write("> Assign the priority\n> ");
    input.Add("priority", Console.ReadLine() ?? string.Empty);

    return input;
}