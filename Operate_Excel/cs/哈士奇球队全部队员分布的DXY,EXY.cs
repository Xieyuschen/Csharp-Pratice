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

           Dictionary<string, int> xs = new Dictionary<string, int>();
           Dictionary<string, int> ys = new Dictionary<string, int>();
           Dictionary<string, int> nums = new Dictionary<string, int>();
           FileStream infile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\passingevents.xlsx", FileMode.Open, FileAccess.Read);
           XSSFWorkbook inworkbook = new XSSFWorkbook(infile);
           ISheet insheet = inworkbook.GetSheet("passingevents");


           for (int i = 1; i < 60000; i++)
           {
               IRow row = insheet.GetRow(i);
               if (row != null)
               {
                   var num = row.GetCell(0).ToString();
                   var org = row.GetCell(2).ToString();
                   var des = row.GetCell(3).ToString();

                   for (int r = 0; r < 2; r++)
                   {
                       var xori = Convert.ToInt32(row.GetCell(7 + 2 * r).ToString());
                       var yori = Convert.ToInt32(row.GetCell(8 + 2 * r).ToString());
                       if (org.Substring(0, 7) == "Huskies")
                       {

                           var key = num;
                           if (xs.ContainsKey(key))
                           {
                               xs[key] += xori;
                           }
                           else
                           {
                               xs.Add(key, xori);
                           }
                           if (ys.ContainsKey(key))
                           {
                               ys[key] += yori;
                           }
                           else
                           {
                               ys.Add(key, yori);
                           }
                           if (nums.ContainsKey(key))
                           {
                               ++nums[key];
                           }
                           else
                           {
                               nums.Add(key, 1);
                           }
                       }
                   }



               }
           }
            

           XSSFWorkbook outwork = new XSSFWorkbook();
           outwork.CreateSheet("1");
           XSSFSheet outsheet = (XSSFSheet)outwork.GetSheet("1");
           for (int i = 0; i < xs.Count; i++)
           {
               outsheet.CreateRow(i);
           }
           int k = 0;
           foreach (var item in xs)
           {
               XSSFRow outsheetRow = (XSSFRow)outsheet.GetRow(k);
               XSSFCell[] outxSSFCells = new XSSFCell[4];
               for (int i = 0; i < 4; i++)
               {
                   outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);
               }

               outxSSFCells[0].SetCellValue(item.Key);
               outxSSFCells[1].SetCellValue(item.Value/nums[item.Key]);
               outxSSFCells[2].SetCellValue(ys[item.Key]/nums[item.Key]);


               ++k;
           }

           FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\excel\Haskies全部队员EX分布.xlsx", FileMode.Create);
           outwork.Write(outfile);

           outfile.Close();
           outwork.Close();
           infile.Close();
           inworkbook.Close();
       }
   }
}
