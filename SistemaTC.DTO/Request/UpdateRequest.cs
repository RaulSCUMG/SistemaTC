﻿using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.Request;
public class UpdateRequest
{
    [Required]
    public Guid RequestId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    [Required]
    public bool Approved { get; set; }
    [Required]
    public string InternalNote { get; set; } = default!;
}
