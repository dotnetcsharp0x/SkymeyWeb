using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using SkymeyLibs.Enums.User;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Models.User;

namespace SkymeyUserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _db;

        public UserController(ILogger<UserController> logger, IUserRepository db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [Route("AddUserToGroup")]
        public async Task AddUserToGroup(SU_001 user, SU010_Types group_nr)
        {
            await _db.AddUserToGroup(user, group_nr);
        }

        [HttpGet]
        [Route("GetUserInGroup")]
        public async Task<SG_001> GetUserInGroup(ObjectId id_user)
        {
            return await _db.GetUserInGroup(id_user);
        }

        [HttpGet]
        [Route("GetGroup")]
        public async Task<SG_010> GetGroup(int group_nr)
        {
            return await _db.GetGroup(group_nr);
        }

        [HttpGet]
        [Route("GetUserByEmail")]
        public async Task<SU_001> GetUserByEmail(string email)
        {
            return await _db.GetUserByEmail(email);
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<List<SU_001>> GetUsers()
        {
            return await _db.GetUsers();
        }

        [HttpGet]
        [Route("CreateUser")]
        public async Task<bool> CreateUser(SU_001 user)
        {
            return await _db.CreateUser(user);
        }
    }
}
