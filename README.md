# CourseDB
## Курсовая работа по предмету Базы данных

Улучшения:
 - разделить класс MainWindow и классы команд. Чтобы между ними был посредник (паттерн?) и их логика не была переплетена.
 - добавить к enum'ы базовый элемент "NONE". И, может, еще как улучшить их
 - вынести буквальную логику главного окна взаимодействия из MainWindow и оставить там только переключение элементов UI.
 - научиться применять DI (внедрение зависимостей)
 - разобраться в MVVM и добавить его
 - рефакторинг
 - внедрить сущности данных из БД
 - вынести всякие стоковые данные в отдельный файл и сделать конфиг подключения к БД
 - ! подумать про многопоточную модель
 - ! перенести эту же работу с ADO.NET на ASP.NET

### Общая задача
Магазин занимается продажей со склада товаров оптом и в розницу. При этом менеджеры следят за тем, чтобы на складе постоянно были товары в наличии. Товары закупаются у посредника по установленной цене.
В дальнейшем магазин продает их по другой цене. Продажи записываются в журнал продаж, где отмечается дата продажи, количество проданного товара и цена за единицу.
Полученные от разницы в покупке и продаже товара деньги магазин тратит на различные статьи расхода. Данные о расходах отмечаются в журнале расходов и характеризуются датой, суммой и самой статьей расхода.
В частности, каждый месяц магазин выплачивает работникам зарплату

### Условия касательно базы данных
1.	Контроль целостности данных, используя механизм связей
2.	Операции модификации групп данных и данных в связанных таблицах должны быть выполнены в рамках транзакций.
3.	Логика работы приложения должна контролироваться триггерами. В частности, должны быть реализованы следующие ограничения:
 - Не позволяет добавить в таблицу товаров товар, у которой не задана сумма
 - Не позволяет добавлять расход, с суммой большей заданной
 - Не позволяет изменять данные в таблице продаж задним числом от сегодняшней даты
4.	Все операции вычисления различных показателей (из требований к клиентскому приложению) должны реализовываться хранимыми процедурами.

### Условия касательно клиентского приложения
1.	Необходимо реализовать интерфейсы для ввода, модификации и удаления справочников:
 - Товаров;
 - Статей расходов.
2.	В главном окне приложения должны быть реализованы интерфейсы:
 - Журнала продаж с возможностью задавать товар, дату продажи, количество и цену за единицу.
 - Журнал расходов с возможностью задавать дату, сумму и статью расхода.
3.	Необходимо реализовать возможность просмотра оператором следующих показателей:
 - Прибыль магазина за заданный месяц.
 - Пять самых доходных товаров за заданный интервал дат.



# CourseDB
## Coursework in the subject of Databases

### General task
The store sells goods wholesale and retail from the warehouse. In this case, managers make sure that the warehouse is always in stock. Goods are purchased from an intermediary at a set price.
The store subsequently sells them at a different price. Sales are recorded in a sales log where the date of sale, the quantity sold and the price per unit are noted.
The money received from the difference in the purchase and sale of goods is spent by the store on various expense items. These expenses are recorded in the expense journal and are characterized by the date, amount, and the expense item itself.
For example, each month the store pays wages to employees

### Conditions regarding the database
1.	Control of data integrity using the linkage mechanism
2. modification operations of data groups and data in linked tables must be performed within transactions. 2.
3. The application logic must be controlled by triggers. In particular, the following restrictions must be implemented:
 - Does not allow adding an item to the goods table that does not have an amount specified
 - Does not allow to add an expense with an amount greater than the specified amount
 - Does not allow to change data in the sales table retroactively from today's date
4.	All operations of calculating various indicators (from the requirements to the client application) should be realized by stored procedures.

### Conditions regarding the client application
1.	It is necessary to realize interfaces for input, modification and deletion of directories:
 - Goods;
 - Expense items.
2.	In the main window of the application interfaces should be implemented:
 - Sales log with the ability to set the item, date of sale, quantity and unit price.
 - Expense log with the ability to set the date, amount and expense item.
3. it is necessary to realize the possibility of operator viewing of the following indicators:
 - Store profit for a given month.
 - The five most profitable products for a given date interval.
