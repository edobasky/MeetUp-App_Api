using DatingAppSocial.Entities;

namespace DatingAppSocial.Interfaces
{
    public interface ITokenService
    {
        string CreationToken(AppUser user);
    }
}
