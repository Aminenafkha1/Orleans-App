FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base  
WORKDIR /app
EXPOSE 8080
EXPOSE 443  
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /src
COPY ["Softylines.OrleansApp.Silo/Softylines.OrleansApp.Silo.csproj", "Softylines.OrleansApp.Silo/"]
COPY ["Softylines.OrleansApp.Grains/Softylines.OrleansApp.Grains.csproj", "Softylines.OrleansApp.Grains/"]
COPY ["Softylines.OrleansApp.Abstractions/Softylines.OrleansApp.Abstractions.csproj", "Softylines.OrleansApp.Abstractions/"]
 
 
RUN dotnet restore "Softylines.OrleansApp.Silo/Softylines.OrleansApp.Silo.csproj"
COPY . .
WORKDIR "/src/Softylines.OrleansApp.Silo"
RUN dotnet build "Softylines.OrleansApp.Silo.csproj" -c  Release -o /app/build

FROM build AS publish 
RUN dotnet publish "Softylines.OrleansApp.Silo.csproj" -c Release -o /app/publish  

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Softylines.OrleansApp.Silo.dll"]








 