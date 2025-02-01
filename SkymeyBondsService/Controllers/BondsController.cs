using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SkymeyLibs;
using SkymeyLibs.Models.Tables.Tokens;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Models.Tables.Bonds;

namespace SkymeyBondsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BondsController : Controller
    {
        private readonly IMongoRepository _db;
        private readonly ILogger<BondsController> _logger;
        private readonly IOptions<MainSettings> _options;

        public BondsController(ILogger<BondsController> logger, IOptions<MainSettings> options, IMongoRepository db)
        {
            _logger = logger;
            _options = options;
            _db = db;
        }

        [HttpGet]
        [Route("GetBonds")]
        public async Task<IEnumerable<stock_bonds>> GetBonds()
        {
            return await _db.GetBonds();
        }

        [HttpGet]
        [Route("GetBondsParams")]
        public async Task<IEnumerable<stock_bonds>> GetBonds(int skip = 0, int take = 1)
        {
            return await _db.GetBondsParams(skip, take);
        }

        [HttpPost]
        [Route("AddBond")]
        public async Task<IActionResult> AddBond(stock_bonds bond)
        {
            await _db.AddBond(bond);
            return Ok();
        }
    }
}
