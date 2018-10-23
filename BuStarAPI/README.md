Before running the project you will have to create a file called appsettings.json
with the following content:

```json {
  "ConnectionStrings":
  {
    "DefaultConnection": "Filename=database.db",
    "WeatherAPIKey" : "Weather-API-Key"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
} 
```

You need to replace Weather-API-Key with a key that can be generated for free from here
https://openweathermap.org/price

## Installation and hosting 

Server advertises its services on port 5001 so it's essential to open the port on your firewall.

### Linux Arch based distros 
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

### Debian based distros
```bash
# Install dotnet-sdk
 wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
wget -q https://packages.microsoft.com/config/debian/9/prod.list
sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list
sudo apt-get update
sudo apt-get install dotnet-sdk-2.1

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
