using AutoMapper;
using Domain.Dto;
using Domain.Dtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net; 

namespace Infrastructre.Servicess
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserService(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper; 
        }

        public async Task<Response<List<UserDto>>> Get()
        {
            try
            {
                var result = _context.Users.ToList();
                var mapped = _mapper.Map<List<UserDto>>(result);
                return new Response<List<UserDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<UserDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }
        public async Task<Response<string>> Register(UserDto userDto)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email || x.MobileNumber == userDto.MobileNumber);
            if (existing != null)
            {
                return new Response<string>(HttpStatusCode.BadRequest,
                    new List<string>() { "This email or phone already exists" });
            }
          
                var map = _mapper.Map<User>(userDto);
                await _context.Users.AddAsync(map);
                await _context.SaveChangesAsync();
                return new Response<string>("You are successfully registered"); 
        }
        public async Task<Response<string>> Login(LoginDto logInDto)
        {
            var existing = await _context.Users.FirstOrDefaultAsync
                (x => (x.Email == logInDto.Username || x.MobileNumber == logInDto.Username)
                && x.Password == logInDto.Password);

            if (existing == null)
            {
                return new Response<string>(HttpStatusCode.BadRequest, new List<string>() { "Username or password is incorrect" });
            }

            return new Response<string>("You are welcome");
        }

        public async Task<Response<UserDto>> Update(UserDto userDto) 
        {
            try
            {

                var existing = await _context.Users.Where(x => x.Id == userDto.Id).AsNoTracking().FirstOrDefaultAsync();
                if (existing == null) return new Response<UserDto>(HttpStatusCode.BadRequest, new List<string>() { "not Found" });

                var mapped = _mapper.Map<User>(userDto);
                _context.Users.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<UserDto>(userDto);
            }
            catch (Exception ex)
            {
                return new Response<UserDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
 
}

