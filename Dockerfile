

FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

COPY . ./

RUN dotnet restore

EXPOSE 8080

CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080"]