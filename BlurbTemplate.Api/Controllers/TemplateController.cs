using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BlurbTemplate.Api.Models;
using BlurbTemplate.Interfaces;

namespace BlurbTemplate.Api.Controllers
{
    [ApiController]
    [Route("api/template")]
    public class TemplateController : ControllerBase
    {
        private readonly ILogger<TemplateController> logger;
        private readonly IWordReplacer wordReplacer;

        public TemplateController(ILogger<TemplateController> logger, IWordReplacer wordReplacer)
        {
            this.logger = logger;
            this.wordReplacer = wordReplacer;
        }

        [HttpPost("replace")]
        public IActionResult RequestReplace([FromBody]ReplaceRequestDto req)
        {
            if (req == null)
                return BadRequest();

            var result = string.Empty;
            try
            {
                result = wordReplacer.ReplaceSinglePlaceholder(req.Template, req.Placeholder, req.Replacement);
            }
            catch (Exception e)
            {
                logger.LogError("Error while performing a replace", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ReplaceResultDto() { Success = false, Message = "Error while performing a replace" });
            }

            return Ok(new ReplaceResultDto() { Success = true, Value = result });
        }
    }
}
