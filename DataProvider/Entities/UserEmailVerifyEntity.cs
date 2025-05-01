using LinqToDB.Mapping;

namespace DataProvider.Entities
{
    [Table(Name = "UserEmailVerify")]
    public class UserEmailVerifyEntity
    {
        [Column]
        public Guid Id { get; set; }        
        
        [Column]
        public Guid UserId { get; set; }
        
        [Column]
        public string VerifyCode { get; set; }
        
        [Column]
        public byte Attempt { get; set; }
        
        [Column]
        public DateTime Created { get; set; }
        
        [Column]
        public DateTime? Modified { get; set; }
    }
}
