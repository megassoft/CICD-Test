FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443
EXPOSE 5001
#ENV ASPNETCORE_HTTPS_PORT=5001
#ENV ASPNETCORE_URLS "http://*:5000;https://*:5001"
ENV ASPNETCORE_ENVIRONMENT "Production"

 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CICD Test/CICD Test.csproj", "CICD Test/"]
RUN dotnet restore "CICD Test/CICD Test.csproj"
COPY . .
WORKDIR "/src/CICD Test"
RUN dotnet build "CICD Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CICD Test.csproj" -c Release -o /app/publish
      
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CICD Test.dll", "--urls", "http://*:5001"]
