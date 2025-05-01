using LinqToDB.Mapping;

namespace DataProvider.Entities
{
    [Table(Name = "Tariffs")]
    public class TariffEntity
    {
        [Column]
        public Guid Id { get; set; }
        
        [Column]
        public int Price { get; set; }
        
        [Column]
        public int RequestsPerMinute { get; set; }
        
        [Column]
        public int SimpleRequestsCount { get; set; }
        
        [Column]
        public int BigRequestsCount { get; set; }        
        
        [Column]
        public bool IsActive { get; set; }
        
        [Column]
        public TimeSpan Duration { get; set; }        
        
        [Column]
        public DateTime Created { get; set; }
        
        [Column]
        public DateTime? Modified { get; set; }
    }
}
