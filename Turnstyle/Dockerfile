FROM mcr.microsoft.com/dotnet/core/runtime:2.2
EXPOSE 5000

COPY app/bin/Release/netcoreapp2.2/publish/ app/

ENTRYPOINT ["dotnet", "app/Turnstyle.dll"]