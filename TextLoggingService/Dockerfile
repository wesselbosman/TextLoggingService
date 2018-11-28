FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["TextLoggingService/TextLoggingService.csproj", "TextLoggingService/"]
RUN dotnet restore "TextLoggingService/TextLoggingService.csproj"
COPY . .
WORKDIR "/src/TextLoggingService"
RUN dotnet build "TextLoggingService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TextLoggingService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TextLoggingService.dll"]