using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        List<CityWeather> cities = new List<CityWeather>() {
            new CityWeather() {
                CityUniqueCode = "LDN",
                CityName = "London",
                DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),
                TemperatureFahrenheit = 33 
            },
            new CityWeather() {
                CityUniqueCode = "NYC",
                CityName = "New York",
                DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),
                TemperatureFahrenheit = 60
            },
            new CityWeather() {
                CityUniqueCode = "PRS",
                CityName = "Paris",
                DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),
                TemperatureFahrenheit = 82
            },
        };

        [Route("/")]
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                return View(cities);
            }
            return View("_Error");
        }
        [Route("/weather/{cityCode}")]
        public IActionResult Details(string? cityCode)
        {
            if(cityCode == null)
            {
                return View("_Error", "error-message");
            }

            CityWeather? city = cities.FirstOrDefault(c => c.CityUniqueCode == cityCode);

            if(city == null)
            {
                return View("_Error", "error-message");
            }

            return View(city);
        }
    }
}
