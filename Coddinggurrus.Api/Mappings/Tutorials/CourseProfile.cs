using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Course;
using Coddinggurrus.Core.Dto.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;

namespace Coddinggurrus.Api.Mappings.Tutorials
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseModel, Course>();
            CreateMap<Course, CourseDto>();
        }
    }
}
