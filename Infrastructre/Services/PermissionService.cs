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
    public class PermissionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PermissionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<PermissionDto>>> GetPermission()
        {
            try
            {
                var result = _context.Permissions.ToList();
                var mapped = _mapper.Map<List<PermissionDto>>(result);
                return new Response<List<PermissionDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<PermissionDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }

        public async Task<Response<PermissionDto>> Add(PermissionDto permissionDto)
        {
            try
            {
                var permission = _mapper.Map<Permission>(permissionDto);
                await _context.Permissions.AddAsync(permission);
                await _context.SaveChangesAsync();
                return new Response<PermissionDto>(permissionDto);
            }
            catch (Exception e)
            {
                return new Response<PermissionDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }

        public async Task<Response<PermissionDto>> Update(PermissionDto permissionDto)
        {
            try
            {
                var existing = await _context.Permissions.Where(x => x.Id == permissionDto.Id).AsNoTracking().FirstOrDefaultAsync();
                if (existing == null) return new Response<PermissionDto>(HttpStatusCode.BadRequest, new List<string>() { "Permission not Found" });
               
                var map = _mapper.Map<Permission>(permissionDto);
                _context.Permissions.Update(map);
                await _context.SaveChangesAsync();
                return new Response<PermissionDto>(permissionDto);
            }
            catch (Exception ex)
            {
                return new Response<PermissionDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var existing = await _context.Permissions.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Id Not Found" });

            _context.Permissions.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
