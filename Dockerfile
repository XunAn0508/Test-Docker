# syntax=docker/dockerfile:1
FROM busybox:latest

# Add the test.txt file to the Docker image
COPY test.txt /app/test.txt

# Add the CompanyWebsite folder to the Docker image
COPY CompanyWebsite /app/CompanyWebsite

# Add the test directory to the Docker image
COPY CompanyWebsite/8.1.0/aspnet-core/test /app/test

COPY --chmod=755 <<EOF /app/run.sh
#!/bin/sh
while true; do
  echo -ne "The time is now $(date +%T)\\r"
  # Run the tests
  echo "Running tests..."
  cd /app/test
  # Replace the command below with the command to run your tests
  # For example, if using the dotnet CLI to run tests:
  # dotnet test
  # If using a different testing framework, replace the command accordingly.
  sleep 1
done
EOF

ENTRYPOINT /app/run.sh
