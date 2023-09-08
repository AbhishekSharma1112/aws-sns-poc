using Amazon;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;

namespace aws_sns_poc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISNSService _smsSender;

        public WeatherForecastController(ISNSService smsSender)
        {
            _smsSender = smsSender;
        }

        [HttpGet]
        public async Task SendSMSAsync([FromQuery] string phoneNumber)
        {
            try
            {
                var result = await _smsSender.SendSMSAsync(
                   phoneNumber,
                       "Title",
                       "Message");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                //handle failure
            }
        }
    }
}