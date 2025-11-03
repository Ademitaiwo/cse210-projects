using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        int listNumber;

        do
        {
            Console.Write("Enter a number here and type 0 to stop the game:  ");
            listNumber = int.Parse(Console.ReadLine());

            if (listNumber != 0)
            {
                numbers.Add(listNumber);
            }

        } while (listNumber != 0); // condition checked after the loop runs once

        // Calculate sum
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        Console.WriteLine($"The sum is: {sum}");

        // Calculate average
        float average = (float)sum / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        // Find max
        int max = numbers[0];
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }
        Console.WriteLine($"The max is: {max}");
    }
}
