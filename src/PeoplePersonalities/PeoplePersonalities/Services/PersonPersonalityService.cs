using MongoDB.Driver;
using PeoplePersonalities.Models;

namespace PeoplePersonalities.Services
{
    public interface IPersonPersonalityService
    {
        Task<List<PersonPersonality>> GetAllAsync(string? type = null);
    }

    public class PersonPersonalityService : IPersonPersonalityService
    {
        private readonly IMongoCollection<PersonPersonality> _collection;

        public PersonPersonalityService(IMongoClient client)
        {
            var database = client.GetDatabase("PeoplePersonalities");
            _collection = database.GetCollection<PersonPersonality>("PeoplePersonalities");
        }

        public async Task<List<PersonPersonality>> GetAllAsync(string? type = null)
        {
            var filter = string.IsNullOrWhiteSpace(type)
                ? FilterDefinition<PersonPersonality>.Empty
                : Builders<PersonPersonality>.Filter.Eq(x => x.Type, type);

            return await _collection.Find(filter).ToListAsync();
        }
    }
}