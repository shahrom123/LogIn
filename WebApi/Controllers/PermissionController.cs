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
    public class PermissionController : ControllerBase
    {
        private PermissionService _permissionService; 

        public PermissionController(PermissionService permissionService)
        {
            _permissionService= permissionService;
        }
        [HttpGet("GetPermission")]
        public async Task<Response<List<PermissionDto>>> Get()
        {
            return await _permissionService.GetPermission(); 
        }
        [HttpPost("AddPermission")]
        public async Task<Response<PermissionDto>> Add(PermissionDto permissionDto)
        {
            if (ModelState.IsValid)
            {
                return await _permissionService.Add(permissionDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<PermissionDto>(HttpStatusCode.BadRequest, errors);
            }
        }

        [HttpPut("UpdatePermission")]
        public async Task<Response<PermissionDto>> Put([FromBody] PermissionDto permissionDto) => await _permissionService.Update(permissionDto);

        [HttpDelete("DeletePermission")]
        public async Task<Response<string>> Delete(int id) => await _permissionService.Delete(id);
    }

}
