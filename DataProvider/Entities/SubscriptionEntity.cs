using LinqToDB.Mapping;

namespace DataProvider.Entities
{
    [Table(Name = "Subscriptions")]
    public class SubscriptionEntity
    {
        [Column]
        public Guid Id { get; set; }
        
        [Column]
        public Guid UserId { get; set; }
        
        [Column]
        public Guid TariffId { get; set; }
        
        [Column]
        public bool IsActive { get; set; }
        
        [Column]
        public DateTime Deadline { get; set; }        
        
        [Column]
        public DateTime Created { get; set; }
        
        [Column]
        public DateTime? Modified { get; set; }
    }
}
