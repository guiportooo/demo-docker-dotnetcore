FROM microsoft/aspnetcore-build:2

WORKDIR /api

COPY DemoDotNetCore.WebApi.csproj .
RUN dotnet restore

COPY . .

RUN dotnet build

RUN chmod +x ./entrypoint.sh 
CMD /bin/bash ./entrypoint.sh