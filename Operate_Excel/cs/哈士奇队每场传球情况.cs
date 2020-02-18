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
                       var num = row.GetCell(0).ToString();
                       var org = row.GetCell(2).ToString();
                       var des = row.GetCell(3).ToString();

                       for(int r = 0; r < 2; r++)
                       {
                           var xori = Convert.ToInt32(row.GetCell(7+2*r));
                           var yori = Convert.ToInt32(row.GetCell(8+2*r));
                           if (org.Substring(0, 7) == "Huskies")
                           {
                               var key = num + "#" + org + "#" + des;
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
               string str;
               XSSFRow outsheetRow = (XSSFRow)outsheet.GetRow(k);
               XSSFCell[] outxSSFCells = new XSSFCell[4];
               for (int i = 0; i < 4; i++)
               {
                   outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);      
               }

               if (item.Key[2] == '#')
               {
                   outxSSFCells[0].SetCellValue(item.Key.Substring(0, 2));
                   str = item.Key.Substring(3);
               }
               else
               {
                   outxSSFCells[0].SetCellValue(item.Key.Substring(0, 1));
                   str = item.Key.Substring(2);

               }
               //Huskies_M1#Huskies_M12
               if (str[11]=='#')
               {
                   outxSSFCells[1].SetCellValue(str.Substring(0,11));
                   outxSSFCells[2].SetCellValue(str.Substring(12));
                   outxSSFCells[3].SetCellValue(item.Value);

               }

               if (str[10] == '#')
               {
                   outxSSFCells[1].SetCellValue(str.Substring(0, 10));
                   outxSSFCells[2].SetCellValue(str.Substring(11));
                   outxSSFCells[3].SetCellValue(item.Value);

               }

               ++k;
           }

           FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\excel\Haskies每场队员互传统计.xlsx", FileMode.Create);
           outwork.Write(outfile);

           outfile.Close();
           outwork.Close();
           infile.Close();
           inworkbook.Close();
       }
   }
}
