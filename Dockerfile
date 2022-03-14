FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5001
      
FROM base AS final
ENTRYPOINT ["dotnet", "CICD Test.dll", "--urls", "http://*:5001"]