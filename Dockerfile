FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
EXPOSE 80

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

RUN apt update 
RUN apt install --yes curl
HEALTHCHECK --interval=5m --timeout=10s --start-period=1s --retries=3 \
	CMD curl --fail http://localhost/healthcheck | grep -E '^{\"status\":\"Healthy\",' || exit 1
ENTRYPOINT ["dotnet", "YelpRandomRestaurantFinder.dll"]