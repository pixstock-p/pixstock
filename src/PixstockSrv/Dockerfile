FROM microsoft/aspnetcore-build:2.0.0 AS build

# 事前に下記のコマンドでnugetパッケージを作成しておく
# dotnet restore --packages nuget-pack

ARG NUGETPACKAGE

RUN echo $NUGETPACKAGE

WORKDIR /nugetpack
COPY .nuget .

### DEBUG
#WORKDIR /nugetpack
#RUN ls -al

WORKDIR /code
COPY src/ .
RUN dotnet restore --source /nugetpack
RUN dotnet publish --output /output --configuration Release --no-restore

FROM microsoft/aspnetcore:2.0.0
COPY --from=build /output /app
WORKDIR /app
ENTRYPOINT [ "dotnet", "Pixstock.Nc.Srv.dll" ]