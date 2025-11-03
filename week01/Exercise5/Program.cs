using System;

class Program
{
    // 1. Displays welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // 2. Asks for user's name and returns it
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    // 3. Asks for user's favorite number and returns it as an integer
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());
        return number;
    }

    // 4. Squares the number and returns the result
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    // 5. Displays the final message
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}.");
    }

    // Main function to run the program
    static void Main(string[] args)
    {
        DisplayWelcome(); // Step 1

        string userName = PromptUserName(); // Step 2
        int userNumber = PromptUserNumber(); // Step 3

        int squaredNumber = SquareNumber(userNumber); // Step 4

        DisplayResult(userName, squaredNumber); // Step 5
    }
}
