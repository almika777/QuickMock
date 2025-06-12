Run as .Net application:
- cd QuickMock
- dotnet run

Run in docker container:
- docker build -t quickmock -f QuickMock/Dockerfile .
- docker run -p 34533:22789 quickmock
