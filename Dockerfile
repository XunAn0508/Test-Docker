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
  
  # Change the current directory to the test folder
  cd /app/test

  # Install the .NET Core SDK (if not already installed)
  # You may need to adjust the version number accordingly
  wget -q https://dot.net/v1/dotnet-install.sh
  chmod +x dotnet-install.sh
  ./dotnet-install.sh --channel Current --install-dir /usr/share/dotnet --version 5.0.301

  # Run the tests using the dotnet test command
  dotnet test --logger "trx;LogFileName=/app/test-results.trx"

  # Capture the exit code
  TEST_EXIT_CODE=$?

  # Display the test results
  echo "Test results:"
  cat /app/test-results.trx

  # Exit with the test exit code
  exit $TEST_EXIT_CODE
done
EOF

ENTRYPOINT /app/run.sh
