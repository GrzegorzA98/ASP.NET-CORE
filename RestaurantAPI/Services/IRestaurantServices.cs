using RestaurantAPI.Models;
using System.Security.Claims;

namespace RestaurantAPI.Services {

    public interface IRestaurantServices {

        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);

    }

}