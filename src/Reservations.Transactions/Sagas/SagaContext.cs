using System;
using System.Collections.Generic;
using Chronicle;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Sagas
{
    public class SagaContext : ISagaContext
    {
        public Guid CorrelationId { get; }

        public string Originator { get; }

        public IReadOnlyCollection<ISagaContextMetadata> Metadata { get; }

        private SagaContext(Guid correlationId, string originator)
        {
            CorrelationId = correlationId;
            Originator = originator;
        }
        
        public static ISagaContext FromCorrelationContext(ICorrelationContext context)
            => new SagaContext(context.Id, context.Resource);

        public ISagaContextMetadata GetMetadata(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetMetadata(string key, out ISagaContextMetadata metadata)
        {
            throw new NotImplementedException();
        }
    }
}