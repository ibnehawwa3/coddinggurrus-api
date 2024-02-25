using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Course;
using Coddinggurrus.Api.Models.Admin.Generic;
using Coddinggurrus.Api.Models.Admin.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coddinggurrus.Api.Controllers.Admin.Tutorials
{
    public class TopicController : AdminController
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _topicService = topicService;
        }
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery] ListingParameter listingParameter)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var courses = await _topicService.GetTopics(listingParameter);
                basicResponse.Data = JsonConvert.SerializeObject(courses);
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
                var course = await _topicService.GetTopicById(intIdRequestModel.Id);
                basicResponse.Data = JsonConvert.SerializeObject(course);
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

                var titleExists = await _topicService.TitleExists(model.Title);
                if (titleExists)
                {
                    basicResponse.ErrorMessage = $"Topic {model.Title} already exists.";
                    basicResponse.Success = false;
                    return Conflict(basicResponse);
                }

                var users = await _topicService.AddTopic(Mapper.Map<Topic>(model));
                basicResponse.Data = users;
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

                await _topicService.UpdateTopic(Mapper.Map<Topic>(model));
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
                await _topicService.DeleteTopic(Id);
                basicResponse.Data = NoContent();
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
    }
}
