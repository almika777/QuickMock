using LinqToDB.Mapping;

namespace DataProvider.Entities
{
    [Table(Name = "Users")]
    public class UserEntity
    {
        [Column]
        public Guid Id { get; set; }
        
        [Column]
        public string Email { get; set; }
        
        [Column]
        public string Username { get; set; }
        
        [Column]
        public string Password { get; set; }
        
        [Column]
        public string RefreshToken { get; set; }
        
        [Column]
        public bool IsActive { get; set; }
        
        [Column]
        public string Comment { get; set; }
        
        [Column]
        public DateTime Created { get; set; }
        
        [Column]
        public DateTime Modified { get; set; }
    }
}
