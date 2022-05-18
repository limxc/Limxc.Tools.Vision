#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

#see https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-ubuntu#1804
#FROM ubuntu:18.04 AS base
#RUN apt-get update \
#    && apt-get -y install wget
#RUN wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
#    && dpkg -i packages-microsoft-prod.deb \
#    && rm packages-microsoft-prod.deb
#RUN apt-get update \
#    && apt-get -y install -y apt-transport-https \
#    && apt-get update \
#    && apt-get -y install -y aspnetcore-runtime-6.0

FROM limxc/aspnetcore6_ubuntu1804 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src 
COPY ./src .
RUN dotnet publish "./Limxc.Tools.Vision.HttpApiHost/Limxc.Tools.Vision.HttpApiHost.csproj" -c Release -r ubuntu.18.04-x64 -o /app

FROM base AS final
WORKDIR /app
EXPOSE 5000
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Limxc.Tools.Vision.HttpApiHost.dll"]