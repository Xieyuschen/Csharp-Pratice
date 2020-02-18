using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Collections.Generic;

namespace DataAnalisis
{
   class Program
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
               IRow row2 = insheet.GetRow(i + 1);
               if (row != null && row2 != null)
               {
                   var rowstr = row.GetCell(3).ToString();
                   var row2str = row2.GetCell(3).ToString();
                   if (rowstr.Substring(0, 7) == "Huskies" && row2str.Substring(0, 8) == "Opponent")
                   {
                       var team = rowstr;
                       if (dic.ContainsKey(team))
                       {
                           dic[team]++;
                       }
                       else
                       {
                           dic.Add(team, 1);
                       }
                   }

               }
           }


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

           FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\球员传球失误数.xlsx", FileMode.Create);
           outwork.Write(outfile);

           outfile.Close();
           outwork.Close();
           infile.Close();
           inworkbook.Close();
       }

   }
}
