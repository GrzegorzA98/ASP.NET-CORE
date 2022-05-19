using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services {

    public class RestaurantServices : IRestaurantServices {

        private readonly RestaurantDbContext restaurantDbContext;
        private readonly IMapper _mapper;

        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper) {

            restaurantDbContext = dbContext;
            _mapper = mapper;

        }

        public bool Update(int id, UpdateRestaurantDto dto) {

            var restaurant = restaurantDbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) {

                return false;

            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            restaurantDbContext.SaveChanges();

            return true;
        
        }

        public bool Delete(int id) {

            var restaurant = restaurantDbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) {

                return false;

            }

            restaurantDbContext.Restaurants.Remove(restaurant);
            restaurantDbContext.SaveChanges();

            return true;

        }

        public RestaurantDto GetById(int id) {

            var restaurant = restaurantDbContext.Restaurants.Include(r => r.Address).Include(r => r.Dishes).FirstOrDefault(r => r.Id == id);

            if (restaurant is null) {

                return null;

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
