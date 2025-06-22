QuickMock - a simple web server for serving text content. Often, during frontend development, you need to test how your application handles responses from the server, and QuickMock can easily and quickly solve this task.

Run as .Net application:
- cd QuickMock
- dotnet run

Run in docker container:
- docker build -t quickmock -f QuickMock/Dockerfile .
- docker run -p 34533:22789 quickmock
