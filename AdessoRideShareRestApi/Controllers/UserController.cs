using AdessoRideShareRestApi.Infrastructure;
using AdessoRideShareRestApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.Get();
            var usersModel = _mapper.Map<IEnumerable<UserModel>>(users);
            return Ok(usersModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.Get(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var userModel = _mapper.Map<UserModel>(user);
            return Ok(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserModel>(userModel);
            await _userService.Create(user);
            userModel = _mapper.Map<UserModel>(user);
            return CreatedAtAction(nameof(Get), new { id = userModel.UserId }, userModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserModel userModel)
        {
            if (await _userService.Validate(id) == false)
            {
                return NotFound("User not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userModel.UserId)
            {
                return BadRequest();
            }

            var user = _mapper.Map<UserModel>(userModel);
            await _userService.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _userService.Validate(id) == false)
            {
                return NotFound("User not found");
            }

            await _userService.Delete(id);
            return NoContent();
        }
    }
}
