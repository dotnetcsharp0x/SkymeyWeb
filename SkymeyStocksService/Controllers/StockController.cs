using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SkymeyLibs;
using SkymeyLibs.Models.Tables.Tokens;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Models.Tables.Stocks;
using MongoDB.Driver;
using SkymeyLibs.Data;

namespace SkymeyStocksService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private MongoClient _client;
        private MongoPostRepository _postRepository;
        private IOptions<MainSettings> _options;

        public StocksController(MongoClient client, IOptions<MainSettings> options)
        {
            _client = client;
            _options = options;
            _postRepository = new MongoPostRepository(_client, _options.Value.MongoDatabase);
        }

        [HttpGet]
        [Route("GetStocks")]
        public async Task<IEnumerable<stock_stocks>> GetStocks()
        {
            return await _postRepository.GetStocks();
        }

        [HttpGet]
        [Route("GetStocksParams")]
        public async Task<IEnumerable<stock_stocks>> GetStocks(int skip = 0, int take = 1)
        {
            return await _postRepository.GetStocksParams(skip, take);
        }

        [HttpGet]
        [Route("GetStock")]
        public async Task<stock_stocks> GetStock(string isin)
        {
            return await _postRepository.GetStock(isin);
        }

        [HttpPost]
        [Route("AddStock")]
        public async Task<IActionResult> AddBond(stock_stocks stock)
        {
            await _postRepository.AddStock(stock);
            return Ok();
        }
    }
}
