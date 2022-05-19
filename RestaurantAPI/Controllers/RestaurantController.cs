using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers {

    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase {

        private readonly IRestaurantServices restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices) {

            this.restaurantServices = restaurantServices;

        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute]int id) {

            if (!ModelState.IsValid) {

                return BadRequest(ModelState);

            }

            var isUpdated = restaurantServices.Update(id, dto);

            if (!isUpdated) {

                return NotFound();

            }

            return Ok();
        
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id) {
        
            var isDeleted = restaurantServices.Delete(id);

            if (isDeleted) {

                return NoContent();

            }

            return NotFound();
        
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto) {

            if (!ModelState.IsValid) {

                return BadRequest(ModelState);

            }
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

            if (restaurant is null) {

                return NotFound();

            }

            return Ok(restaurant);

        }

    }

}
