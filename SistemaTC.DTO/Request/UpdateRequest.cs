using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.Request;
public class UpdateRequest
{
    public Guid? AssignedToUserId { get; set; }
    [Required]
    public bool Approved { get; set; }
    [Required]
    public string InternalNote { get; set; } = default!;
    [Required]
    public string User { get; set; } = default!;
}
