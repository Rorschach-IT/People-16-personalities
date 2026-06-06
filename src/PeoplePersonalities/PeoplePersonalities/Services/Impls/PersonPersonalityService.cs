using MongoDB.Driver;
using PeoplePersonalities.Models;
using PeoplePersonalities.Services.Interfaces;

namespace PeoplePersonalities.Services.Impls
{
    public class PersonPersonalityService : IPersonPersonalityService
    {
        private readonly IMongoCollection<PersonPersonality> _collection;

        public PersonPersonalityService(IMongoCollection<PersonPersonality> collection)
        {
            _collection = collection;
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