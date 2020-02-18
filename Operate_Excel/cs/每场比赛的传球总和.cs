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
                   var matchid = row.GetCell(0).ToString();
                   var team = row.GetCell(1).ToString();
                   var key = matchid + '#' + team;
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
               XSSFCell[] outxSSFCells = new XSSFCell[3];
               for (int i = 0; i < 3; i++)
               {
                   outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);
               }
               if (item.Key[1] == '#')
               {
                   outxSSFCells[0].SetCellValue(item.Key.Substring(0,1));
                   outxSSFCells[1].SetCellValue(item.Key.Substring(2));
                   outxSSFCells[2].SetCellValue(item.Value);

               }
               if (item.Key[2] == '#')
               {
                   outxSSFCells[0].SetCellValue(item.Key.Substring(0,2));
                   outxSSFCells[1].SetCellValue(item.Key.Substring(3));
                   outxSSFCells[2].SetCellValue(item.Value);

               }
               ++k;
           }

           FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\每场比赛传球总数.xlsx", FileMode.Create);
           outwork.Write(outfile);

           outfile.Close();
           outwork.Close();
           infile.Close();
           inworkbook.Close();
       }
   }
}