using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers {

    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase {

        private readonly IRestaurantServices restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices) {

            this.restaurantServices = restaurantServices;

        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute]int id) {

            restaurantServices.Update(id, dto);

            return Ok();
        
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id) {
        
            restaurantServices.Delete(id);

            return NotFound();
        
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto) {

            var id = restaurantServices.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll() {

            var restaurantDtos = restaurantServices.GetAll();

            return Ok(restaurantDtos);
        
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id) {

            var restaurant = restaurantServices.GetById(id);

            return Ok(restaurant);

        }

    }

}
