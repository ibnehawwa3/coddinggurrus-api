﻿using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Models.Generic;

namespace Coddinggurrus.Core.Interfaces.Repositories.Tutorials
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCourses(ListingParameter listingParameter);
        Task<int> AddCourse(Course course);
        Task<bool> TitleExists(string title);
        Task<bool> UpdateCourse(Course model);
        Task<bool> DeleteCourse(long Id);
        Task<Course> GetCourseById(long id);
        Task<IEnumerable<DropdownListItems>> GetAllCoursesForDropdown();

    }
}
