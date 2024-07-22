FROM mcr.microsoft.com/dotnet/aspnet:8.0.7 AS base
WORKDIR /app

RUN adduser amorphie-contractuser --disabled-password --gecos "" && chown -R amorphie-contractuser:amorphie-contractuser /app
USER amorphie-contractuser


FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS build
WORKDIR /src
ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false

COPY ["./amorphie.contract/amorphie.contract.csproj", "."]
RUN dotnet restore "./amorphie.contract.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./amorphie.contract/amorphie.contract.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./amorphie.contract/amorphie.contract.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "amorphie.contract.dll"]
