#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TestAplication/TestAplication.csproj", "TestAplication/"]
COPY ["TestAplication.Repository/TestAplication.Repository.csproj", "TestAplication.Repository/"]
COPY ["TestAplication.EntityFramework/TestAplication.EntityFramework.csproj", "TestAplication.EntityFramework/"]
RUN dotnet restore "TestAplication/TestAplication.csproj"
COPY . .
WORKDIR "/src/TestAplication"
RUN dotnet build "TestAplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestAplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -fs /usr/share/zoneinfo/America/Lima /etc/localtime
RUN dpkg-reconfigure --frontend noninteractive tzdata
ENTRYPOINT ["dotnet", "TestAplication.dll"]