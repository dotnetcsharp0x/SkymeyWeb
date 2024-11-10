using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SkymeyLibs.Data;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Models.Tables.Posts;
using SkymeyLibs;
using SkymeyLibs.Models.Tables.Tokens;

namespace SkymeyTokens.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly IMongoRepository _db;
        private readonly ILogger<TokenController> _logger;
        private readonly IOptions<MainSettings> _options;

        public TokenController(ILogger<TokenController> logger, IOptions<MainSettings> options,IMongoRepository db)
        {
            _logger = logger;
            _options = options;
            _db = db;
        }

        [HttpGet]
        [Route("GetTokens")]
        public async Task<IEnumerable<API_TOKEN>> GetTokens()
        {
            return await _db.GetTokens();
        }

        [HttpGet]
        [Route("GetTokenList")]
        public async Task<IEnumerable<TokenList>> GetTokenList()
        {
            return await _db.GetTokenList();
        }

        [HttpPost]
        [Route("AddToken")]
        public async Task<IActionResult> AddToken(API_TOKEN token)
        {
            await _db.AddToken(token);
            return Ok();
        }
    }
}
