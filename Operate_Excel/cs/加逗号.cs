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

           Dictionary<(string,string), int> dic = new Dictionary<(string,string), int>();

           FileStream infile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\excel\Haskies每场队员互传统计.xlsx", FileMode.Open, FileAccess.Read);
           XSSFWorkbook inworkbook = new XSSFWorkbook(infile);
           ISheet insheet = inworkbook.GetSheet("1");


           for (int i = 0; i < 60000; i++)
           {
               IRow row = insheet.GetRow(i);
               if (row != null)
               {
                   var id=row.GetCell(0).ToString()+"#";
                   var org =id+"'"+row.GetCell(1).ToString().Substring(8)+"',";
                   var des =id+"'"+ row.GetCell(2).ToString().Substring(8)+"',";
                   var num =Convert.ToInt32(row.GetCell(3).ToString());
                   var key = (org,des) ;
                   dic.Add(key, num);
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
               XSSFCell[] outxSSFCells = new XSSFCell[4];
               for (int i = 0; i < 4; i++)
               {
                   outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);
               }
               if (item.Key.Item1[1] == '#')
               {
                   outxSSFCells[0].SetCellValue(item.Key.Item1.Substring(2));
                   outxSSFCells[1].SetCellValue(item.Key.Item2.Substring(2));
                   outxSSFCells[2].SetCellValue(item.Value);
                   outxSSFCells[3].SetCellValue(item.Key.Item1.Substring(0, 1));
               }
               else
               {
                   outxSSFCells[0].SetCellValue(item.Key.Item1.Substring(3));
                   outxSSFCells[1].SetCellValue(item.Key.Item2.Substring(3));
                   outxSSFCells[2].SetCellValue(item.Value);
                   outxSSFCells[3].SetCellValue(item.Key.Item1.Substring(0,2));

               }



               ++k;
           }

           FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\excel\1.xlsx", FileMode.Create);
           outwork.Write(outfile);

           outfile.Close();
           outwork.Close();
           infile.Close();
           inworkbook.Close();
       }
   }
}
