using MongoDB.Driver;
using PeoplePersonalities.Models;

namespace PeoplePersonalities.Services
{
    public interface IAddPersonService
    {
        Task<bool> ExistsAsync(string firstName, string lastName);
        Task AddAsync(PersonPersonality person);
        Task EnsureIndexesAsync();
    }

    public class AddPersonService : IAddPersonService
    {
        private readonly IMongoCollection<PersonPersonality> _collection;

        public AddPersonService(IMongoClient client)
        {
            var database = client.GetDatabase("PeoplePersonalities");
            _collection = database.GetCollection<PersonPersonality>("PeoplePersonalities");
        }

        public async Task<bool> ExistsAsync(string firstName, string lastName)
        {
            var filter = Builders<PersonPersonality>.Filter.And(
                Builders<PersonPersonality>.Filter.Eq(x => x.FirstName, firstName),
                Builders<PersonPersonality>.Filter.Eq(x => x.LastName, lastName)
            );

            return await _collection.Find(filter).AnyAsync();
        }

        public async Task AddAsync(PersonPersonality person)
        {
            await _collection.InsertOneAsync(person);
        }

        public async Task EnsureIndexesAsync()
        {
            var indexKeys = Builders<PersonPersonality>.IndexKeys
                .Ascending(x => x.FirstName)
                .Ascending(x => x.LastName);

            var indexOptions = new CreateIndexOptions
            {
                Unique = true,
                Name = "UX_FirstName_LastName"
            };

            var model = new CreateIndexModel<PersonPersonality>(indexKeys, indexOptions);

            await _collection.Indexes.CreateOneAsync(model);
        }
    }
}