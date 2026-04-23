FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY EStateBidMicroservice.sln ./
COPY Src/AuctionService/AuctionService.csproj ./Src/AuctionService/
COPY Src/SharedMicro/SharedMicro.csproj ./Src/SharedMicro/
RUN dotnet restore ./Src/AuctionService/AuctionService.csproj
COPY . .
RUN dotnet publish ./Src/AuctionService/AuctionService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet","AuctionService.dll" ]