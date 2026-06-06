using PeoplePersonalities.Models;

namespace PeoplePersonalities.Services.Interfaces;

public interface IAddPersonService
{
    Task<AddPersonResult> AddPersonAsync(PersonPersonality person);
    Task EnsureIndexesAsync();
}

public class AddPersonResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }

    public static AddPersonResult Ok() => new() { Success = true };
    public static AddPersonResult Fail(string message) => new() { Success = false, ErrorMessage = message };
}