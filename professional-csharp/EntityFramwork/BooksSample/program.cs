using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Advance_Csharp
{
    //有main方法主函数的时候，所在的类名称不能说Main
    public class program
    {
        public static void Main()
        {
            var optionBuilder = new DbContextOptionsBuilder<BooksContext>();
            optionBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Books;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            using(var context=new BooksContext(optionBuilder.Options))
            {
                var book = new Book
                {
                    Title = "hello",
                    Publisher = "world"
                };
                context.Add(book); 
                int result= context.SaveChanges();
                Console.WriteLine($"{result } record added");
            }
        }
    }
}
