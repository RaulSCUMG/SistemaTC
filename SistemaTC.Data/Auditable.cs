namespace SistemaTC.Data;
public class Auditable
{
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
}