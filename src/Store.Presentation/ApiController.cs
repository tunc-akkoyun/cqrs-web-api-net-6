using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Store.Presentation.Models;

namespace Store.Presentation;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
// [Route("{company_id}/[controller]")]
[ApiExplorerSettings(GroupName = "Base")]
[ProducesResponseType(typeof(ErrorResponse), 400)]
[ProducesResponseType(typeof(ErrorResponse), 401)]
[ProducesResponseType(typeof(ErrorResponse), 403)]
[ProducesResponseType(typeof(ErrorResponse), 404)]
[ProducesResponseType(typeof(ErrorResponse), 500)]
public abstract class ApiController : ControllerBase
{
    private ISender _sender;

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
}