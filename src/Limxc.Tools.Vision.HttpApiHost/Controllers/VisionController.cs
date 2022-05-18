using Limxc.Tools.Vision.Models;
using Limxc.Tools.Vision.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Limxc.Tools.Vision.HttpApiHost.Controllers;

[ApiController]
[Route("[controller]")]
public class VisionController : ControllerBase
{
    private readonly ILogger<VisionController> _logger;

    public VisionController(ILogger<VisionController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public Task<MetaImage> TurnGray(MetaImage meta)
    {
        var base64 = CvHelper.ToGray(meta.Base64);
        return Task.FromResult(new MetaImage { Base64 = base64 });
    }

    [HttpPost("base64")]
    public Task<string> TurnGray(string meta)
    {
        var base64 = CvHelper.ToGray(meta);
        return Task.FromResult(base64);
    }
}