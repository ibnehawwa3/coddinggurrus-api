using AutoMapper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coddinggurrus.Web.Api.Controllers.Tutorials
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ApiController
    {
        private readonly IWebCourseService _webCourseService;
        public CourseController(IWebCourseService webCourseService, IMapper mapper, IConfiguration config) : base(mapper, config)
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
