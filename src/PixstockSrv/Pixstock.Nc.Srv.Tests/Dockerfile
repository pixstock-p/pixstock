FROM microsoft/dotnet:2.0.0-sdk

# 事前に下記のコマンドでnugetパッケージを作成しておく
# dotnet restore --packages nuget-pack

WORKDIR /nugetpack
COPY .nuget .

### DEBUG
#WORKDIR /nugetpack
#RUN ls -al

WORKDIR /code
COPY src/ .
RUN dotnet restore --source /nugetpack
RUN dotnet build --no-restore

ENTRYPOINT [ "dotnet", "test", "PixstockSrv/Pixstock.Nc.Srv.Tests/Pixstock.Nc.Srv.Tests.csproj", "--no-build", "--no-restore" ]
