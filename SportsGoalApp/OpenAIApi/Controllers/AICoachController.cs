using ChatGPT.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenAIApi.Options;
using OpenAIApi.Wrappers;

namespace OpenAIApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AICoachController : ControllerBase
    {
        private readonly ILogger<AICoachController> _logger;
        private readonly IChatGpt _openai;
        private readonly ChatGptSettings _options;
        private IConfiguration _configuration;

        public AICoachController(ILogger<AICoachController> logger, IConfiguration configuration, IChatGpt openai, IOptions<ChatGptSettings> options)
        {
            _logger = logger;
            _configuration = configuration;
            _openai = openai;
            _options = options.Value;
        }

        [HttpGet("version")]
        public string Version()
        {
            return _configuration["VERSION"];
        }

        [HttpPost("coachingAdvice")]
        public async Task<IActionResult> GetCoachingAdvice([FromBody] SentencePayloadRequest request)
        {

            if(string.IsNullOrEmpty(_options.ApiKey))
            {
                return NotFound("OpenAiKey is not set");
            }


            var coachingAdvice = await _openai.Ask($"Give coaching advice: { request.RawInput} ");

            if(coachingAdvice == null)
            {
                return NotFound("Error giving coaching advice");
            }

            return Ok(new SentencePayloadResponse() { CoachingAdvice = coachingAdvice});
        }
    }
}
