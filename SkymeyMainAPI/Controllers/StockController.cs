using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkymeyLibs.Models.Tables.Posts;
using SkymeyLibs.Models.Tables.Stocks;
using SkymeyLibs.Models.Tables.Tokens;
using System.Net.Http;
using System.Text.Json;

namespace SkymeyGatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        [HttpGet]
        [Route("GetStocks")]
        public async Task<IEnumerable<stock_stocks>> GetStocks()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<stock_stocks> result = await client.GetFromJsonAsync<IEnumerable<stock_stocks>>("https://localhost:5703/Stocks/GetStocks");
                return result;
            }
        }
        [HttpGet]
        [Route("GetStocksParams")]
        public async Task<List<stock_stocks>> GetStocksParams(int skip = 0, int take = 1)
        {
            using (var client = new HttpClient())
            {
                List<stock_stocks> result = await client.GetFromJsonAsync<List<stock_stocks>>("https://localhost:5703/Stocks/GetStocksParams?skip=" + skip+"&take="+take);
                return result;
            }
        }
        [HttpGet]
        [Route("GetStock")]
        public async Task<stock_stocks> GetStock(string isin)
        {
            using (var client = new HttpClient())
            {
                stock_stocks result = await client.GetFromJsonAsync<stock_stocks>("https://localhost:5703/Stocks/GetStock?isin=" + isin);
                return result;
            }
        }
        [HttpPost]
        [Route("AddStock")]
        public async Task<bool> AddStock(stock_stocks stock)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonSerializer.Serialize(stock), null, "application/json");

                var resp = await client.PostAsync("https://localhost:5703/Stocks/AddStock", content);
            }
            return true;
        }
    }
}
