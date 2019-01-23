using System;
using AirportDb;

namespace ExamPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //Company c = new Company("asdf", 12, 12.00);

            AirportDatabase aiportDb = AirportDatabase.Test();
            foreach (var item in aiportDb.Query1())
            {
                System.Console.WriteLine(item.CompanyName);

            }



        }
    }
}
