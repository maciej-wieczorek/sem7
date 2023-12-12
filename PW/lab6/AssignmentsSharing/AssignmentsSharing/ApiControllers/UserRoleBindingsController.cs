using AssignmentsSharing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssignmentsSharing.ApiControllers
{
    [MiddlewareFilter(typeof(ApiKeyMiddleware))]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleBindingsController : ControllerBase
    {
        private readonly UserManager<Developer> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public UserRoleBindingsController(UserManager<Developer> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/<UserRoleBindingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserRoleBindingsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        public class PostHelper
        {
            public string userName { get; set; }
            public string role { get; set; }
        }
        // POST api/<UserRoleBindingsController>
        [HttpPost]
        public void Post([FromBody] PostHelper parameters)
        {
            Task<Developer?> task = _userManager.FindByNameAsync(parameters.userName);
            task.Wait();
            var user = task.Result;
            _userManager.AddToRoleAsync(user, parameters.role);
        }

        // PUT api/<UserRoleBindingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserRoleBindingsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
