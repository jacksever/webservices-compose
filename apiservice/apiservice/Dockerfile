FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80
COPY *.csproj ./
RUN dotnet restore "apiservice.csproj"
COPY . ./
RUN dotnet publish apiservice.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "apiservice.dll"]
