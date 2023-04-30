using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using YelpRestaurantFinderComponent.Services;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddHealthChecks()
    .AddCheck<YelpApIConnectivityHealthCheck>(nameof(YelpApIConnectivityHealthCheck), HealthStatus.Unhealthy, default,
        TimeSpan.FromMinutes(1));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.MapHealthChecks("/healthcheck",
    new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();