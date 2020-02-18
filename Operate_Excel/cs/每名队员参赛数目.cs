using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Collections.Generic;

namespace DataAnalisis
{
    class Everyone
    {
        static void Main(string[] args)
        {

            Dictionary<string, int> dic = new Dictionary<string, int>();
            Dictionary<string, int> xs = new Dictionary<string, int>();
            Dictionary<string, int> ys = new Dictionary<string, int>();
            Dictionary<string, int> nums = new Dictionary<string, int>();
            FileStream infile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\excel\Haskies每场队员平均分布.xlsx", FileMode.Open, FileAccess.Read);
            XSSFWorkbook inworkbook = new XSSFWorkbook(infile);
            ISheet insheet = inworkbook.GetSheet("1");

            
            for (int i = 1; i < 60000; i++)
            {
                IRow row = insheet.GetRow(i);
                if (row != null)
                {
                    string key;
                    var str = row.GetCell(0).ToString();
                    if (str[1] == 'H')
                    {
                        key = str.Substring(2);
                    }
                    else
                    {
                        key = str.Substring(3);
                    }

                    if (dic.ContainsKey(key))
                    {
                        ++dic[key];
                    }
                    else
                    {
                        dic.Add(key, 1);
                        }

                    
                }
            }
           



            //foreach(var item in dic)
            //{
            //    Console.WriteLine(item.Key + ": " + item.Value);
            //}
            XSSFWorkbook outwork = new XSSFWorkbook();
            outwork.CreateSheet("1");
            XSSFSheet outsheet = (XSSFSheet)outwork.GetSheet("1");
            for (int i = 0; i < dic.Count; i++)
            {
                outsheet.CreateRow(i);
            }
            int k = 0;
            foreach (var item in dic)
            {
                XSSFRow outsheetRow = (XSSFRow)outsheet.GetRow(k);
                XSSFCell[] outxSSFCells = new XSSFCell[2];
                for (int i = 0; i < 2; i++)
                {
                    outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);
                }

                    outxSSFCells[0].SetCellValue(item.Key);

                    outxSSFCells[1].SetCellValue(item.Value);


                ++k;
            }

            FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\excel\Haskies参赛场数.xlsx", FileMode.Create);
            outwork.Write(outfile);

            outfile.Close();
            outwork.Close();
            infile.Close();
            inworkbook.Close();
        }
    }
}
