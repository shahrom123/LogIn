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
    public class UserRoleController : ControllerBase
    {
        private UserRoleService _userRoleService;

        public UserRoleController(UserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }
        [HttpGet("GetUserRole")]
        public async Task<Response<List<UserRoleDto>>> Get()
        {
            return await _userRoleService.GetUserRole();

        }
        [HttpPost("AddUserRole")]
        public async Task<Response<UserRoleDto>> Add(UserRoleDto userRoleDto)
        {
            if (ModelState.IsValid)
            {
                return await _userRoleService.Add(userRoleDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<UserRoleDto>(HttpStatusCode.BadRequest, errors);
            }
        }

        [HttpPut("UpdateUserRole")]
        public async Task<Response<UserRoleDto>> Put([FromBody] UserRoleDto userRoleDto) => await _userRoleService.Update(userRoleDto);

        [HttpDelete("DeleteUserRole")]
        public async Task<Response<string>> Delete(int id) => await _userRoleService.Delete(id);
    }

}
