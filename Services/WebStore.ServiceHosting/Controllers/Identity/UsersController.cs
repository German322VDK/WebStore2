using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;
using WebStrore.DAL.Context;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebAPI.Identity.Users)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _UserStore;

        public UsersController(WebStoreDB db) => _UserStore = 
            new UserStore<User, Role, WebStoreDB>(db);

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers() =>
            await _UserStore.Users.ToArrayAsync();
    }
}
