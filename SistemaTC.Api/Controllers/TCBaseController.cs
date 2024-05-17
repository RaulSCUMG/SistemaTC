using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.DTO.User;

namespace SistemaTC.Api.Controllers;

[Authorize]
public class TCBaseController : ControllerBase
{
    protected LoggedInUser LoggedInUser => (LoggedInUser)HttpContext.Items[nameof(LoggedInUser)]!;
}
