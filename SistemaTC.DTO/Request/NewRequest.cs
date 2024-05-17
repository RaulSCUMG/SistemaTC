using System.ComponentModel.DataAnnotations;
using static SistemaTC.Core.Enums;

namespace SistemaTC.DTO.Request;
public class NewRequest
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public RequestType Type { get; set; }
    [Required]
    public string Note { get; set; } = default!;
}
