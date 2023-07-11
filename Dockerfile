# syntax=docker/dockerfile:1
FROM busybox:latest


# Add the test.txt file to the Docker image
COPY test.txt /app/test.txt

# Add the CompanyWebsite folder to the Docker image
COPY CompanyWebsite /app/CompanyWebsite

COPY --chmod=755 <<EOF /app/run.sh
#!/bin/sh
while true; do
  echo -ne "The time is now $(date +%T)\\r"
  sleep 1
done
EOF

# Use an appropriate base image for your project
FROM mcr.microsoft.com/dotnet/sdk:5.0

# Set the working directory
WORKDIR /app

# Copy the test project files to the container
COPY CompanyWebsite/8.1.0/aspnet-core/test/CompanyWebsite.Tests/CompanyWebsite.Tests.csproj .

# Restore the test project dependencies
RUN dotnet restore

# Copy the rest of the test project files
COPY CompanyWebsite/8.1.0/aspnet-core/test .

# Run the tests
CMD ["dotnet", "test", "--logger:trx"]
ENTRYPOINT /app/run.sh
