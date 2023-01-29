using Domain.Dto;
using Domain.Dtos;
using Domain.Response;
using Infrastructre.Servicess;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class UserLoginController : ControllerBase
    {
        private UserLoginService _userLoginService;

        public UserLoginController(UserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }
        [HttpGet("UserLogin")]
        public async Task<Response<List<UserLogInDto>>> Get()
        {
            return await _userLoginService.Get();

        }
        [HttpPost("Register")]
        public async Task<Response<string>> Register(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                return await _userLoginService.Register(userDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<string>(HttpStatusCode.BadRequest, errors);
            }
        }
        [HttpPost("login")]
        public async Task<Response<string>> Login(LoginDto logInDto)
        {
            if (ModelState.IsValid)
            {
                return await _userLoginService.Login(logInDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).
                    Select(e => e.ErrorMessage).ToList();
                return new Response<string>(HttpStatusCode.BadRequest, errors);

            }
        }
        [HttpPut("UpdateUserLogin")]
        public async Task<Response<UserLogInDto>> Put([FromBody] UserLogInDto userLoginDto) => await _userLoginService.Update(userLoginDto);
        [HttpDelete("DeleteLogin")]
        public async Task<Response<string>> Delete(int id) => await _userLoginService.Delete(id);



    }
}