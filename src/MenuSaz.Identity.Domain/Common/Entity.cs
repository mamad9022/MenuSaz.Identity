using MediatR;

namespace Nitro.Fund.Backend.Domain.Common;
public abstract class Entity<T> where T : IEquatable<T>
{
    private List<INotification> _domainEvents;

    public T Id { get; }
    public byte[] Version { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    
    public List<INotification> DomainEvents => _domainEvents;

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        if (_domainEvents is null) return;
        _domainEvents.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public static bool operator ==(Entity<T> entity1, Entity<T> entity2) =>
        ReferenceEquals(entity1, entity2) || (((object)entity1 != null) && entity1.Equals(entity2));

    public static bool operator !=(Entity<T> entity1, Entity<T> entity2) => !(entity1 == entity2);

    public override bool Equals(object obj)
    {
        Entity<T> entity = obj as Entity<T>;

        return (entity != null) && (entity.GetType() == this.GetType()) && entity.Id.Equals(this.Id);
    }

    public override int GetHashCode() => this.Id?.GetHashCode() ?? 0;

    public override string ToString() => this.Id?.ToString() ?? string.Empty;
}
