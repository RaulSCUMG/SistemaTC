namespace SistemaTC.DTO.Request;
public class UpdateRequest
{
    public Guid AssignedToUserId { get; set; }
    public bool Approved { get; set; }
    public string InternalNote { get; set; } = default!;
    public string User { get; set; } = default!;
}
