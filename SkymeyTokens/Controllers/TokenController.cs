using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SkymeyLibs.Data;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Models.Tables.Posts;
using SkymeyLibs;
using SkymeyLibs.Models.Tables.Tokens;
using MongoDB.Driver;

namespace SkymeyTokens.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private MongoClient _client;
        private MongoPostRepository _postRepository;
        private IOptions<MainSettings> _options;

        public TokenController(MongoClient client, IOptions<MainSettings> options)
        {
            _client = client;
            _options = options;
            _postRepository = new MongoPostRepository(_client, _options.Value.MongoDatabase);
        }

        [HttpGet]
        [Route("GetTokens")]
        public async Task<IEnumerable<API_TOKEN>> GetTokens()
        {
            return await _postRepository.GetTokens();
        }

        [HttpGet]
        [Route("GetTokenList")]
        public async Task<IEnumerable<TokenList>> GetTokenList()
        {
            return await _postRepository.GetTokenList();
        }
        [HttpGet]
        [Route("GetToken")]
        public async Task<API_TOKEN> GetToken(string slug)
        {
            return await _postRepository.GetToken(slug);
        }

        [HttpPost]
        [Route("AddToken")]
        public async Task<IActionResult> AddToken(API_TOKEN token)
        {
            await _postRepository.AddToken(token);
            return Ok();
        }
    }
}
