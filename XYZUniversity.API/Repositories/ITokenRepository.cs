using Microsoft.AspNetCore.Identity;

namespace XYZUniversity.API.Repositories
{
    public interface ITokenRepository
    {
        String CreateJWTToken(IdentityUser user, List<string> roles);

    }
}
