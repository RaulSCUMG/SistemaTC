using static SistemaTC.Core.Enums;

namespace SistemaTC.DTO.Request;
public class Request
{
    public Guid RequestId { get; set; }
    public Guid RequestedByUserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public int Number { get; set; }
    public RequestType Type { get; set; }
    public string Note { get; set; } = default!;
    public bool Approved { get; set; }
    public string InternalNote { get; set; } = default!;
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
}
