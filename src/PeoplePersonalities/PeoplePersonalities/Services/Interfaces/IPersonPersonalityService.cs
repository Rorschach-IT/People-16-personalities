using PeoplePersonalities.Models;

namespace PeoplePersonalities.Services.Interfaces;

public interface IPersonPersonalityService
{
    Task<List<PersonPersonality>> GetAllAsync(string? type = null);
}