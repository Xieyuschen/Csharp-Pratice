using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace Advance_Csharp
{
    [Table("Books")]
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
    }
}
