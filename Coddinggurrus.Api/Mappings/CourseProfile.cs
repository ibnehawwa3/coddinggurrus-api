using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Course;
using Coddinggurrus.Core.Entities;

namespace Coddinggurrus.Api.Mappings
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseModel, Course>();
        }
    }
}
