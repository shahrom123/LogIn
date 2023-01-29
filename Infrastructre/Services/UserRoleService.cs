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
    public class UserRoleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRoleService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<UserRoleDto>>> GetUserRole()
        {
            try
            {
                var result = _context.UserRoles.ToList();
                var mapped = _mapper.Map<List<UserRoleDto>>(result);
                return new Response<List<UserRoleDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<UserRoleDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }

        public async Task<Response<UserRoleDto>> Add(UserRoleDto userRoleDto)
        {
            try
            {
                var userRole = _mapper.Map<UserRole>(userRoleDto);
                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();
                return new Response<UserRoleDto>(userRoleDto);
            }
            catch (Exception e)
            {
                return new Response<UserRoleDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }

        public async Task<Response<UserRoleDto>> Update(UserRoleDto userRoleDto)
        {
            try
            {
                var existing = await _context.UserRoles.Where(x => x.Id == userRoleDto.Id).AsNoTracking().FirstOrDefaultAsync();
                if (existing == null) return new Response<UserRoleDto>(HttpStatusCode.BadRequest, new List<string>() { "Role not Found" });

                var mapped = _mapper.Map<RolePermission>(userRoleDto);
                _context.RolePermissions.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<UserRoleDto>(userRoleDto);
            }
            catch (Exception ex)
            {
                return new Response<UserRoleDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var existing = await _context.UserRoles.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Id Not Found" });

            _context.UserRoles.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
