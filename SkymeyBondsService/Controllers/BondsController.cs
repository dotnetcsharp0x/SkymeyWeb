using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SkymeyLibs;
using SkymeyLibs.Models.Tables.Tokens;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Models.Tables.Bonds;
using SkymeyLibs.Data;
using MongoDB.Driver;

namespace SkymeyBondsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BondsController : ControllerBase
    {
        private MongoClient _client;
        private MongoPostRepository _postRepository;
        private IOptions<MainSettings> _options;
        public BondsController(MongoClient client, IOptions<MainSettings> options)
        {
            _client = client;
            _options = options;
            _postRepository = new MongoPostRepository(_client, _options.Value.MongoDatabase);
        }

        [HttpGet]
        [Route("GetBonds")]
        public async Task<IEnumerable<stock_bonds>> GetBonds()
        {
            return await _postRepository.GetBonds();
        }

        [HttpGet]
        [Route("GetBondsParams")]
        public async Task<IEnumerable<stock_bonds>> GetBonds(int skip = 0, int take = 1)
        {
            return await _postRepository.GetBondsParams(skip, take);
        }

        [HttpPost]
        [Route("AddBond")]
        public async Task<IActionResult> AddBond(stock_bonds bond)
        {
            await _postRepository.AddBond(bond);
            return Ok();
        }
    }
}
