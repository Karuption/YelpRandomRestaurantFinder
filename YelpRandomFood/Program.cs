using Azure.Identity;

using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using YelpRestaurantFinderComponent.Services;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

GraphQLHttpClientOptions gcloptions = new() {
    EndPoint = new(builder.Configuration.GetSection("Yelp")["Url"]),
    PreprocessRequest = (request, client) => {
        client.HttpClient.DefaultRequestHeaders.Authorization = new("bearer", Environment.GetEnvironmentVariable("YelpApiKey"));
        return Task.FromResult(request is GraphQLHttpRequest graphQLHttpRequest ? graphQLHttpRequest : new GraphQLHttpRequest(request));
    }
};
builder.Services.AddScoped<IGraphQLClient>(x => new GraphQLHttpClient(gcloptions, new SystemTextJsonSerializer(), new HttpClient()));

builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IYelpRetrievalService, YelpRetrievalService>();

builder.Services.AddLogging();
builder.Logging.AddConsole();

builder.Services.AddProtectedBrowserStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
