FROM mcr.microsoft.com/dotnet/sdk:latest AS build-env
WORKDIR /app

COPY YelpRandomFood/*.csproj YelpRandomFood/
COPY YelpRestaurantFinderComponent/*.csproj YelpRestaurantFinderComponent/
COPY *.sln ./
RUN dotnet restore

COPY * ./
RUN dotnet publish -c Release -o out --no-restore YelpRandomRestaurantFinder.sln

FROM mcr.microsoft.com/dotnet/aspnet:latest
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "YelpRandomRestaurantFinder.dll"]