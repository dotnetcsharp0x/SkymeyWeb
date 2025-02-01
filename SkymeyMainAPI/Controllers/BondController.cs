using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkymeyLibs.Models.Tables.Bonds;
using SkymeyLibs.Models.Tables.Posts;
using SkymeyLibs.Models.Tables.Tokens;
using System.Net.Http;
using System.Text.Json;

namespace SkymeyGatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BondController : ControllerBase
    {
        [HttpGet]
        [Route("GetBonds")]
        public async Task<IEnumerable<stock_bonds>> GetBonds()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<stock_bonds> result = await client.GetFromJsonAsync<IEnumerable<stock_bonds>>("https://localhost:5701/Bonds/GetBonds");
                return result;
            }
        }
        [HttpGet]
        [Route("GetBondsParams")]
        public async Task<List<stock_bonds>> GetBondsParams(int skip = 0, int take = 1)
        {
            using (var client = new HttpClient())
            {
                List<stock_bonds> result = await client.GetFromJsonAsync<List<stock_bonds>>("https://localhost:5701/Bonds/GetBondsParams?skip=" + skip+"&take="+take);
                return result;
            }
        }
        [HttpPost]
        [Route("AddBond")]
        public async Task<bool> AddBond(stock_bonds bond)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonSerializer.Serialize(bond), null, "application/json");

                var resp = await client.PostAsync("https://localhost:5701/Bonds/AddBond", content);
            }
            return true;
        }
    }
}
