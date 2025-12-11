
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Create activities
        var run = new RunningActivity(
            new DateTime(2022, 11, 3), 30, 3.0);

        var cycle = new CyclingActivity(
            new DateTime(2022, 11, 3), 45, 10.0);

        var swim = new SwimmingActivity(
            new DateTime(2022, 11, 3), 25, 40); // 40 laps

        // Place in a list
        List<Activity> activities = new List<Activity> { run, cycle, swim };

        // Display summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
