FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["src/MenuSaz.Identity.Api/MenuSaz.Identity.Api.csproj", "src/MenuSaz.Identity.Api/"]
COPY ["src/MenuSaz.Identity.Infrastructure/MenuSaz.Identity.Infrastructure.csproj", "src/MenuSaz.Identity.Infrastructure/"]
COPY ["src/MenuSaz.Identity.Persistence/MenuSaz.Identity.Persistence.csproj", "src/MenuSaz.Identity.Persistence/"]
COPY ["src/MenuSaz.Identity.Domain/MenuSaz.Identity.Domain.csproj", "src/MenuSaz.Identity.Domain/"]
COPY ["src/MenuSaz.Identity.Application/MenuSaz.Identity.Application.csproj", "src/MenuSaz.Identity.Application/"]

RUN dotnet nuget remove source nuget.org
RUN dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget-proxy
COPY . . 
WORKDIR "src/MenuSaz.Identity.Api"
RUN dotnet build "MenuSaz.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MenuSaz.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# allow weak certificates (certificate signed with SHA1)
# by downgrading OpenSSL security level from 2 to 1
RUN sed -i 's/SECLEVEL=2/SECLEVEL=1/g' /etc/ssl/openssl.cnf

ENTRYPOINT ["dotnet", "MenuSaz.Identity.Api.dll"]