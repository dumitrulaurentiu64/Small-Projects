using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            DiskBook book = new DiskBook("Jeffrey");

            book.GradeAdded += OnGradeAdded;
            book.GradeAdded += OnGradeAdded;
            book.GradeAdded -= OnGradeAdded;

            EnterGrades(book); // extracted method

            Stats result = book.GetStats();

            System.Console.WriteLine($"Average grade is {result.Average}.");
            System.Console.WriteLine($"Highest grade is {result.High}.");
            System.Console.WriteLine($"Lowest grade is {result.Low}.");

            Console.WriteLine($"Media generala a elevului este: {result.Letter}");



            //Console.WriteLine("Hello " + args[0] + "!");
            //Console.WriteLine($"Hello {args[0]}!");
            //double[] grades = new double[3] {12.7, 10.3, 6.11};

            //var grades = new[]{12.7, 10.3, 6.11}; same thing as above  
            //foreach(double number in grades){}      
            // List<double> grades = new List<double>() { 12.7432, 10.331, 6.11312 };
            // Console.WriteLine($"First Grade = {grades[0]:N2}) -- N2 specifies how many decimals are showed

        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit.");

                var input = Console.ReadLine();

                if (input == "q")
                    break;

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
        }

        static void OnGradeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("Grade added");
        }
    }

}
