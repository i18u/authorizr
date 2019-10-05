FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy our project/sln skeleton
COPY ./src/i18u.Authorizr.Core/i18u.Authorizr.Core.csproj ./i18u.Authorizr.Core/
COPY ./src/i18u.Authorizr.Web/i18u.Authorizr.Web.csproj ./i18u.Authorizr.Web/
COPY ./src/i18u.Authorizr.sln ./
COPY ./src/NuGet.config ./

# Cache our dotnet restore step
RUN dotnet restore ./i18u.Authorizr.Web/i18u.Authorizr.Web.csproj

# Copy all our remaining content
COPY ./src/i18u.Authorizr.Core/ ./i18u.Authorizr.Core/
COPY ./src/i18u.Authorizr.Web/ ./i18u.Authorizr.Web/

# Run our publish without a restore
RUN dotnet publish ./i18u.Authorizr.Web/i18u.Authorizr.Web.csproj -c Release -o out --no-restore

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app

COPY --from=build-env /app/i18u.Authorizr.Web/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "i18u.Authorizr.Web.dll"]
