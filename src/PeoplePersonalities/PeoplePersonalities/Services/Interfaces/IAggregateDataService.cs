namespace PeoplePersonalities.Services.Interfaces;

public interface IAggregateDataService
{
    Task<List<string>> GetDistinctTypesAsync();
}