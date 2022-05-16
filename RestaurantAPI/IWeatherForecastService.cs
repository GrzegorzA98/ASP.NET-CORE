namespace RestaurantAPI {

    public interface IWeatherForecastService {

        IEnumerable<WeatherForecast> Get(int count, int max, int min);

    }

}