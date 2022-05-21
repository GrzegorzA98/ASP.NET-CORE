using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services {

    public class DishService : IDishService {

        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;

        public DishService(RestaurantDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        public int Create(int restaurantId, CreateDishDto dto) {

            var restaurant = GetRestaurantById(restaurantId);
            var dishEntity = mapper.Map<Dish>(dto);

            dishEntity.RestaurantId = restaurantId;

            context.Dishes.Add(dishEntity);
            context.SaveChanges();

            return dishEntity.Id;

        }

        public DishDto GetById(int restaurantId, int dishId) {

            var restaurant = GetRestaurantById(restaurantId);

            var dish = context.Dishes.FirstOrDefault(d => d.Id == dishId);

            if (dish is null || dish.RestaurantId != restaurantId) {

                throw new NotFoundException("Dish not found");

            }

            var dishDto = mapper.Map<DishDto>(dish);

            return dishDto;

        }

        public List<DishDto> GetAll(int restaurantId) {

            var restaurant = GetRestaurantById(restaurantId);
            var dishDtos = mapper.Map<List<DishDto>>(restaurant.Dishes);

            return dishDtos;

        }

        public void RemoveAll(int restaurantId) {

            var restaurant = GetRestaurantById(restaurantId);

            context.RemoveRange(restaurant.Dishes);
            context.SaveChanges();

        }

        private Restaurant GetRestaurantById(int restaurantId) {

            var restaurant = context.Restaurants.Include(r => r.Dishes).FirstOrDefault(r => r.Id == restaurantId);

            return restaurant;

        }

    }

}
