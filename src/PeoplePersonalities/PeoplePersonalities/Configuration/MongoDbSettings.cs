namespace PeoplePersonalities.Configuration;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PersonPersonalitiesCollectionName { get; set; } = null!;
}