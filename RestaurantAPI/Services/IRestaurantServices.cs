using RestaurantAPI.Models;

namespace RestaurantAPI.Services {

    public interface IRestaurantServices {

        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        bool Delete(int id);
        public bool Update(int id, UpdateRestaurantDto dto);

    }

}