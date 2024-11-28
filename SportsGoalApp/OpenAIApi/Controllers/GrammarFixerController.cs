using ChatGPT.Net;
using Microsoft.AspNetCore.Mvc;

namespace OpenAIApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrammarFixerController : ControllerBase
    {
        private readonly ILogger<GrammarFixerController> _logger;
        private IConfiguration _configuration;

        public GrammarFixerController(ILogger<GrammarFixerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("version")]
        public string Version()
        {
            return _configuration["VERSION"];
        }

        [HttpPost("fixGrammar")]
        public async Task<IActionResult> FixGrammar([FromBody] SentencePayloadRequest request)
        {
            var openAiKey = _configuration["OPEN_API_KEY"];

            if(openAiKey == null)
            {
                return NotFound("OpenAiKey is not set");
            }

            var openai = new ChatGpt(openAiKey);

            var fixedSentence = await openai.Ask($"Fix sentence: { request.RawSentence} ");

            if(fixedSentence == null)
            {
                return NotFound("Error fixing sentence");
            }

            return Ok(new SentencePayloadResponse() { fixedSentence = fixedSentence});
        }
    }
}
