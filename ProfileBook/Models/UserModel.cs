using SQLite;

namespace ProfileBook.Models
{
    [Table("Users")]
    public class UserModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}
