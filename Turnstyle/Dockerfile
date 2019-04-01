FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["Turnstyle/Turnstyle.csproj", "Turnstyle/"]
RUN dotnet restore "Turnstyle/Turnstyle.csproj"
COPY . .
WORKDIR "/src/Turnstyle"
RUN dotnet build "Turnstyle.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Turnstyle.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Turnstyle.dll"]