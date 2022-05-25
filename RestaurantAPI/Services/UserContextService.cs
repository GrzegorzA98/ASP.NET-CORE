using System.Security.Claims;

namespace RestaurantAPI.Services {

    public class UserContextService : IUserContextService {

        private readonly IHttpContextAccessor httpContextAccessor;

        public ClaimsPrincipal User => httpContextAccessor.HttpContext?.User;
        public int? GetUserId => User is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public UserContextService(IHttpContextAccessor httpContextAccessor) {

            this.httpContextAccessor = httpContextAccessor;

        }



    }

}
