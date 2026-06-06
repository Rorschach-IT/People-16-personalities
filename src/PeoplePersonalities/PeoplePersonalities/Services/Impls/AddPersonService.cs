using MongoDB.Driver;
using PeoplePersonalities.Models;
using PeoplePersonalities.Services.Interfaces;
using System.Globalization;

namespace PeoplePersonalities.Services.Impls;

public class AddPersonService : IAddPersonService
{
    private readonly IMongoCollection<PersonPersonality> _collection;

    public AddPersonService(IMongoCollection<PersonPersonality> collection)
    {
        _collection = collection;
    }

    public async Task<AddPersonResult> AddPersonAsync(PersonPersonality person)
    {
        person.FirstName = CapitalizeAndTrim(person.FirstName);
        person.LastName = CapitalizeAndTrim(person.LastName);
        person.Type = person.Type?.Trim().ToUpperInvariant() ?? string.Empty;

        var exists = await ExistsAsync(person.FirstName, person.LastName);
        if (exists)
        {
            return AddPersonResult.Fail("Osoba o takim imieniu i nazwisku już istnieje w bazie.");
        }

        try
        {
            await _collection.InsertOneAsync(person);
            return AddPersonResult.Ok();
        }
        catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
        {
            return AddPersonResult.Fail("Osoba o takim imieniu i nazwisku już istnieje w bazie.");
        }
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

    private async Task<bool> ExistsAsync(string firstName, string lastName)
    {
        var filter = Builders<PersonPersonality>.Filter.And(
            Builders<PersonPersonality>.Filter.Eq(x => x.FirstName, firstName),
            Builders<PersonPersonality>.Filter.Eq(x => x.LastName, lastName)
        );

        return await _collection.Find(filter).AnyAsync();
    }

    private static string CapitalizeAndTrim(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        value = value.Trim();
        var culture = new CultureInfo("en-EN");

        if (value.Length == 1)
            return value.ToUpper(culture);

        return char.ToUpper(value[0], culture) + value.Substring(1).ToLower(culture);
    }
}