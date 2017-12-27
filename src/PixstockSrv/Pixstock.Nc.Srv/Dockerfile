FROM microsoft/aspnetcore-build:2.0.0 AS build

# 事前に下記のコマンドでnugetパッケージを作成しておく
# dotnet restore --packages nuget-pack

WORKDIR /nugetpack
COPY .nuget .

WORKDIR /code
COPY src/ .
RUN dotnet restore --source /nugetpack
RUN dotnet publish --output /output --configuration Release --no-restore

FROM microsoft/aspnetcore:2.0.0
COPY --from=build /output /app

# Extention
COPY --from=build /output/Pixstock.Nc.Srv.Ext.FullBuild.dll /root/Pixstock.Srv/extention/Pixstock.Nc.Srv.Ext.FullBuild.pex

WORKDIR /app
ENTRYPOINT [ "dotnet", "Pixstock.Nc.Srv.dll" ]