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
        //计算了活动区域的平均值以及方差
       static void Main(string[] args)
       {
           Dictionary<string, int> xPosition = new Dictionary<string, int>();
           Dictionary<string, int> yPosition = new Dictionary<string, int>();
           Dictionary<string, int> nums = new Dictionary<string, int>();

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
                       for (int j = 0; j < 2; j++)
                       {
                           var xorg = Convert.ToInt32(row.GetCell(7 + j * 2).ToString());
                           var yorg = Convert.ToInt32(row.GetCell(8 + j * 2).ToString());

                           var ydes = Convert.ToInt32(row.GetCell(10).ToString());
                           if (xPosition.ContainsKey(org.Substring(8)))
                           {
                               xPosition[org.Substring(8)] += xorg*xorg;
                           }
                           else
                           {
                               xPosition.Add(org.Substring(8), 0);
                           }

                           if (yPosition.ContainsKey(org.Substring(8)))
                           {
                               yPosition[org.Substring(8)] += yorg*yorg;
                           }
                           else
                           {
                               yPosition.Add(org.Substring(8), 0);
                           }

                           if (nums.ContainsKey(org.Substring(8)))
                           {
                               nums[org.Substring(8)]++;
                           }
                           else
                           {
                               nums.Add(org.Substring(8), 1);
                           }

                       }
                   }
               }
           }

           Dictionary<string, (int x, int y)> avg = new Dictionary<string, (int, int)>();
           foreach (var item in nums)
           {
               avg.Add(item.Key, (xPosition[item.Key] / item.Value, yPosition[item.Key] / item.Value));
           }


           XSSFWorkbook outwork = new XSSFWorkbook();
           outwork.CreateSheet("1");
           XSSFSheet outsheet = (XSSFSheet)outwork.GetSheet("1");
           for (int i = 0; i < avg.Count; i++)
           {
               outsheet.CreateRow(i);
           }

           int k = 0;

           foreach (var item in avg)
           {
               XSSFCell[] outxSSFCells = new XSSFCell[3];
               XSSFRow outsheetRow = (XSSFRow)outsheet.GetRow(k);
               for (int i = 0; i < 3; i++)
               {
                   outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);
               }

               outxSSFCells[0].SetCellValue(item.Key);
               outxSSFCells[1].SetCellValue(item.Value.x);
               outxSSFCells[2].SetCellValue(item.Value.y);
               ++k;
           }

           FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\E(X^2).xlsx", FileMode.Create);
           outwork.Write(outfile);

           outfile.Close();
           outwork.Close();
           infile.Close();
           inworkbook.Close();
       }

   }
}
