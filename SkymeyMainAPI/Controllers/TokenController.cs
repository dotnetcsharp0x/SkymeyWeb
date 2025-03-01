using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkymeyLibs.Models.Tables.Posts;
using SkymeyLibs.Models.Tables.Tokens;
using System.Net.Http;
using System.Text.Json;

namespace SkymeyGatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        [Route("GetTokens")]
        public async Task<IEnumerable<API_TOKEN>> GetTokens()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<API_TOKEN> result = await client.GetFromJsonAsync<IEnumerable<API_TOKEN>>("https://localhost:5101/Token/GetTokens");
                return result;
            }
        }
        [HttpGet]
        [Route("GetTokenList")]
        public async Task<List<TokenList>> GetTokenList()
        {
            using (var client = new HttpClient())
            {
                List<TokenList> result = await client.GetFromJsonAsync<List<TokenList>>("https://localhost:5101/Token/GetTokenList");
                return result;
            }
        }
        [HttpGet]
        [Route("GetToken")]
        public async Task<API_TOKEN> GetToken(string slug)
        {
            using (var client = new HttpClient())
            {
                API_TOKEN result = await client.GetFromJsonAsync<API_TOKEN>("https://localhost:5101/Token/GetToken?slug=" + slug);
                return result;
            }
        }
        [HttpPost]
        [Route("AddToken")]
        public async Task<bool> AddToken(API_TOKEN token)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonSerializer.Serialize(token), null, "application/json");

                var resp = await client.PostAsync("https://localhost:5101/Token/AddToken", content);
            }
            return true;
        }
    }
}
