using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        //creating and assigning field context
        public IUserRepository _userRepository { get; }
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)//instantiating datacontext with context from IUserRepository
        {
            _mapper = mapper;
            _userRepository = userRepository;
            //Overall this method called dependency injection
        }

        //Get all method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers() //ActionResult-Helps in getting HTTP responses. IEnumerable-Since we return list so we use IEnumerable
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }

        //Get indivijual users
        [HttpGet("{username}")]
        public async Task<ActionResult<MembersDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }
    }
}