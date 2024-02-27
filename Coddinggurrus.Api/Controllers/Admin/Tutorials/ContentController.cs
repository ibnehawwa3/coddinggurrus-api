using AutoMapper;
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
    public class ContentController : AdminController
    {
        private readonly IContentService _contentService;
        public ContentController(IContentService contentService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _contentService = contentService;
        }
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ListingParameter listingParameter)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var courses = await _contentService.GetContents(listingParameter);
                basicResponse.Data = JsonConvert.SerializeObject(courses);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpPost("get-content")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContentById([FromBody] IntIdRequestModel intIdRequestModel)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var course = await _contentService.GetContentById(intIdRequestModel.Id);
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
        public async Task<IActionResult> Add(ContentModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest($"Missing required fields.");

                var titleExists = await _contentService.TitleExists(model.Title);
                if (titleExists)
                {
                    basicResponse.ErrorMessage = $"Topic {model.Title} already exists.";
                    basicResponse.Success = false;
                    return Conflict(basicResponse);
                }

                var users = await _contentService.AddContent(Mapper.Map<Content>(model));
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
        public async Task<IActionResult> Update(ContentModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                    return BadRequest($"Missing required fields.");

                await _contentService.UpdateContent(Mapper.Map<Content>(model));
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
                await _contentService.DeleteContent(Id);
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
