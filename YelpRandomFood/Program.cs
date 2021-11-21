using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using YelpRandomRestaurantFinder.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

GraphQLHttpClientOptions gcloptions = new () {
    EndPoint = new(builder.Configuration.GetSection("Yelp")["Url"]),
    PreprocessRequest = (request, client) => {
        client.HttpClient.DefaultRequestHeaders.Authorization = new("bearer", builder.Configuration["Yelp:ApiKey"]);
        return Task.FromResult(request is GraphQLHttpRequest graphQLHttpRequest ? graphQLHttpRequest : new GraphQLHttpRequest(request));
    }
};
builder.Services.AddScoped<IGraphQLClient>(x => new GraphQLHttpClient( gcloptions, new SystemTextJsonSerializer(), new HttpClient()));

builder.Services.AddScoped<ILocationService, LocationService>();

builder.Services.AddLogging();
builder.Logging.AddConsole();

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
