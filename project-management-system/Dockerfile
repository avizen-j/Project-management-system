FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80

RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["project-management-system.csproj", ""]
RUN dotnet restore "./project-management-system.csproj"
COPY . .
#WORKDIR "/src/."
RUN dotnet build "project-management-system.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "project-management-system.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "project-management-system.dll"]