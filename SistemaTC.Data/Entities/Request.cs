using static SistemaTC.Core.Enums;

namespace SistemaTC.Data.Entities;
public class Request: Auditable
{
    public Guid RequestId { get; set; }
    public Guid RequestedByUserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public int Number { get; set; }
    public RequestType Type { get; set; }
    public string Note { get; set; } = string.Empty;
    public bool Approved { get; set; }
    public string InternalNote { get; set; } = string.Empty;

    public User RequestedBy { get; set; } = default!;
    public User AssignedTo { get; set; } = default!;
}
