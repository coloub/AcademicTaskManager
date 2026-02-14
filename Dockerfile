# Multi-stage Dockerfile for .NET 10 Blazor Server Application
# Compatible with Render.com Docker deployment

# ============================================
# Stage 1: Build
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies (cached layer)
COPY ["AcademicTaskManager.csproj", "./"]
RUN dotnet restore "AcademicTaskManager.csproj"

# Copy project files and build
COPY . .
RUN dotnet build "AcademicTaskManager.csproj" -c Release -o /app/build

# ============================================
# Stage 2: Publish
# ============================================
FROM build AS publish
RUN dotnet publish "AcademicTaskManager.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ============================================
# Stage 3: Runtime
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Create non-root user for security
RUN useradd -m -u 1001 appuser && chown -R appuser /app
USER appuser

# Copy published application from publish stage
COPY --from=publish /app/publish .

# Expose port 5000 (default, overridden by PORT env var at runtime)
EXPOSE 5000

# Set environment variables
# PORT will be provided by Render.com at runtime
# ASPNETCORE_URLS is configured in Program.cs based on PORT variable
ENV ASPNETCORE_ENVIRONMENT=Production

# Entry point
ENTRYPOINT ["dotnet", "AcademicTaskManager.dll"]
