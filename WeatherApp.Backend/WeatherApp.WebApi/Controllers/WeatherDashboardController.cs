using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.DataContract.Api;
using WeatherApp.DomainModel.Contracts;
using WeatherApp.DomainModel.DomainModels;

namespace WeatherApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class WeatherDashboardController : ControllerBase
    {
        private readonly IWeatherAppService _weatherAppService;
        private IMapper _mapper;
        private readonly ILogger<WeatherDashboardController> _logger;

        public WeatherDashboardController(IWeatherAppService weatherAppService, IMapper mapper, ILogger<WeatherDashboardController> logger)
        {
            _weatherAppService = weatherAppService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("weather")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WeatherResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> GetCurrentWeatherDetails([FromHeader(Name = "X-User-Id")] string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return BadRequest("Empty 'userId' header.");
                }

                var weatherDetails = await _weatherAppService.GetCurrentWeatherRecordsForAllCitiesAsync(userId);

                if (weatherDetails == null || !weatherDetails.Any())
                {
                    return NotFound("No weather details found.");
                }

                var response = _mapper.Map<IEnumerable<WeatherResponseModel>>(weatherDetails);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving weather details");

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                };

                return StatusCode(problemDetails.Status.Value, problemDetails);
            }
        }

        [HttpPost("weather/userpreference")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPreferenceDomainModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> SetUserPreference([FromBody] UserPreferenceDomainModel userPreference)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdPreference = await _weatherAppService.SaveUserPreferenceAsync(userPreference);
                return Ok(createdPreference);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving user preferences");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while saving user preferences");
            }
        }

        [HttpGet("weather/trend")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<string,string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> GetCurrentTrends()
        {
            try
            {
                var weatherTrends = await _weatherAppService.GetTemperatureTrendForAllCitiesAsync();
                return Ok(weatherTrends);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving weather trend");
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                };
                return StatusCode(problemDetails.Status.Value, problemDetails);
            }
        }
    }
}
