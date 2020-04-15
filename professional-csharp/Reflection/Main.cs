using System;

namespace Advance_Csharp
{
    class Program
    {
        static void Main()
        {
            var helper = new CsvSolver();
            var employeeList = helper.ParseFile("1.txt");
            foreach (var employee in employeeList)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} lives in {employee.City}, {employee.State}.");
            }
            Console.ReadLine();
        }
    }
}