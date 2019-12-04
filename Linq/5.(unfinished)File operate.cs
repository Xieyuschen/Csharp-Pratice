using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Linq
{
    class File_operate
    {
        static void Main()
        {
            string[] file1 = System.IO.File.ReadAllText(@"./1.txt");
            string file2 = System.IO.File.ReadAllText(@"./2.txt");
            var mergeQuery = file1.Union(file2);
            var groupQuery = from name in mergeQuery
                             let n = name.Split(',')
                             group name by n[0][0] into g
                             orderby g.key
                             select g;
        }
    }
}
