using DatingAppSocial.DTOs;
using DatingAppSocial.Entities;
using DatingAppSocial.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingAppSocial.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);

        Task<PagedList<MembersDto>> GetMembersAsync(UserParams userParams);
        Task<MembersDto> GetMemberAsync(string username);
    }
}
