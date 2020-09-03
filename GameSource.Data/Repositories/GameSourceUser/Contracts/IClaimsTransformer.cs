using System.Security.Claims;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser.Contracts
{
    public interface IClaimsTransformer
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal);
    }
}
