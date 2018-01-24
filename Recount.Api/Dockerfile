FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY Recount.Api/Recount.Api.csproj Recount.Api/
RUN dotnet restore
COPY . .
WORKDIR /src/Recount.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY Recount.Api/app.yml app.yml
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Recount.Api.dll"]
