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
    [Route(WebAPI.Identity.Roles)]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleStore<Role> _RoleStore;

        public RolesController(WebStoreDB db) => _RoleStore = new RoleStore<Role>(db);

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAllUsers() =>
            await _RoleStore.Roles.ToArrayAsync();
    }
}
