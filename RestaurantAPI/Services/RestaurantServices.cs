using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Security.Claims;

namespace RestaurantAPI.Services {

    public class RestaurantServices : IRestaurantServices {

        private readonly RestaurantDbContext restaurantDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantServices> _logger;
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;

        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantServices> logger, IAuthorizationService authorizationService, IUserContextService userContextService) {

            restaurantDbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
        }

        public void Update(int id, UpdateRestaurantDto dto) {

            var restaurant = restaurantDbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) {

                throw new NotFoundException("Restaurant not found");

            }

            var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded) {

                throw new ForbidException();

            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            restaurantDbContext.SaveChanges();
        
        }

        public void Delete(int id) {

            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = restaurantDbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) {

                throw new NotFoundException("Restaurant not found");

            }

            var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded) {

                throw new ForbidException();

            }

            restaurantDbContext.Restaurants.Remove(restaurant);
            restaurantDbContext.SaveChanges();

        }

        public RestaurantDto GetById(int id) {

            var restaurant = restaurantDbContext.Restaurants.Include(r => r.Address).Include(r => r.Dishes).FirstOrDefault(r => r.Id == id);

            if (restaurant is null) {

                throw new NotFoundException("Restaurant not found");

            }

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;

        }

        public IEnumerable<RestaurantDto> GetAll() {

            var restaurants = restaurantDbContext.Restaurants.Include(r => r.Address).Include(r => r.Dishes).ToList();

            //var restaurantsDtos = restaurants.Select(r => new RestaurantDto() {

            //    Name = r.Name,
            //    Category = r.Category,
            //    City = r.Address.City

            //});

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantsDtos;

        }

        public int Create(CreateRestaurantDto dto) {

            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = userContextService.GetUserId;
            restaurantDbContext.Restaurants.Add(restaurant);
            restaurantDbContext.SaveChanges();

            return restaurant.Id;

        }

    }

}
