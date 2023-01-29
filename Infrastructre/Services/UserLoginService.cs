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
    public class UserLoginService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserLoginService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<UserLogInDto>>> Get()
        {
            try
            {
                var result = _context.UserLogins.ToList();
                var mapped = _mapper.Map<List<UserLogInDto>>(result);
                return new Response<List<UserLogInDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<UserLogInDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
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
                (x => (x.Email == logInDto.Username) && x.Password == logInDto.Password);

            if (existing == null)
            {
                return new Response<string>(HttpStatusCode.BadRequest, new List<string>() { "Username or password is incorrect" });
            }

            return new Response<string>("You are welcome");
        }

        public async Task<Response<UserLogInDto>> Update(UserLogInDto userDto)
        {
            try
            {

                var find = await _context.UserLogins.Where(x=>x.Id ==  userDto.Id).AsNoTracking().FirstOrDefaultAsync();
                if (find == null) return new Response<UserLogInDto>(HttpStatusCode.BadRequest, new List<string>() { "not Found" });

                var mapped = _mapper.Map<UserLogin>(userDto);
                _context.UserLogins.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<UserLogInDto>(userDto);
            }
            catch (Exception ex)
            {
                return new Response<UserLogInDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var existing = await _context.UserLogins.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });

            _context.UserLogins.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }

}

