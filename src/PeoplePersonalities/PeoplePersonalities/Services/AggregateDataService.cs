using MongoDB.Driver;
using PeoplePersonalities.Models;

namespace PeoplePersonalities.Services
{
    public interface IAggregateDataService
    {
        Task<List<string>> GetDistinctTypesAsync();
    }

    public class AggregateDataService : IAggregateDataService
    {
        private readonly IMongoCollection<PersonPersonality> _collection;

        public AggregateDataService(IMongoClient client)
        {
            var database = client.GetDatabase("PeoplePersonalities");
            _collection = database.GetCollection<PersonPersonality>("PeoplePersonalitiesMocks");
        }

        public async Task<List<string>> GetDistinctTypesAsync()
        {
            var types = await _collection
                .Distinct<string>("Type", FilterDefinition<PersonPersonality>.Empty)
                .ToListAsync();

            return types
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .OrderBy(x => x)
                .ToList();
        }
    }
}