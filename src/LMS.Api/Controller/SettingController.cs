using LMS.Application.Common;
using LMS.Application.Setting;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;

[Route(ApiRoutes.Setting.Get)]
[ApiController]
public class SettingController(IMediator mediator) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> UpdateSetting([FromQuery] UpdateSettingCommand command)
    {
        var result = await mediator.Send(command);
        if(result is not null)
        return NoContent();

        return BadRequest();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSetting()
    {
        var query = new GetSetting();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
