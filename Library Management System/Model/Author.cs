using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_System.Model
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }
           

        public string Email { get; set; }

        public string NameEmail { get; set; }

        public int DateCreated { get; set; }
    }

}