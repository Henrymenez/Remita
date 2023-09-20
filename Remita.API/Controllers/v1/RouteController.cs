using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Remita.Controllers.v1.Shared;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;

namespace Remita.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/routes")]
[ApiVersion("1.0")]
[ApiController]
public class RouteController : BaseController
{
    private readonly IEnumerable<EndpointDataSource> _endpointSources;

    public RouteController(IEnumerable<EndpointDataSource> endpointSources)
    {
        _endpointSources = endpointSources;
    }
    [AllowAnonymous]

    [HttpGet("get-all-routes")]
    [SwaggerOperation(Summary = "Gets all routes ")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "Routes Retrieved")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Unauthorized User", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
    public IActionResult GetRoutes()
    {
        var endpoints = _endpointSources.SelectMany(es => es.Endpoints)
        .OfType<RouteEndpoint>();
        IEnumerable<string?> output = endpoints.Select(e =>
        {
            var controller = e.Metadata
                .OfType<ControllerActionDescriptor>()
                .FirstOrDefault();
            var httpMethod = controller?.MethodInfo
                .GetCustomAttributes<HttpMethodAttribute>()
                .FirstOrDefault()
                ?.Name;

            return httpMethod;
        });
        return Ok(output);
    }
}
