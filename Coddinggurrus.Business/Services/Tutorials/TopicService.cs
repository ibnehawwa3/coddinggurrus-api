
using AutoMapper;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Core.Models.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials
{
    public class TopicService : BaseService, ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        public TopicService(ITopicRepository topicRepository, IConfiguration config, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _topicRepository = topicRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public async Task<int> AddTopic(Topic topic)
        {
            return await _topicRepository.AddTopic(topic);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTopic(long Id)
        {
            return await _topicRepository.DeleteTopic(Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Topic> GetTopicById(long id)
        {
            return await _topicRepository.GetTopicById(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listingParameter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TopicCount>> GetTopics(ListingParameter listingParameter)
        {
            return await _topicRepository.GetTopics(listingParameter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<DropdownListItems>> GetTopicsByCourseId(long courseId)
        {
            return await _topicRepository.GetTopicsByCourseId(courseId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<bool> TitleExists(string title)
        {
            var exists = await _topicRepository.TitleExists(title);
            return exists;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTopic(Topic model)
        {
            return await _topicRepository.UpdateTopic(model);
        }
    }
}
