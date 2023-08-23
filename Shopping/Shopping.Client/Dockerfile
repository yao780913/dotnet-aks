FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR src
COPY Shopping.Client/*.csproj Shopping.Client/
COPY Shopping.Domain/*.csproj Shopping.Domain/
RUN dotnet restore Shopping.Client/Shopping.Client.csproj
COPY . .
RUN dotnet publish Shopping.Client/Shopping.Client.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Shopping.Client.dll"]