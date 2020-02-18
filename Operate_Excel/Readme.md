# 在C#种使用NPOI操作Excel数据集：
在操作数据的时候,我们可以选择NPOI这个包,使用C#的Nuget包管理器下载NPOI,即可使用.

# 创建一个新的Excel文档:
示例代码:
```csharp
 XSSFWorkbook outwork = new XSSFWorkbook();
outwork.CreateSheet("1");
XSSFSheet outsheet = (XSSFSheet)outwork.GetSheet("1");
outsheet.CreateRow(i);
XSSFCell[] outxSSFCells = new XSSFCell[3];
XSSFRow outsheetRow = (XSSFRow)outsheet.GetRow(k);
for (int i = 0; i < 3; i++)
{
    outxSSFCells[i] = (XSSFCell)outsheetRow.CreateCell(i);
    outxSSFCells[i].SetCellValue(i);
}

FileStream outfile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\DataAnalisis\E(X^2).xlsx", FileMode.Create);
outwork.Write(outfile);

outfile.Close();
outwork.Close();
```

# 从一个Excel中读取文件:
```csharp
Dictionary<string, int> dic = new Dictionary<string, int>();
//here input the file path
FileStream infile = new FileStream(@"E:\C_Cpp_and_Csharp\DataAnalisis\passingevents.xlsx", FileMode.Open, FileAccess.Read);
XSSFWorkbook inworkbook = new XSSFWorkbook(infile);
ISheet insheet = inworkbook.GetSheet("passingevents");

//获取第一行第一列的元素值
IRow row = insheet.GetRow(1);
var item=row.GetCell(0);
```

# 小结
NPOI的基本功能其实就是这些,当然肯定还有一大堆和excel联动进行计算的操作.本次就先写到这里,之后有机会(又用到)的时候会再加点东西hhh.
