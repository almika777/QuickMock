
![image](https://github.com/user-attachments/assets/4b97a2c3-9bef-4069-9f89-31e9463ce3ee)

Run as .Net application:
- cd QuickMock
- dotnet run

Run in docker container:
- docker build -t quickmock -f QuickMock/Dockerfile .
- docker run -p 34533:22789 quickmock
