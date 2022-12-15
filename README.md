# BlogApi (ASP.NET MVC WebAPI)
<h2>Задание (back-end)</h2>
<b>Задание:</b>
<p>Реализовать блог с возможностью комментировать и лайкать посты. Пользователь выбирает
интересные ему темы (теги ) и получает новостную ленту по выбранным темам.</p>
<b>Функционал:</b>
<ol>
<li>Модели: пользователь, блог, тег, комментарий и др.</li>
<li>Авторизация упрощенная.</li>
<li>Графики: количество постов по дате, по пользователю, по тегам и др.</li>
</ol>
<b>Требования:</b>
<ol>
<li>Приложение представляет собой WebApi сервис.</li>
<li>Хранение данных происходит в БД. Возможно использование SQLite, PostgreSQL, MySql. </li>
<li>Доступ "сервера" к данным БД происходит выполнением прямых SQL команд без использования какой-либо ORM.</li>
<li>Для тестирования API можно использовать Postman или аналогичные инструменты.</li>
</ol>

<h2>Реализовано:</h2>
<ol>
<li>Данные хранятся в базе SQLite. См. <b>blog.db</b>.</li>
<li>Аутентификация и авторизация осуществляется на основе JWT-токена. В базе прописаны 4 пользователя с 3 ролями.
Для доступа необходимо ввести логин (UserName из таблицы Users: <b>oleg, dima, alex, ivan</b>) и пароль (<b>Qwerty123</b>). Пароль для всех пользователей одинаковый. В базе храниться хеш пароля с солью. </li>
<li>Использована трехуровневая архитектура. На уровне DAL использованы паттерны Repository и UnitOfWork. Передача моделей между уровнями осуществляется с помощью AutoMapper.</li>
<li>Обработка исключений осуществляется с помощью фильтра GlobalExeptionFilter.</li>
<li>Для тестирования добавлен Swagger.</li>
</ol>
