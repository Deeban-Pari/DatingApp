using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context,ITokenService tokenService)
        {
            _context=context;
            _tokenService = tokenService;
        }

    //User Register Method
        [HttpPost("register")]//Post:http://4200/api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username))
            return BadRequest("Username already exists"); // Return an appropriate response if the username is taken or not

            if (string.IsNullOrEmpty(registerDto.Password))
            return BadRequest("Password is required"); // Return an appropriate response if the password is null or empty

            using var hmac=new HMACSHA512(); //purpost of using->when ever this class is used and it removes this from memory when we use "using" by "Automatic garbage collector" 
            var user=new AppUser
            {
                UserName=registerDto.Username,
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt=hmac.Key,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username=user.UserName,
                Token=_tokenService.CreateToken(user)
            };
        }

    //Login Method
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await _context.Users.SingleOrDefaultAsync(x=>x.UserName==loginDto.Username); //SingleOrDefaultAsync->Lookes for single value to get if nothing matches return Default value same as FirstOrDefault
            if(user==null) return Unauthorized("Invalid Username");
            using var hmac=new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0;i<computedHash.Length;i++)
            {
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password ");
            }
            return new UserDto
            {
                Username=user.UserName,
                Token=_tokenService.CreateToken(user)
            };

        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x=>x.UserName==username.ToLower());
        }
        
    }
}