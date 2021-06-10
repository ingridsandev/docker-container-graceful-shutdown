FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine3.13 AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.12 AS build
COPY ./Clean.Shut.Down.Web.Api.sln ./Clean.Shut.Down.Web.Api.sln
COPY ./src/Clean.Shut.Down.Web.Api/*.csproj ./src/Clean.Shut.Down.Web.Api/
RUN dotnet restore "/src/Clean.Shut.Down.Web.Api/Clean.Shut.Down.Web.Api.csproj"

COPY ./src/Clean.Shut.Down.Web.Api/. ./Clean.Shut.Down.Web.Api/

COPY . . 

RUN dotnet build "/src/Clean.Shut.Down.Web.Api/Clean.Shut.Down.Web.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore "/src/Clean.Shut.Down.Web.Api/Clean.Shut.Down.Web.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Clean.Shut.Down.Web.Api.dll"]