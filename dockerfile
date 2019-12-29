FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS builder
WORKDIR /app

COPY ./ ./
RUN dotnet publish src/Host/Api/ViajaNet.JobApplication.Host.Api.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS runner
WORKDIR /out

COPY --from=builder ./publish ./

EXPOSE 5000
ENTRYPOINT [ "dotnet", "ViajaNet.JobApplication.Host.Api.dll" ]
