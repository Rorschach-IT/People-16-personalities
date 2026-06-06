using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using PeoplePersonalities.Configuration;
using PeoplePersonalities.Models;
using PeoplePersonalities.Services.Impls;
using PeoplePersonalities.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));

// Singleton ConnectionString init
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Fetch database
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

// Fetch collection
builder.Services.AddScoped(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return database.GetCollection<PersonPersonality>(
        settings.PersonPersonalitiesCollectionName);
});

// Init app services
builder.Services.AddScoped<IPersonPersonalityService, PersonPersonalityService>();
builder.Services.AddScoped<IAggregateDataService, AggregateDataService>();
builder.Services.AddScoped<IAddPersonService, AddPersonService>();

var app = builder.Build();

// Init try catch database connection
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
    await db.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));

    app.Logger.LogInformation("MongoDB connection OK.");
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "MongoDB connection failed.");
    throw;
}

using (var scope = app.Services.CreateScope())
{
    var addPersonService = scope.ServiceProvider.GetRequiredService<IAddPersonService>();
    await addPersonService.EnsureIndexesAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
