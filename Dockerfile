FROM mcr.microsoft.com/dotnet/samples:aspnetapp

COPY bin/Release/net50/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Commander.dll"]