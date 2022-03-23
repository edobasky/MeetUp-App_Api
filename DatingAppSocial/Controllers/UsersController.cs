using AutoMapper;
using DatingAppSocial.Data;
using DatingAppSocial.DTOs;
using DatingAppSocial.Entities;
using DatingAppSocial.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingAppSocial.Controllers
{
   [Authorize]
    public class UsersController : BaseApiController
    {
       
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(DataContext context, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MembersDto>>(users);
            return Ok(usersToReturn);
         ;
            
        }

        //api/user/3
       
        [HttpGet("{username}")]
        public async Task<ActionResult<MembersDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
           
            
        }
    }
}
