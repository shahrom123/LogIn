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
    public class RoleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RoleService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<RoleDto>>> GetRole()
        {
            try
            {
                var result = _context.Roles.ToList();
                var mapped = _mapper.Map<List<RoleDto>>(result);
                return new Response<List<RoleDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<RoleDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }

        public async Task<Response<RoleDto>> Add(RoleDto roleDto)
        {
            try
            {
                var roles = _mapper.Map<Role>(roleDto);
                await _context.Roles.AddAsync(roles);
                await _context.SaveChangesAsync();
                return new Response<RoleDto>(roleDto);
            }
            catch (Exception e)
            {
                return new Response<RoleDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }

        public async Task<Response<RoleDto>> Update(RoleDto roleDto)
        {
            try
            {
                var existing = await _context.Roles.Where(x => x.Id == roleDto.Id).AsNoTracking().FirstOrDefaultAsync();

                if (existing == null) return new Response<RoleDto>(HttpStatusCode.BadRequest, new List<string>() {"Role not Found"});

                var mapped = _mapper.Map<Role>(roleDto);
                _context.Roles.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<RoleDto>(roleDto);
            }
            catch (Exception ex)
            {
                return new Response<RoleDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var existing = await _context.Roles.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() {"Id Not Found"});

            _context.Roles.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
