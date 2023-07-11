# Use an appropriate base image for your project
FROM mcr.microsoft.com/dotnet/sdk:5.0

# Set the working directory
WORKDIR /app

# Copy the test project files to the container
COPY CompanyWebsite/8.1.0/aspnet-core/test/CompanyWebsite.Tests/CompanyWebsite.Tests.csproj .

# Restore the test project dependencies
RUN dotnet restore

# Copy the rest of the test project files
COPY CompanyWebsite/8.1.0/aspnet-core/test/CompanyWebsite.Tests/CompanyWebsite.Tests.csproj .

# Run the tests
CMD ["dotnet", "test", "--logger:trx"]
