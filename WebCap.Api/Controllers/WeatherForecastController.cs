using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace WebCap.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("Get")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [Route("GetSeasons")]
        [HttpGet]
        public string[] GetSeasons([FromServices] ICapPublisher capBus)
        {
            string[] Seasons = new[] { "Summer", "Sprint", "Autim", "Winter" };
            capBus.Publish("test.show.msg", "Seasons Called!");
            return Seasons;
        }

        [Route("SendMessage")]
        [HttpPost]
        public IActionResult SendMessage([FromServices] ICapPublisher capBus)
        {
            capBus.Publish("test.show.time", DateTime.Now);
            return Ok();
        }


        [Route("ReceiveMessage")]
        [HttpPost]
        [CapSubscribe("test.show.time")]
        public void ReceiveMessage(DateTime time)
        {
            Console.WriteLine("message time is:" + time);
        }
    }
}