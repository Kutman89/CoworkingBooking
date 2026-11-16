using DotNetEnv;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class DatabaseConnectionTests
{
    [Fact]
    public async Task CanConnectToDatabase()
    {
        // Загружаем .env
        Env.Load();

        // Получаем строку подключения
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        Assert.False(
            string.IsNullOrWhiteSpace(connectionString),
            "ConnectionStrings__DefaultConnection не найдена в .env"
        );

        // Создаём DbContext
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        await using var context = new AppDbContext(options);

        // Проверяем подключение
        var canConnect = await context.Database.CanConnectAsync();

        Assert.True(canConnect, "Не удалось подключиться к PostgreSQL");
    }
}