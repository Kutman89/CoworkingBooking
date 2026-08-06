# CoworkingBooking

Краткое описание
- Минимальный backend для управления комнатами коворкинга (CRUD).
- Стек: .NET 10, ASP.NET Core, EF Core 10, PostgreSQL (Npgsql).
- Проект разделён на слои: `Web`, `Application`, `Domain`, `Infrastructure`.

Текущее состояние (на данный момент)
- Реализованы сущности `Room` и `Booking` (основная логика — для `Room`).
- Полный CRUD для `Room` через REST API:
  - создание, получение списка, получение по Id, обновление, логическое удаление.
- В `Infrastructure` есть миграция `InitialCreate` для таблицы `Rooms`.
- Swagger включён (в режимe разработки).
- Валидация запросов через `DataAnnotations`.
- Авторизации/аутентификации нет.
- Бронирования (`Booking`) пока только модель — контроллер/сервис для бронирований не реализованы.

Требования
- .NET 10 SDK
- PostgreSQL (или строка подключения к рабочей БД)

Быстрый старт (PowerShell)
1. Установить строку подключения (пример):
   $env:ConnectionStrings__DefaultConnection = "Host=localhost;Database=coworking;Username=postgres;Password=secret"
2. Перейти в корень репозитория и восстановить пакеты:
   dotnet restore
3. Применить миграции:
   dotnet ef database update --project src/Infrastructure --startup-project src/Web
4. Запустить Web-приложение:
   dotnet run --project src/Web

Запуск из Visual Studio
- Открыть решение `CoworkingBooking.slnx`.
- Установить `src/Web` как стартовый проект и запустить (F5). Swagger UI доступен в режиме разработки.

API (основные эндпоинты)
- GET  /api/test — health / тест
- GET  /api/rooms — получить список активных комнат
- GET  /api/rooms/{id} — получить комнату по Id
- POST /api/rooms — создать комнату
  Пример тела (JSON):
  {
    "name": "Зал переговоров A",
    "description": "Оборудованная комната на 8 человек",
    "capacity": 8,
    "floor": 2,
    "type": 1-3
  }
  Где `type` — значение из `Domain.Enums.RoomType`.
- PUT  /api/rooms/{id} — обновить комнату (использует ту же модель что и POST)
- DELETE /api/rooms/{id} — деактивировать комнату (логическое удаление)

Структура проекта (ключевые папки)
- `src/Web` — API, контроллеры, запуск приложения.
- `src/Application` — DTO, интерфейсы, бизнес-сервисы (например `RoomService`).
- `src/Domain` — сущности и перечисления (`Room`, `Booking`, `RoomType`, `BookingStatus`).
- `src/Infrastructure` — EF Core `AppDbContext`, репозитории, миграции.

Где смотреть миграции
- `src/Infrastructure/Migrations` — есть миграция создания таблицы `Rooms`.

Планы / TODO
- Реализовать API/сервисы для `Booking`.
- Добавить интеграционные/юнит-тесты.
- Конфигурация окружений (appsettings) и CI/CD.
- Авторизация/аутентификация (если потребуется).

Известные замечания
- Отсутствуют тесты.
- Отсутствует механизм аутентификации и управления пользователями.
- `Booking` пока только модель — бизнес-логика не реализована.
