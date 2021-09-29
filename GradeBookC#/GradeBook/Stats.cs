using System;
using System.Collections.Generic;

namespace GradeBook
{
    public class Stats
    {
        public void Add(double number)
        {
            Sum += number;
            Count += 1;

            Low = Math.Min(number, Low);
            High = Math.Max(number, High);
        }
        public Stats()
        {
            High = double.MinValue;
            Low = double.MaxValue;
        }

        public char Letter {
            get
            {
                switch (Average)
                {
                    case var  d when d >= 90.0:
                        return 'A';

                    case var d when d >= 80.0:
                        return 'B';

                    case var d when d >= 70.0:
                        return 'C';

                    case var d when d >= 60.0:
                        return 'D';

                    default:
                        return 'F';
            }
            }
        }

        public double Average{
            get{
                return Sum / Count;
            }
        }
        public double Low;
        public double High;
        public double Sum;
        public int Count;
    }
}


