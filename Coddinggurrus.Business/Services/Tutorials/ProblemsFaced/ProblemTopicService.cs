using AutoMapper;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.ProblemsFaced;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.ProblemsFaced;
using Coddinggurrus.Core.Models.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials.ProblemsFaced
{
    public class ProblemTopicService : BaseService, IProblemTopicService
    {
        private readonly IProblemTopicRepository _problemTopicRepository;
        public ProblemTopicService(IProblemTopicRepository problemTopicRepository, IConfiguration config, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _problemTopicRepository = problemTopicRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public async Task<int> AddTopic(Topic topic)
        {
            return await _problemTopicRepository.AddTopic(topic);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTopic(long Id)
        {
            return await _problemTopicRepository.DeleteTopic(Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Topic> GetTopicById(long id)
        {
            return await _problemTopicRepository.GetTopicById(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listingParameter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TopicCount>> GetTopics(ListingParameter listingParameter)
        {
            return await _problemTopicRepository.GetTopics(listingParameter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<DropdownListItems>> GetTopicsByCourseId(long courseId)
        {
            return await _problemTopicRepository.GetTopicsByCourseId(courseId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<bool> TitleExists(string title)
        {
            var exists = await _problemTopicRepository.TitleExists(title);
            return exists;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTopic(Topic model)
        {
            return await _problemTopicRepository.UpdateTopic(model);
        }
    }
}
