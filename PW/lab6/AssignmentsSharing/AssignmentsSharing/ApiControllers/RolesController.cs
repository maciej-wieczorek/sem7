using AssignmentsSharing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssignmentsSharing.ApiControllers
{
    [MiddlewareFilter(typeof(ApiKeyMiddleware))]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UserManager<Developer> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public RolesController(UserManager<Developer> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            return _roleManager.Roles.Select(r => new { r.Id, r.Name }).ToList();
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RolesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _roleManager.CreateAsync(new IdentityRole<Guid>(value)).Wait();
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
