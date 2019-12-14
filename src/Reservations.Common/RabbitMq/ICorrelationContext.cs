using System;

namespace Reservations.Common.RabbitMq
{
    public interface ICorrelationContext
    {
        Guid Id { get; }
        Guid UserId { get; }
        string Resource { get; }
        DateTime CreatedAt { get; }
    }
}