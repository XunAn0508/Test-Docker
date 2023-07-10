FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/CompanyWebsite.Web.Host/CompanyWebsite.Web.Host.csproj", "src/CompanyWebsite.Web.Host/"]
COPY ["src/CompanyWebsite.Web.Core/CompanyWebsite.Web.Core.csproj", "src/CompanyWebsite.Web.Core/"]
COPY ["src/CompanyWebsite.Application/CompanyWebsite.Application.csproj", "src/CompanyWebsite.Application/"]
COPY ["src/CompanyWebsite.Core/CompanyWebsite.Core.csproj", "src/CompanyWebsite.Core/"]
COPY ["src/CompanyWebsite.EntityFrameworkCore/CompanyWebsite.EntityFrameworkCore.csproj", "src/CompanyWebsite.EntityFrameworkCore/"]
WORKDIR "/src/src/CompanyWebsite.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/CompanyWebsite.Web.Host", "src/CompanyWebsite.Web.Host"]
COPY ["src/CompanyWebsite.Web.Core", "src/CompanyWebsite.Web.Core"]
COPY ["src/CompanyWebsite.Application", "src/CompanyWebsite.Application"]
COPY ["src/CompanyWebsite.Core", "src/CompanyWebsite.Core"]
COPY ["src/CompanyWebsite.EntityFrameworkCore", "src/CompanyWebsite.EntityFrameworkCore"]
WORKDIR "/src/src/CompanyWebsite.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "CompanyWebsite.Web.Host.dll"]
