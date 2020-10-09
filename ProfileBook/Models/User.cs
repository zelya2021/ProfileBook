using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public string Password { get; set; }
    }
}
