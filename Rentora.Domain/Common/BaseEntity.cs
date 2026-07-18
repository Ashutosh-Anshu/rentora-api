using Rentora.Domain.Common.Interfaces;

namespace Rentora.Domain.Common;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}