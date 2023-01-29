using Domain.Dto;
using Domain.Dtos;
using Domain.Response;
using Infrastructre.Services;
using Infrastructre.Servicess;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{ 
    [ApiController]
    [Route("Controller")]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetUser")]
        public async Task<Response<List<UserDto>>> Get()
        {
            return await _userService.Get();

        }
        [HttpPost("RegisterUser")] 
        public async Task<Response<string>> Register(UserDto userDto) 
        {
            if (ModelState.IsValid)
            {
                return await _userService.Register(userDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<string>(HttpStatusCode.BadRequest, errors);
            }
        }
        [HttpPost("loginUser")]
        public async Task<Response<string>> Login(LoginDto logInDto)
        {
            if (ModelState.IsValid)
            {
                return await _userService.Login(logInDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).
                    Select(e => e.ErrorMessage).ToList();
                return new Response<string>(HttpStatusCode.BadRequest, errors);

            }
        }
        [HttpPut("UpdateUser")]
        public async Task<Response<UserDto>> Put([FromBody] UserDto userDto) => await _userService.Update(userDto);
        [HttpDelete("DeleteUser")]
        public async Task<Response<string>> Delete(int id) => await _userService.Delete(id);



    }
}