# Тестовое задание от компании MetaQuotes

### Задача: 
Разработать веб-приложение для получения координат пользователя по его IP-адресу и получения списка местоположений по названию города.

### Средства разработки:
- C#, ASP.NET MVC 4 или 5 версии
- MS Visual Studio
- HTML5, CSS3, JavaScript
- Без использования СУБД

### Требование к архитектуре и исходному коду
- Веб-приложение должно быть спроектировано и разработано с расчетом на 10 000 000 уникальных пользователей в день и от 100 000 000 запросов в день.
- Клиентская часть должна быть выполнена в виде Single Page Application
- Исходный код должен быть оформлен в едином стиле и содержать необходимые комментарии.
- Аккуратность исходного кода будет оцениваться наряду с функциональностью приложения.
- Для клиентского кода нет требований по минимально поддерживаемой версии браузера. Можно использовать возможности последних версий браузеров (Chrome, Firefox, IE11, Edge).
 
### Техническое описание приложения
- База данных хранится в файле geobase.dat, которые содержится в прикрепленном к письму архиве.
- База данных не будет изменятся она предназначена только для чтения.
- База данных имеет бинарный формат. В файле последовательно хранятся:

60 байт - заголовок

```csharp
int   version;           // версия база данных
sbyte name[32];          // название/префикс для базы данных
ulong timestamp;         // время создания базы данных
int   records;           // общее количество записей
uint  offset_ranges;     // смещение относительно начала файла до начала списка записей с геоинформацией
uint  offset_cities;     // смещение относительно начала файла до начала индекса с сортировкой по названию городов
uint  offset_locations;  // смещение относительно начала файла до начала списка записей о местоположении
```

20 байт * Header.records (количество записей) - cписок записей с информацией об интервалах IP адресов отсортированный по полям ip_from и ip_to

```csharp
ulong ip_from;           // начало диапозона IP адресов
ulong ip_to;             // конец диапозона IP адресов
uint location_index;     // индекс записи о местоположении
```

96 байт * Header.records (количество записей) - cписок записей с информацией о местоположении с координатами (долгота и широта)

```csharp
sbyte country[8];        // название страны (случайная строка с префиксом "cou_")
sbyte region[12];        // название области (случайная строка с префиксом "reg_")
sbyte postal[12];        // почтовый индекс (случайная строка с префиксом "pos_")
sbyte city[24];          // название города (случайная строка с префиксом "cit_")
sbyte organization[32];  // название организации (случайная строка с префиксом "org_")
float latitude;          // широта
float longitude;         // долгота
```

4 байта * Header.records (количество записей) - список индексов записей местоположения отсортированный по названию города

- База данных грузится полностью в память при старте приложения.
- Время загрузки базы в память должно не быть более 200 мс (для информации: есть решение, которое позволяет загрузить базу в память быстрее 30 мс).
- Необходимо реализовать быстрый поиск по загруженной базе по IP адресу и по точному совпадению названия города с учетом регистра.
- В приложении должны быть реализованы два метода HTTP API:

```
GET /ip/location?ip=123.234.432.321
GET /city/locations?city=cit_Gbqw4
ответ сервера на каждый из запросов должен быть представлен в формате JSON.
```

- Клиентская часть приложения должны быть выполнена в идеологии Single Page Application.
- Страница должна состоять из двух частей: в левой части меню переключения экранов, в правой части отображается выбранный экран.
- Клиентская часть должна реализовать два экрана: поиск гео-информации по IP, поиск списка местоположений по названию города.
- Экран поиска гео-информации содержит: поле для ввода IP адреса, кнопку "Искать" и область для вывода результата.
По нажатию кнопки "Искать" на сервер отправляется запрос GET /ip/location?ip=123.234.432.321
Обработанный ответ от сервера выводится в область вывода результатов.
- Экран поиска списка метоположений содержит: поле для ввода названия города, кнопку "Искать" и область для вывода результата.
По нажатию кнопки "Искать" на сервер отправляется запрос GET /city/locations?city=cit_Gbqw4
Обработанный ответ от сервера выводится в область вывода результатов.

## Использование

```csharp
DataBase database = DataBaseManager.Read(@"geobase.dat");
//---
Console.WriteLine($"Database loaded in {database.LoadTime} ms.");
//---
ISearchService searchService = new SearchService(database);
//---
IReadOnlyCollection<Location> locations = searchService.GetLocationsByCity("cit_Opyfu");
//---
foreach (Location location in locations)
{
    Console.WriteLine(location);
}
//---
Console.WriteLine();
//---
Location? ipLocation = searchService.GetLocationByIp(16287938);
if (ipLocation.HasValue)
{
    Console.WriteLine(ipLocation);
}
//---
Console.ReadKey();
```
