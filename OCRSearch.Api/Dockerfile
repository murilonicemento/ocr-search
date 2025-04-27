FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

USER $APP_UID

WORKDIR /app

EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["OCRSearch.Api/OCRSearch.Api.csproj", "OCRSearch.Api/"]
COPY ["OCRSearch.Application/OCRSearch.Application.csproj", "OCRSearch.Application/"]
COPY ["OCRSearch.Infrastructure/OCRSearch.Infrastructure.csproj", "OCRSearch.Infrastructure/"]

RUN dotnet restore "OCRSearch.Api/OCRSearch.Api.csproj"

COPY . .

WORKDIR "/src/OCRSearch.Api"

RUN dotnet build "OCRSearch.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "OCRSearch.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app

USER root

RUN apt update && apt install -y --no-install-recommends \
    libc6-dev \
    libleptonica-dev \
    tesseract-ocr \
    tesseract-ocr-eng \
 && rm -rf /var/lib/apt/lists/* \
 && mkdir /app/x64 \
 && ln -s /lib/x86_64-linux-gnu/libleptonica.so /app/x64/libleptonica-1.82.0.so \
 && ln -s /lib/x86_64-linux-gnu/libdl.so.2 /lib/x86_64-linux-gnu/libdl.so \
 && ln -s /lib/x86_64-linux-gnu/libtesseract.so.5 /app/x64/libtesseract50.so

USER $APP_UID

COPY --from=publish /app/publish .

COPY "OCRSearch.Application/tessdata" /usr/share/tesseract-ocr/4.00/tessdata

ENTRYPOINT ["dotnet", "OCRSearch.Api.dll"]
