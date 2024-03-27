using AutoMapper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coddinggurrus.Api.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseContentController : ApiController
    {
        private readonly ICourseContentService _courseContentService;
        public CourseContentController(ICourseContentService courseContentService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _courseContentService = courseContentService;
        }
        [HttpGet("topics-by-course-Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopicsCourseId(long courseId)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var topics = await _courseContentService.GetTopicsByCourseId(courseId);
                basicResponse.Data = JsonConvert.SerializeObject(topics);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpGet("topic-content-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopicContentById(long topicId)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var topicContent = await _courseContentService.GetTopicContentById(topicId);
                basicResponse.Data = topicContent;
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

    }
}
