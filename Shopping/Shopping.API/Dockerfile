FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR src
COPY Shopping.API/*.csproj Shopping.API/
COPY Shopping.Domain/*.csproj Shopping.Domain/
RUN dotnet restore Shopping.API/Shopping.API.csproj
COPY . .
RUN dotnet publish Shopping.API/Shopping.API.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Shopping.API.dll"]
