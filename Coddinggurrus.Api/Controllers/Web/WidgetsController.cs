using AutoMapper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coddinggurrus.Api.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetsController : ApiController
    {
        private readonly IWidgetsService _widgetsService;
        public WidgetsController(IWidgetsService widgetsService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _widgetsService = widgetsService;
        }
        [HttpGet("course-list-for-slider")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoursesForSlider()
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var courses = await _widgetsService.GetCoursesForSlider();
                basicResponse.Data = JsonConvert.SerializeObject(courses);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        [HttpGet("browse-topics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrowseTopics()
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var courses = await _widgetsService.GetBrowseTopics();
                basicResponse.Data = JsonConvert.SerializeObject(courses);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        [HttpGet("problem-faced-topics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProblemFacedTopics()
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var courses = await _widgetsService.GetProblemFacedTopics();
                basicResponse.Data = JsonConvert.SerializeObject(courses);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
    }
}
