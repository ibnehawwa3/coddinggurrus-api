using AutoMapper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coddinggurrus.Api.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebCourseController : ApiController
    {
        private readonly IWebCourseService _webCourseService;
        public WebCourseController(IWebCourseService webCourseService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _webCourseService = webCourseService;
        }
        [HttpGet("course-list-for-slider")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoursesForSlider()
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var courses = await _webCourseService.GetCoursesForSlider();
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
