## Installation and hosting 
### Linux Arch based systems 
```bash
# Install dependencies
sudo pacman -S dotnet-sdk

# Install a tool to generate the certificate
dotnet tool install --global dotnet-dev-certs

# Fetch project packages
dotnet restore

# Export path (works only in current session)
export DOTNET_ROOT="/opt/dotnet/"

# Generate certificates
dotnet dev-certs https

# Start the API server
dotnet run
```

