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
    public class RolePermissionController : ControllerBase
    {
        private RolePermissionService _rolePermissionService;

        public RolePermissionController(RolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }
        [HttpGet("GetRolePermission")]
        public async Task<Response<List<RolePermissionDto>>> Get()
        {
            return await _rolePermissionService.GetRolePermission();

        }
        [HttpPost("AddRolePermission")]
        public async Task<Response<RolePermissionDto>> Add(RolePermissionDto rolePDto)
        {
            if (ModelState.IsValid)
            {
                return await _rolePermissionService.Add(rolePDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<RolePermissionDto>(HttpStatusCode.BadRequest, errors);
            }
        }

        [HttpPut("UpdateRolePermission")]
        public async Task<Response<RolePermissionDto>> Put([FromBody] RolePermissionDto rolePDto) => await _rolePermissionService.Update(rolePDto);

        [HttpDelete("DeleteRolePermission")]
        public async Task<Response<string>> Delete(int id) => await _rolePermissionService.Delete(id);
    }

}
