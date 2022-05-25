using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Security.Claims;

namespace RestaurantAPI.Authorization {

    public class CreatedMultipleRestaurantRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantRequirement> {

        private readonly RestaurantDbContext restaurantDbContext;

        public CreatedMultipleRestaurantRequirementHandler(RestaurantDbContext restaurantDbContext) {

            this.restaurantDbContext = restaurantDbContext;

        }
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantRequirement requirement) {

            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

           var createdRestaurantsCount =  restaurantDbContext.Restaurants.Count(r => r.CreatedById == userId);

            if (createdRestaurantsCount >= requirement.MinimumRestaurantsCreated) {

                context.Succeed(requirement);

            }

            return Task.CompletedTask;

        }

    }

}
