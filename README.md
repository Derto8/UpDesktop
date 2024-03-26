<p align="center">
      <img src="https://i.ibb.co/pRDgdhZ/img.png" alt="ASP-Net Core" width="726">
</p>

<p align="center">
   <img src="https://img.shields.io/badge/.NETCore-3.1-brightgreen" alt=".NET Version 6">
   <img src="https://img.shields.io/badge/EF%20Version-5.0.17-blue" alt="EF Version 5.0.17">
</p>

## О проекте

Проект представляет из себя desktop-приложение для подачи и обработок заявок. В проекте было реализовано:
- Система авторизации пользователей, разделение ролей на "Пользователя","Менеджера" и "Работника".
- База данных, содержащая в таблицу Заявок, Пользователей и Историй.
- Менеджер может добавлять/изменять/удалять записи из базы данных находясь в приложении, смотреть статистику.
- Пользователь может подавать заявку.
- Работник может изменять статус завявки, обрабатывать её, генерировать pdf - отчёт по проделанной работе.

## Установка проекта

- Клонировать репозиторий на ваш накопитель.
- Зайти в файл `dbSettings.json`, в строке `DefaultConnection` изменить атрибут `Database=`, на название вашей базы данных. Если ваша база данных находится на локальном сервере, то в атрибуте `Server=` укажите ip вашего сервера. В случае, ничего не изменять в файле `dbSettings.json`, при запуске проекта создасться локальная база данных под названием `DBElectronicsShop` на устройстве, с которого запущен проект.
- В верхней панели среды разработки `Visual Studio` зайти в `Вид`>`Другие окна`>`Консоль диспетчера пакетов`, далее прописать команду `Update-Database`. Можно запускать проект!
- Если ваша база данных не находится на сервере то, чтобы посмотреть данные в базе данных, нужно зайти в `Вид`>`Обозреватель объектов SQL сервер`, в списке найти название вашей базы данных.

## Developer

- [Николай Полозов](https://github.com/Derto8)
