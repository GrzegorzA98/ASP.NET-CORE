using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers {

    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase {

        private readonly IDishService dishService;

        public DishController(IDishService dishService) {

            this.dishService = dishService;

        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId) {

            dishService.RemoveAll(restaurantId);

            return NoContent();
        
        }

        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId, [FromBody]CreateDishDto dto) {

            var newDishId = dishService.Create(restaurantId, dto);

            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}", null);
        
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId) {

            DishDto dish = dishService.GetById(restaurantId, dishId);

            return Ok(dish);
        
        }

        [HttpGet]
        public ActionResult<List<DishDto>> Get([FromRoute] int restaurantId) {

            var result = dishService.GetAll(restaurantId);

            return Ok(result);

        }

    }

}
