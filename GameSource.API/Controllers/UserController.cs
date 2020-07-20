using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await userService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetByID(int id)
        {
            return await userService.GetByIDAsync(id);
        }

        [HttpPost]
        public async Task Insert([FromBody] User user)
        {
            await userService.InsertAsync(user);
        }

        [HttpPost]
        public async Task Update([FromBody] User user)
        {
            await userService.UpdateAsync(user);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await userService.DeleteAsync(id);
        }
    }
}
