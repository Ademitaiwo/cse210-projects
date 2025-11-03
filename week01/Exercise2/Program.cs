using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your score: ");
        int score = int.Parse(Console.ReadLine());

       
         int A_GRADE = 90;
         int B_GRADE = 80;
         int C_GRADE = 70;
         int D_GRADE = 60;
        string grade
;
        if (score >= A_GRADE)
        {
            Console.WriteLine("Score is greater than or equal to A grade threshold.");
            Console.WriteLine("Congratulations! you got an A grade");
            grade = "A";
        }

        else if (score >= B_GRADE)
        {
            Console.WriteLine("Score is greater than or equal to B grade threshold.");
            Console.WriteLine("Well done! you got a B grade, Therefore you passed the course");
            grade = "B";
        }

        else if (score >= C_GRADE)
        {
            Console.WriteLine("Score is greater than or equal to C grade threshold.");
            Console.WriteLine("Good effort! you got a C grade, Therefore you passed the course");
            grade = "C";
        }
        else if (score >= D_GRADE)
        {
            Console.WriteLine("Score is greater than or equal to D grade threshold.");
            Console.WriteLine("You got a D grade, Therefore you failed the course");
            Console.WriteLine("Better luck next time!");
            grade = "D";
        }
        else
        {
            Console.WriteLine("Score is less than D grade threshold.");
            Console.WriteLine("Unfortunately, you got an F grade, Therefore you failed the course");
            Console.WriteLine("Better luck next time!");
            grade = "F";
        }

        Console.WriteLine($"Score: {score} -> Grade: {grade}");
    }
}