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
            Response.StatusCode = StatusCodes.Status418ImATeapot;
            ViewBag.Error = Response.StatusCode;
            ViewBag.ErrorMessage = "Guess something went wrong...";
            return View("_Error");
        }
        [Route("/weather/{cityCode}")]
        public IActionResult Details(string? cityCode)
        {
            if(cityCode == null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorMessage = "No city code";
                return View("_Error");
            }

            CityWeather? city = cities.FirstOrDefault(c => c.CityUniqueCode == cityCode);

            if(city == null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorMessage = "No city found";
                return View("_Error");
            }

            return View(city);
        }
    }
}
