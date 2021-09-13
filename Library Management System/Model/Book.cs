using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_System.Model
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Available { get; set; }

        public int Quantity { get; set; }

        public int DateCreated { get; set; }
    }

}