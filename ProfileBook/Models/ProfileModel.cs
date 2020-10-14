using SQLite;
using System;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class ProfileModel
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
