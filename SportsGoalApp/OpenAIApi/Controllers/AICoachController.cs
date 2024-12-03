using ChatGPT.Net;
using Microsoft.AspNetCore.Mvc;

namespace OpenAIApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AICoachController : ControllerBase
    {
        private readonly ILogger<AICoachController> _logger;
        private IConfiguration _configuration;

        public AICoachController(ILogger<AICoachController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("version")]
        public string Version()
        {
            return _configuration["VERSION"];
        }

        [HttpPost("coachingAdvice")]
        public async Task<IActionResult> GetCoachingAdvice([FromBody] SentencePayloadRequest request)
        {
            var openAiKey = _configuration["open-api-key"];

            if(openAiKey == null)
            {
                return NotFound("OpenAiKey is not set");
            }

            var openai = new ChatGpt(openAiKey);

            var coahingAdvice = await openai.Ask($"Give coaching advice: { request.RawInput} ");

            if(coahingAdvice == null)
            {
                return NotFound("Error giving coaching advice");
            }

            return Ok(new SentencePayloadResponse() { CoachingAdvice = coahingAdvice});
        }
    }
}
