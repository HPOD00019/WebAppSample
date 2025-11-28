FROM mcr.microsoft.com/dotnet/sdk:8.0

RUN apt-get update && apt-get install -y curl
RUN curl -fsSL https://deb.nodesource.com/setup_22.x | bash -
RUN apt-get install -y nodejs

WORKDIR /app

COPY Backend /app/Backend
COPY Client /app/Client

RUN dotnet restore Backend/WebApplicationSample/src/Microservices/GameEngineService/GameEngineService.Api/GameEngineService.Api.csproj

RUN dotnet build Backend/WebApplicationSample/src/Microservices/GameEngineService/GameEngineService.Api/GameEngineService.Api.csproj --configuration Debug  --no-restore 

WORKDIR /app/Client
RUN npm install

RUN npm run build

WORKDIR /app
RUN dotnet publish Backend/WebApplicationSample/src/Microservices/GameEngineService/GameEngineService.Api/GameEngineService.Api.csproj -c Debug  -o /app/publish

RUN mkdir -p /app/publish/wwwroot

RUN cp -r /app/Client/dist /app/publish/wwwroot/

EXPOSE 80 

WORKDIR /app/publish

#ENTRYPOINT ["dotnet", "GameEngineService.Api.dll"]

EXPOSE 5173

CMD bash -c "cd /app/Client && npm run dev -- --host 0.0.0.0 & cd /app/publish && dotnet GameEngineService.Api.dll"