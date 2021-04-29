FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["HR.ATS.WebAPI/HR.ATS.WebAPI.csproj", "HR.ATS.WebAPI/"]
RUN dotnet restore "HR.ATS.WebAPI/HR.ATS.WebAPI.csproj"
COPY . .
WORKDIR "/src/HR.ATS.WebAPI"
RUN dotnet build "HR.ATS.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR.ATS.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR.ATS.WebAPI.dll"]
