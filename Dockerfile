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

ENTRYPOINT /app/run.sh
