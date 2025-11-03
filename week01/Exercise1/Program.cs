using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        Console.Write("What is your last name? ");
        string firstName = Console.ReadLine();
        string LastName = Console.ReadLine();
        Console.WriteLine($"Your name is {firstName}, {LastName}");
        
    }
}