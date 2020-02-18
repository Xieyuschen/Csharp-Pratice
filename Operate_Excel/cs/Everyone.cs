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


           for (int i = 1; i < 60000; i++)
           {
               IRow row = insheet.GetRow(i);
               if (row != null)
               {
                   var org = row.GetCell(2).ToString();
                   var des = row.GetCell(3).ToString();
                   if (org.Substring(0, 7) == "Huskies" && des.Substring(0, 7) == "Huskies")
                   {

                       var key = org.Substring(8) + "_" + des.Substring(8);
                       if (dic.ContainsKey(org.Substring(8)))
                       {
                           ++dic[org.Substring(8)];
                       }
                       else
                       {
                           dic.Add(org.Substring(8), 1);
                       }
                       if (dic.ContainsKey(des.Substring(8)))
                       {
                           ++dic[des.Substring(8)];
                       }
                       else
                       {
                           dic.Add(des.Substring(8), 1);
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
               for (int i = 0; i < 2; i++)
               {
                   outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);
               }
                   outxSSFCells[0].SetCellValue(item.Key);
                   outxSSFCells[1].SetCellValue(item.Value);
               ++k;
           }

           FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\Everyone.xlsx", FileMode.Create);
           outwork.Write(outfile);

           outfile.Close();
           outwork.Close();
           infile.Close();
           inworkbook.Close();
       }
   }
}
