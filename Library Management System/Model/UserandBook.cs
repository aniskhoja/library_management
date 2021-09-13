using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_System.Model
{
    public class UserandBook
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public int BorrowDate { get; set; }

        public int ReturnDate { get; set; }
    }
}