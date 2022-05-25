using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization {

    public class CreatedMultipleRestaurantRequirement : IAuthorizationRequirement {

        public CreatedMultipleRestaurantRequirement(int minimumRestaurantsCreated) {

            MinimumRestaurantsCreated = minimumRestaurantsCreated;

        }

        public int MinimumRestaurantsCreated {

            get;
            set;

        }

    }

}
