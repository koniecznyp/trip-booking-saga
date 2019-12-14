namespace Reservations.Common.Events
{
    public interface IRejectedEvent : IEvent
    {
        string Message { get; }
    }
}