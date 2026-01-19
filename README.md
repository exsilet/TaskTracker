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
3. Открыть https://localhost:5219/swagger/index.html

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

## 🔌 API Endpoints

| Метод | URL | Описание |
|-------|-----|----------|
| GET | `http://localhost:5219/api/tasks` | Получить все задачи |
| GET | `http://localhost:5219/api/tasks/{id}` | Получить задачу по ID |
| POST | `http://localhost:5219/api/tasks` | Создать задачу |
| PUT | `http://localhost:5219/api/tasks/{id}` | Обновить задачу |
| DELETE | `http://localhost:5219/api/tasks/{id}` | Удалить задачу |

## 📝 Пример запроса

**POST /api/tasks**
```json
{
  "title": "Изучить ASP.NET Core",
  "description": "Создать первый проект",
  "dueDate": "2024-12-31T00:00:00Z",
  "priority": 2
}
