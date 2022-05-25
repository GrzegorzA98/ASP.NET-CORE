using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantAPI.Authorization {

    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement> {

        private readonly ILogger<MinimumAgeRequirement> logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirement> logger) {

            this.logger = logger;

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement) {

            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            logger.LogInformation($"User: {userEmail} with date of birth: [{dateOfBirth}])");

            if (dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today) {

                logger.LogInformation("Authorization succedded");
                context.Succeed(requirement);

            }

            else {

                logger.LogInformation("Authorization failed");
            
            }

            return Task.CompletedTask;

        }

    }

}
