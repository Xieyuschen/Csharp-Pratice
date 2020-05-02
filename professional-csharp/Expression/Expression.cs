using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Advance_Csharp
{
    [Description("LearnExpr")]
    public class Person
    {
        [Description("PersonName")]
        
        public string Name { get; set; }

        [Description("Idetity Id")]
        public string Id { get; set; }
    }
    class Expression
    {
        public Tuple<string, string> GetPropertyValue<T>(T instance, Expression<Func<T, string>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            string propertyName = memberExpression.Member.Name;
            var t = memberExpression.Member.GetCustomAttributes(true)[0];
            string attributeName = (t as DescriptionAttribute).Description;

            var property = typeof(T).GetProperties().Where(l => l.Name == propertyName).First();

            return new Tuple<string, string>(attributeName, property.GetValue(instance).ToString());

        }
        public void TestMethod()
        {
            var p = new Person
            {
                Id = "123",
                Name = "Schmidt"
            };
            var result = GetPropertyValue(p, t => t.Name);
            Console.WriteLine($"显示名称：{result.Item1}-值:{result.Item2}");
        }

        public static void Main()
        {
            Expression expression = new Expression();
            expression.TestMethod();
        }
    }
}
