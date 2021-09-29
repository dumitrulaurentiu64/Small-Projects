using System.Collections.Generic;
using System;
using System.IO;

namespace GradeBook
{

    //normally put in another file
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name{
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Stats GetStats();
        string Name {get;}
        event GradeAddedDelegate GradeAdded;
    }
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Stats GetStats();
    }
    
    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
        }

        public override void AddGrade(double grade)
        {
            if ( grade <= 100 && grade > 0)
            {
                grades.Add(grade);
                if(GradeAdded != null){
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                // Console.WriteLine("Invalid input!");
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override Stats GetStats()
        {
            var result = new Stats();

            for (int index = 0; index < grades.Count; index++)
            {
               result.Add(grades[index]);
            }

            return result;
        }
    
        public override event GradeAddedDelegate GradeAdded;
        private List<double> grades;
        public const string CATEGORY = "Science";

        /*public string Name{
            get{
                return name;
            }
            set{
                if(!String.IsNullOrEmpty(value))
                {
                    name = value;
                }
            }
        } */
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }   
            //writer.Dispose();
            // with using and curly braces we force the compiler to call the dispose/close method  COOL STUFF
        }

        public override Stats GetStats()
        {
            var result = new Stats();

            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
    }
}