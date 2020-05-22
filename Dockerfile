FROM debian AS libgdipluscompile
ENV DEBIAN_FRONTEND noninteractive
RUN apt-get update && \
    apt-get -y install gcc mono-mcs && \
    rm -rf /var/lib/apt/lists/*
COPY ./libgdiplus_compile.sh .
RUN chmod +x ./libgdiplus_compile.sh 
RUN ./libgdiplus_compile.sh

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
COPY ./setupAspose.sh .
RUN chmod +x ./setupAspose.sh 
RUN ./setupAspose.sh
# copy modified libgdiplus
COPY --from=libgdipluscompile libgdiplus/mono/lib/libgdiplus.so ./usr/lib


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY libgdiplus-limit.csproj .
RUN dotnet restore libgdiplus-limit.csproj
COPY . .
RUN dotnet build libgdiplus-limit.csproj -c Release -o /app


FROM build AS publish
RUN dotnet publish libgdiplus-limit.csproj -c Release -o /app


FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "libgdiplus-limit.dll"]

