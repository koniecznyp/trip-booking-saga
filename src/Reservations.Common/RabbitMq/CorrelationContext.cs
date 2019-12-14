using System;

namespace Reservations.Common.RabbitMq
{
    public class CorrelationContext : ICorrelationContext
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Resource { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public CorrelationContext()
        {
        }
        
        private CorrelationContext(Guid id)
        {
            Id = id;
        }
        
        public CorrelationContext(Guid id, Guid userId, string resource)
        {
            Id = id;
            UserId = userId;
            Resource = resource;
            CreatedAt = DateTime.UtcNow;
        }

        public static ICorrelationContext FromId(Guid id)
            => new CorrelationContext(id);
    }
}