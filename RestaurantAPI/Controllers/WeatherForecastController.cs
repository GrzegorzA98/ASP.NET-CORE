using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {

        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService) {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery]int count, [FromBody]TemperatureRequest temperatureRequest) {

            if (count <= 0 || temperatureRequest.Max < temperatureRequest.Min) {

                return BadRequest();

            }

            else {

                var result = _weatherForecastService.Get(count, temperatureRequest.Min, temperatureRequest.Max  );
                return Ok(result);
            
            }


        }
        [HttpPost]
        public ActionResult<string> Hello([FromBody] string name) {

            HttpContext.Response.StatusCode = 401;
            return $"Hello {name}";

        }

    }

}