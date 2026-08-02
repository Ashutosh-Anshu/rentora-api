using Rentora.Domain.Common.Interfaces;

namespace Rentora.Domain.Common;

public abstract class BaseAuditableEntity
    : BaseEntity, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }

    public Guid? CreatedById { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedById { get; set; }

    public bool IsDeleted { get; set; }
}