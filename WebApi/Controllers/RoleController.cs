using Domain.Dto;
using Domain.Dtos;
using Domain.Response;
using Infrastructre.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class RoleController : ControllerBase
    {
        private RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService= roleService;
        }
        [HttpGet("GetRole")]
        public async Task<Response<List<RoleDto>>> Get()
        {
            return await _roleService.GetRole();

        }
        [HttpPost("AddRole")]
        public async Task<Response<RoleDto>> Add(RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                return await _roleService.Add(roleDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<RoleDto>(HttpStatusCode.BadRequest, errors);
            }
        }

        [HttpPut("UpdateRole")]
        public async Task<Response<RoleDto>> Put([FromBody] RoleDto roleDto) => await _roleService.Update(roleDto);

        [HttpDelete("DeleteRole")]
        public async Task<Response<string>> Delete(int id) => await _roleService.Delete(id);
    }

}
