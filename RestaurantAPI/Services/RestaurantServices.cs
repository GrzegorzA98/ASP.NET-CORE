using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services {

    public class RestaurantServices : IRestaurantServices {

        private readonly RestaurantDbContext restaurantDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantServices> _logger;

        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantServices> logger) {

            restaurantDbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public void Update(int id, UpdateRestaurantDto dto) {

            var restaurant = restaurantDbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) {

                throw new NotFoundException("Restaurant not found");

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
            restaurantDbContext.Restaurants.Add(restaurant);
            restaurantDbContext.SaveChanges();

            return restaurant.Id;

        }

    }

}
