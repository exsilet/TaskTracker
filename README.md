# Task Tracker API

Простой и понятный пример ASP.NET Core Web API с чистой архитектурой.

## 🏗️ Архитектура
- **Domain** - бизнес-сущности
- **Application** - DTO, сервисы, бизнес-логика
- **Infrastructure** - работа с БД (Entity Framework)
- **API** - контроллеры, точка входа

## 🚀 Быстрый старт
1. `docker-compose up -d` (PostgreSQL + Redis)
2. Запустить TaskTracker.API в Rider
3. Открыть https://localhost:7043/swagger

## 📁 Структура
TaskTracker/
├── TaskTracker.API/
├── TaskTracker.Application/
├── TaskTracker.Domain/
├── TaskTracker.Infrastructure/
├── docker-compose.yml
└── README.md


## 🔧 Технологии
- .NET 9, Entity Framework Core, PostgreSQL
- Docker, Redis, Swagger
- Clean Architecture, DTO, Dependency Injection

## 📝 Особенности
- Валидация DTO
- Глобальная обработка ошибок
- Сервисный слой
- Автоматическое создание БД