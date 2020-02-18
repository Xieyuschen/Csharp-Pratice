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
           FileStream infile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\passingevents.xlsx", FileMode.Open, FileAccess.Read);
           XSSFWorkbook inworkbook = new XSSFWorkbook(infile);
           ISheet insheet = inworkbook.GetSheet("passingevents");

           for (int m = 1; m <= 16; m++)
           {
               for (int i = 1; i < 60000; i++)
               {
                   IRow row = insheet.GetRow(i);
                   if (row != null)
                   {
                       var org = row.GetCell(2).ToString();
                       var des = row.GetCell(3).ToString();
                       var key = org + "_" + des;
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
               XSSFCell[] outxSSFCells = new XSSFCell[3];
               for (int i = 0; i < 3; i++)
               {
                   outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);
               }
               if (item.Key[11] == '_')
               {
                   outxSSFCells[0].SetCellValue(item.Key.Substring(0, 11));
                   outxSSFCells[1].SetCellValue(item.Key.Substring(12));
                   outxSSFCells[2].SetCellValue(item.Value);

               }
               if (item.Key[12] == '_')
               {
                   outxSSFCells[0].SetCellValue(item.Key.Substring(0, 12));
                   outxSSFCells[1].SetCellValue(item.Key.Substring(13));
                   outxSSFCells[2].SetCellValue(item.Value);

               }
               ++k;
           }

           FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\每个队队员互传统计.xlsx", FileMode.Create);
           outwork.Write(outfile);

           outfile.Close();
           outwork.Close();
           infile.Close();
           inworkbook.Close();
       }
   }
}
