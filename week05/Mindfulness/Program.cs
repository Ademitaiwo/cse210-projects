// Program.cs
// Mindfulness Program for CSE210 assignment
// Creativity note (exceeds core requirements): This program appends a simple activity log to "mindfulness_log.txt"
// located in the running directory. The log includes activity name, duration, and timestamp.
//
// Author: (Your Name)
// Date: (Add date)
// -------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MindfulnessProgram
{
    // Base class that contains shared attributes and behavior
    abstract class Activity
    {
        // Encapsulated fields
        private string _name;
        private string _description;
        private int _durationSeconds;

        // Protected so derived classes can access safely
        protected Random _random = new Random();

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
            _durationSeconds = 0;
        }

        // Public property to read duration
        public int DurationSeconds => _durationSeconds;

        // Start message common to all activities
        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"--- {_name} ---");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();
            SetDurationFromUser();
            Console.WriteLine();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
        }

        // End message common to all activities
        public void End()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            Console.WriteLine($"You have completed the activity: {_name}");
            Console.WriteLine($"Duration: {_durationSeconds} seconds.");
            ShowSpinner(3);

            // Save to a simple log (creative extra)
            SaveActivityLog(_name, _durationSeconds);
        }

        // Ask the user for duration and set it
        private void SetDurationFromUser()
        {
            int seconds = 0;
            while (true)
            {
                Console.Write("Enter the duration in seconds (e.g., 30): ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out seconds) && seconds > 0)
                {
                    _durationSeconds = seconds;
                    break;
                }
                Console.WriteLine("Please enter a positive whole number for seconds.");
            }
        }

        // Spinner animation helper
        protected void ShowSpinner(int seconds)
        {
            char[] spinner = new char[] { '|', '/', '-', '\\' };
            Stopwatch sw = Stopwatch.StartNew();
            int idx = 0;
            while (sw.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(spinner[idx % spinner.Length]);
                Thread.Sleep(250);
                Console.Write("\b");
                idx++;
            }
            Console.WriteLine();
        }

        // Countdown helper (shows numbers descending)
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            Console.WriteLine();
        }

        // Helper: save a simple log entry to file
        private void SaveActivityLog(string activityName, int duration)
        {
            try
            {
                string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {activityName} | {duration}s";
                File.AppendAllLines("mindfulness_log.txt", new[] { logLine });
            }
            catch
            {
                // If logging fails, don't crash the program — it's just a small extra.
            }
        }

        // Abstract method each activity must implement
        public abstract void Run();
    }

    // Breathing Activity
    class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing."
        )
        { }

        public override void Run()
        {
            Start();

            // We'll alternate "Breathe in..." and "Breathe out..." until the duration is reached
            Stopwatch sw = Stopwatch.StartNew();
            bool breatheIn = true;

            // Define a typical breathe segment length (seconds). We'll use 4 seconds per in/out as a friendly pace.
            int segmentSeconds = 4;

            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
                if (breatheIn)
                {
                    Console.WriteLine();
                    Console.Write("Breathe in... ");
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Breathe out... ");
                }

                // Show a countdown for this segment but don't overshoot the total duration
                int remaining = DurationSeconds - (int)sw.Elapsed.TotalSeconds;
                int countdown = Math.Min(segmentSeconds, Math.Max(1, remaining));

                ShowCountdown(countdown);

                breatheIn = !breatheIn;
            }

            End();
        }
    }

    // Reflection Activity
    class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity() : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life."
        )
        { }

        public override void Run()
        {
            Start();

            // Show a random prompt
            string prompt = _prompts[_random.Next(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine("Prompt:");
            Console.WriteLine($"  --- {prompt} ---");
            Console.WriteLine();

            // Allow the user a few seconds to think before starting the questions
            Console.WriteLine("When you're ready, we will reflect on some questions.");
            Console.Write("Preparing to ask questions ");
            ShowSpinner(3);
            Console.WriteLine();

            Stopwatch sw = Stopwatch.StartNew();

            // We'll pick questions at random until time runs out
            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
                string question = _questions[_random.Next(_questions.Count)];
                Console.WriteLine();
                Console.WriteLine($"> {question}");
                // Pause for a few seconds to let them reflect, showing spinner
                int pauseSeconds = 5;
                // But don't overshoot total duration
                double remaining = DurationSeconds - sw.Elapsed.TotalSeconds;
                int actualPause = (int)Math.Min(pauseSeconds, Math.Max(1, Math.Floor(remaining)));
                ShowSpinner(actualPause);
            }

            End();
        }
    }

    // Listing (Enumeration) Activity
    class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
        )
        { }

        // Helper: read one line with timeout (returns null if timed out)
        private string? ReadLineWithTimeout(int timeoutMilliseconds)
        {
            Task<string?> readTask = Task.Run(() => Console.ReadLine());
            bool completed = readTask.Wait(timeoutMilliseconds);
            if (completed)
            {
                return readTask.Result;
            }
            else
            {
                return null;
            }
        }

        public override void Run()
        {
            Start();

            // Show a random prompt
            string prompt = _prompts[_random.Next(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine("Prompt:");
            Console.WriteLine($"  --- {prompt} ---");
            Console.WriteLine();
            Console.WriteLine("You will have a few seconds to think, then list as many items as you can.");
            Console.Write("Get ready ");
            ShowSpinner(3);
            Console.WriteLine();

            Console.WriteLine($"Start listing items now! (Press Enter after each item). You have {DurationSeconds} seconds.");
            List<string> items = new List<string>();

            DateTime endTime = DateTime.Now.AddSeconds(DurationSeconds);

            // We'll read lines repeatedly but ensure we don't block beyond the allowed time.
            while (DateTime.Now < endTime)
            {
                // Calculate milliseconds left and allow that as timeout for the read
                int msRemaining = (int)(endTime - DateTime.Now).TotalMilliseconds;
                if (msRemaining <= 0)
                    break;

                // If user types nothing and waits, we still don't want to block past the end time.
                string? line = ReadLineWithTimeout(msRemaining);
                if (line == null)
                {
                    // timed out
                    break;
                }
                line = line.Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    items.Add(line);
                    Console.WriteLine($"  (Recorded) {line}");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {items.Count} item(s).");

            // Optionally display the items
            if (items.Count > 0)
            {
                Console.WriteLine("Here are the things you listed:");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {items[i]}");
                }
            }

            End();
        }
    }

    // Main program with menu
    class Program
    {
        static void Main(string[] args)
        {
            bool done = false;
            while (!done)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Mindfulness Program!");
                Console.WriteLine("Please choose an activity:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            Activity a = new BreathingActivity();
                            a.Run();
                            PauseBeforeContinue();
                            break;
                        }
                    case "2":
                        {
                            Activity a = new ReflectionActivity();
                            a.Run();
                            PauseBeforeContinue();
                            break;
                        }
                    case "3":
                        {
                            Activity a = new ListingActivity();
                            a.Run();
                            PauseBeforeContinue();
                            break;
                        }
                    case "4":
                        done = true;
                        Console.WriteLine("Goodbye! Remember to take a moment to breathe today.");
                        Thread.Sleep(1200);
                        break;
                    default:
                        Console.WriteLine("Please enter a number from 1 to 4.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        private static void PauseBeforeContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }
    }
}
