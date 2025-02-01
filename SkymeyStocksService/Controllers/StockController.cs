using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SkymeyLibs;
using SkymeyLibs.Models.Tables.Tokens;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Models.Tables.Stocks;

namespace SkymeyStocksService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly IMongoRepository _db;
        private readonly ILogger<StocksController> _logger;
        private readonly IOptions<MainSettings> _options;

        public StocksController(ILogger<StocksController> logger, IOptions<MainSettings> options, IMongoRepository db)
        {
            _logger = logger;
            _options = options;
            _db = db;
        }

        [HttpGet]
        [Route("GetStocks")]
        public async Task<IEnumerable<stock_stocks>> GetStocks()
        {
            return await _db.GetStocks();
        }

        [HttpGet]
        [Route("GetStocksParams")]
        public async Task<IEnumerable<stock_stocks>> GetStocks(int skip = 0, int take = 1)
        {
            return await _db.GetStocksParams(skip, take);
        }

        [HttpPost]
        [Route("AddStock")]
        public async Task<IActionResult> AddBond(stock_stocks stock)
        {
            await _db.AddStock(stock);
            return Ok();
        }
    }
}
