using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingAppSocial.DTOs;
using DatingAppSocial.Entities;
using DatingAppSocial.Helpers;
using DatingAppSocial.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingAppSocial.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MembersDto> GetMemberAsync(string username)
        {
            return await _context.User
                .Where(x => x.UserName == username)
                .ProjectTo<MembersDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<MembersDto>> GetMembersAsync(UserParams userParams)
        {
            /*filter query*/
            var query = _context.User.AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);

            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

         /*then send to member Dto*/       
            return await PagedList<MembersDto>.CreateAsync(query.ProjectTo<MembersDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
                
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
          var response =  await _context.User.FindAsync(id);
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public  async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.User
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.User
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
             return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
