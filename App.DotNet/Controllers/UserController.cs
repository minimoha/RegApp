using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using App.DotNet.Models;
using App.DotNet.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var Users = _userRepository.GetUsers();
            return new OkObjectResult(Users);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var User = _userRepository.GetUserByID(id);
            return new OkObjectResult(User);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User User)
        {
            using (var scope = new TransactionScope())
            {
                _userRepository.InsertUser(User);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = User.Id }, User);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] User User)
        {
            if (User != null)
            {
                using (var scope = new TransactionScope())
                {
                    _userRepository.UpdateUser(User);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userRepository.DeleteUser(id);
            return new OkResult();
        }
    }
}
