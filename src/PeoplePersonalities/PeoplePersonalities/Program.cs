using MongoDB.Bson;
using MongoDB.Driver;
using PeoplePersonalities.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient("mongodb://192.168.1.148:27017"));

builder.Services.AddScoped<IPersonPersonalityService, PersonPersonalityService>();
builder.Services.AddScoped<IAggregateDataService, AggregateDataService>();
builder.Services.AddScoped<IAddPersonService, AddPersonService>();

var app = builder.Build();

try
{
    var client = app.Services.GetRequiredService<IMongoClient>();
    var db = client.GetDatabase("PeoplePersonalities");

    await db.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));

    app.Logger.LogInformation("MongoDB connection OK. MongoDB connection OK. MongoDB connection OK. MongoDB connection OK. MongoDB connection OK.");
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