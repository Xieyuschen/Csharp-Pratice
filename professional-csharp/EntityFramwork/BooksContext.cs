using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Advance_Csharp
{
    //DbContext在Microsoft.EntityFrameworkCore命名空间里面
    public class BooksContext:DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }

        //字符串只要换行就自动切换为用+号连接
        //private const string ConnectionString = 
        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{
        //    base.OnConfiguring(dbContextOptionsBuilder);
        //    //通过UseSqlServer连接数据库
        //    dbContextOptionsBuilder.UseSqlServer(ConnectionString);
        //}
    }
}
