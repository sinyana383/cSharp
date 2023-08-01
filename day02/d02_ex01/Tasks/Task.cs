using System;
using System.Globalization;

namespace d02_ex01.Tasks
{
    public class Task
{
    public string Title { get; }
    public string Description { get; }
    public DateTime? DueDate { get; }
    public Category Type { get; }
    public Priority? Priority { get; }
    public TaskState State { get; }

    public bool TryMakeDone() => State.SetDone();
    public bool TryMakeIrrelevant() => State.SetIrrelevant();

    private Task(string title, string description, TaskState state)
    {
        Title = title;
        Description = description;
        State = state;
    }
    public Task(string title, string summary, DateTime? dueDate, Category type, Priority? priority, TaskState state)
    {
        Title = title;
        Description = summary;
        DueDate = dueDate;
        Type = type;
        Priority = priority;
        State = state;
    }

    public override string ToString()
    {
        string output = $"- {Title}\n" +
                        $"[{Type}] [{State}]\n" +
                        $"Priority: {Priority}";
        if (DueDate is not null)
        {
            DateTime d = DueDate ?? DateTime.Today;
            output += $", Due till {d.ToString("d", new CultureInfo("en-GB"))}";
        }

        output += "\n";
        output += Description;
        
        return output;
    }

    public static bool TryParse(string title, string summary, string dueDateData, string typeData, string priorityData,
        out Task task)
    {
        task = new Task(title, summary, new TaskState());
        DateTime? dueDate = null;
        Priority? priority = Tasks.Priority.Normal;
        Category type;

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(typeData) || !Enum.TryParse(typeData, out type))
            return false;
        if (!string.IsNullOrEmpty(dueDateData))
        {
            DateTime date;
            if (!DateTime.TryParse(dueDateData, out date))
            {
                if (!DateTime.TryParseExact(dueDateData, "MM/dd/yyyy", new CultureInfo("en-GB"),
                        DateTimeStyles.None, out date))
                    return false;
            }
            dueDate = date;
        }
        if (!string.IsNullOrEmpty(priorityData))
        {
            if (!Enum.TryParse(priorityData, out Tasks.Priority newPriority))
                return false;
            priority = newPriority;
        }

        task = new Task(title, summary, dueDate, type, priority, new TaskState());
        return true;
    }
}
}