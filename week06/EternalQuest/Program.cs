using System;
using System.Collections.Generic;
using System.IO;

// =========================
// BASE CLASS (Inheritance)
// =========================
public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public abstract bool IsComplete();
    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string SaveString();
}

// =========================
// SIMPLE GOAL
// =========================
public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override bool IsComplete() => _isComplete;

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return Points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        return _isComplete ? "[X]" : "[ ]";
    }

    public override string SaveString()
    {
        return $"SimpleGoal|{Name}|{Description}|{Points}|{_isComplete}";
    }
}

// =========================
// ETERNAL GOAL
// =========================
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override bool IsComplete() => false;

    public override int RecordEvent() => Points;

    public override string GetStatus() => "[∞]";

    public override string SaveString()
    {
        return $"EternalGoal|{Name}|{Description}|{Points}";
    }
}

// =========================
// CHECKLIST GOAL
// =========================
public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _targetCount = target;
        _bonus = bonus;
        _currentCount = 0;
    }

    public override bool IsComplete()
    {
        return _currentCount >= _targetCount;
    }

    public override int RecordEvent()
    {
        _currentCount++;

        if (_currentCount == _targetCount)
        {
            return Points + _bonus;
        }

        return Points;
    }

    public override string GetStatus()
    {
        string mark = IsComplete() ? "[X]" : "[ ]";
        return $"{mark} Completed {_currentCount}/{_targetCount}";
    }

    public override string SaveString()
    {
        return $"ChecklistGoal|{Name}|{Description}|{Points}|{_targetCount}|{_bonus}|{_currentCount}";
    }
}

// =========================
// GOAL MANAGER
// =========================
public class GoalManager
{
    private List<Goal> _goals = new();
    private int _score = 0;

    public void CreateGoal()
    {
        Console.WriteLine("Choose goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choice: ");
        string choice = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string desc = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (choice == "1")
        {
            _goals.Add(new SimpleGoal(name, desc, points));
        }
        else if (choice == "2")
        {
            _goals.Add(new EternalGoal(name, desc, points));
        }
        else if (choice == "3")
        {
            Console.Write("Target times: ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("Bonus: ");
            int bonus = int.Parse(Console.ReadLine());

            _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }

        Console.WriteLine("Goal created!\n");
    }

    public void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        int i = 1;

        foreach (Goal g in _goals)
        {
            Console.WriteLine($"{i}. {g.GetStatus()} {g.Name} ({g.Description})");
            i++;
        }

        Console.WriteLine();
    }

    public void RecordEvent()
    {
        ListGoals();
        Console.Write("Which goal number did you complete? ");
        int index = int.Parse(Console.ReadLine()) - 1;

        int points = _goals[index].RecordEvent();
        _score += points;

        Console.WriteLine($"\nYou earned {points} points! Total score: {_score}\n");
    }

    public void Save()
    {
        Console.Write("File name: ");
        string file = Console.ReadLine();

        using StreamWriter sw = new(file);
        sw.WriteLine(_score);

        foreach (Goal g in _goals)
        {
            sw.WriteLine(g.SaveString());
        }

        Console.WriteLine("Saved!\n");
    }

    public void Load()
    {
        Console.Write("File name: ");
        string file = Console.ReadLine();

        string[] lines = File.ReadAllLines(file);
        _score = int.Parse(lines[0]);
        _goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] p = lines[i].Split("|");

            if (p[0] == "SimpleGoal")
            {
                SimpleGoal g = new SimpleGoal(p[1], p[2], int.Parse(p[3]));

                // Load completed state
                if (bool.Parse(p[4]))
                {
                    g.RecordEvent();
                }

                _goals.Add(g);
            }
            else if (p[0] == "EternalGoal")
            {
                _goals.Add(new EternalGoal(p[1], p[2], int.Parse(p[3])));
            }
            else if (p[0] == "ChecklistGoal")
            {
                ChecklistGoal g = new ChecklistGoal(
                    p[1], p[2], int.Parse(p[3]),
                    int.Parse(p[4]), int.Parse(p[5])
                );

                // Load progress
                typeof(ChecklistGoal)
                    .GetField("_currentCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .SetValue(g, int.Parse(p[6]));

                _goals.Add(g);
            }
        }

        Console.WriteLine("Loaded!\n");
    }
}
