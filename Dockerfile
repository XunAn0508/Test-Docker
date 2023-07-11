# Use an appropriate base image for your project
FROM mcr.microsoft.com/dotnet/sdk:5.0

# Set the working directory
WORKDIR /app

# Copy the test project files to the container
COPY ./path/to/your/test/project.csproj .

# Restore the test project dependencies
RUN dotnet restore

# Copy the rest of the test project files
COPY ./path/to/your/test/ .

# Run the tests
CMD ["dotnet", "test", "--logger:trx"]
