using AutoMapper;
using Domain.Dto;
using Domain.Dtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructre.Services
{
    public class RolePermissionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RolePermissionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<RolePermissionDto>>> GetRolePermission()
        {
            try
            {
                var result = _context.RolePermissions.ToList();
                var mapped = _mapper.Map<List<RolePermissionDto>>(result);
                return new Response<List<RolePermissionDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<RolePermissionDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }

        public async Task<Response<RolePermissionDto>> Add(RolePermissionDto rolePDto)
        {
            try
            {
                var roleP = _mapper.Map<RolePermission>(rolePDto);
                await _context.RolePermissions.AddAsync(roleP);
                await _context.SaveChangesAsync();
                return new Response<RolePermissionDto>(rolePDto);
            }
            catch (Exception e)
            {
                return new Response<RolePermissionDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }

        public async Task<Response<RolePermissionDto>> Update(RolePermissionDto rolePDto)
        {
            try
            {
                var existing = await _context.RolePermissions.Where(x => x.Id == rolePDto.Id).AsNoTracking().FirstOrDefaultAsync();

                if (existing == null) return new Response<RolePermissionDto>(HttpStatusCode.BadRequest, new List<string>() { "RolePermission not Found" });

                var mapped = _mapper.Map<RolePermission>(rolePDto);
                _context.RolePermissions.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<RolePermissionDto>(rolePDto);
            }
            catch (Exception ex)
            {
                return new Response<RolePermissionDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var existing = await _context.RolePermissions.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Id Not Found" });

            _context.RolePermissions.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
