using Microsoft.AspNetCore.Mvc;
using SpeakToSQL.API.Services;

namespace SpeakToSQL.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SpeakToSQLController : Controller
    {
        private readonly AOAIClient _client;
        public SpeakToSQLController(AOAIClient client)
        {
            _client = client;
        }

        [HttpGet("sqlsummary")]
        public IActionResult SummarizeSQL()
        {
            string summary = _client.SummarizeSQLScript();
            return Ok(summary);
        }

        [HttpGet("texttosql")]
        public IActionResult GetSQLFromText(string prompt, string dbstructure)
        {
            string sql = _client.GetSQLFromText(prompt, dbstructure);
            return Ok(sql);
        }
    }
}