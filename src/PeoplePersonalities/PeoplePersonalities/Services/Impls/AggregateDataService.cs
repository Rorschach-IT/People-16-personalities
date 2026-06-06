using MongoDB.Driver;
using PeoplePersonalities.Models;
using PeoplePersonalities.Services.Interfaces;

namespace PeoplePersonalities.Services.Impls
{
    public class AggregateDataService : IAggregateDataService
    {
        private readonly IMongoCollection<PersonPersonality> _collection;

        public AggregateDataService(IMongoCollection<PersonPersonality> collection)
        {
            _collection = collection;
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