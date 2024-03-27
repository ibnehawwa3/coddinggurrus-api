using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Generic;
using Coddinggurrus.Api.Models.Admin.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.ProblemsFaced;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coddinggurrus.Api.Controllers.Admin.ProblemsFaced
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemTopicController : AdminController
    {
        private readonly IProblemTopicService _problemTopicService;
        public ProblemTopicController(IProblemTopicService problemTopicService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _problemTopicService = problemTopicService;
        }
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery] ListingParameter listingParameter)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var topics = await _problemTopicService.GetTopics(listingParameter);
                basicResponse.Data = JsonConvert.SerializeObject(topics);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpPost("get-topic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourseById([FromBody] IntIdRequestModel intIdRequestModel)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var topics = await _problemTopicService.GetTopicById(intIdRequestModel.Id);
                basicResponse.Data = JsonConvert.SerializeObject(topics);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(TopicModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest($"Missing required fields.");

                var titleExists = await _problemTopicService.TitleExists(model.Title);
                if (titleExists)
                {
                    basicResponse.ErrorMessage = $"Topic {model.Title} already exists.";
                    basicResponse.Success = false;
                    return Conflict(basicResponse);
                }

                var topics = await _problemTopicService.AddTopic(Mapper.Map<Topic>(model));
                basicResponse.Data = topics;
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(TopicModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest($"Missing required fields.");

                await _problemTopicService.UpdateTopic(Mapper.Map<Topic>(model));
                basicResponse.Data = NoContent();
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(long Id)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                await _problemTopicService.DeleteTopic(Id);
                basicResponse.Data = NoContent();
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        #region Course dropdown list
        [HttpGet("topic-by-course-dropdown-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCoursesForDropdown(long courseId)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var topics = await _problemTopicService.GetTopicsByCourseId(courseId);
                basicResponse.Data = JsonConvert.SerializeObject(topics);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        #endregion
    }
}
