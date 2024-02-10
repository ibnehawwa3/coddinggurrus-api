using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Course;
using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;

namespace Coddinggurrus.Api.Controllers.Admin
{
    public class CourseController : AdminController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _courseService= courseService;
        }
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList(int pageNo, int pageSize, string searchText = "")
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var users = await _courseService.GetCourses(pageNo, pageSize, searchText);
                basicResponse.Data = users;
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CourseModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest($"Missing required fields.");

                var titleExists = await _courseService.TitleExists(model.Title);
                if (titleExists) return BadRequest($"Course {model.Title} already exists.");

                var users = await _courseService.AddCourse(Mapper.Map<Course>(model));
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
        public async Task<IActionResult> UpdateCourse(CourseModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest($"Missing required fields.");

                await _courseService.UpdateCourse(Mapper.Map<Course>(model));
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
                await _courseService.DeleteCourse(Id);
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
