using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Course;
using Coddinggurrus.Api.Models.Admin.Generic;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coddinggurrus.Api.Controllers.Admin.Tutorials
{
    public class CourseController : AdminController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _courseService = courseService;
        }
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery] ListingParameter listingParameter)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var courses = await _courseService.GetCourses(listingParameter);
                basicResponse.Data = JsonConvert.SerializeObject(courses);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpPost("get-course")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourseById([FromBody] IntIdRequestModel intIdRequestModel)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var course = await _courseService.GetCourseById(intIdRequestModel.Id);
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
        public async Task<IActionResult> Add(CourseModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest($"Missing required fields.");

                var titleExists = await _courseService.TitleExists(model.Title);
                if (titleExists)
                {
                    basicResponse.ErrorMessage = $"Course {model.Title} already exists.";
                    basicResponse.Success = false;
                    return Conflict(basicResponse);
                }

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

        #region Course dropdown list
        [HttpGet("course-dropdown-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCoursesForDropdown()
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var courses = await _courseService.GetAllCoursesForDropdown();
                basicResponse.Data = JsonConvert.SerializeObject(courses);
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
