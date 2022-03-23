using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingAppSocial.DTOs;
using DatingAppSocial.Entities;
using DatingAppSocial.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public Task<IEnumerable<MembersDto>> GetMembersAsync()
        {
            throw new System.NotImplementedException();
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
