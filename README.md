## Project Launch
### Docker hub:
 1. docker pull almirok/quickmock
### Docker:
 1. docker build -t quickmock -f QuickMock/Dockerfile .
 2. docker run -p your_free_port:22789 quickmock
### .NET:
 1. dotnet build
 2. dotnet run --project ./QuickMock/QuickMock.csproj
