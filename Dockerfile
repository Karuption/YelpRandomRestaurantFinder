FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY YelpRandomFood/*.csproj YelpRandomFood/
COPY YelpRestaurantFinderComponent/*.csproj YelpRestaurantFinderComponent/
COPY *.sln .

FROM build-env AS restore
RUN dotnet restore YelpRandomRestaurantFinder.sln

FROM restore AS src
COPY . .

FROM src AS build
RUN dotnet publish -c Release -o out YelpRandomRestaurantFinder.sln

FROM mcr.microsoft.com/dotnet/aspnet:latest
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "YelpRandomRestaurantFinder.dll"]