go
create database ShopMarekMichura
go
use ShopMarekMichura
go
--lista adresowa
create table Addresses(
Address_id int primary key identity(1,1),
Address_country nchar(25) not null,
Address_city nchar(50) not null,
Address_street nchar(50) not null,
Address_building_number nchar(10) not null,
Address_zip_code nchar(10),
constraint UC_addres unique (Address_country, Address_city, Address_street, Address_building_number, Address_zip_code)
)
create index I_Addresses on Addresses(Address_id)

--typy urzytkownikow
create table User_groups(
User_group_id int primary key identity(1,1),
User_group_name nchar(25)
)

--lista urzytkownikow
create table Users(
[User_id] int primary key identity(1,1),
[User_name] nchar(25) NOT NULL,
User_password nchar(25) NOT NULL,
User_group_id int NOT NULL,
User_active bit default(1) not null
)
alter table Users--polaczenie z tabela User_group
add constraint FK_Users__User_groups foreign key(User_group_id)
references User_groups(User_group_id)
on delete no action
on update cascade
create index I_User on Users([User_id])--index

--dane urzytkownikow
create table Users_data(
[User_id] int primary key not null,
User_first_name nchar(25) not null,
User_second_name nchar(25),
User_family_name nchar(25) not null
)
alter table Users_data--polaczenie z tabela Users
add constraint FK_Users_data__Users foreign key([User_id])
references Users([User_id])

--adresy urzytkownikow
create table User_Addresses(
User_Address_id int primary key identity(1,1),
[User_id] int not null,
Address_id int not null
)
alter table User_Addresses--polaczenie z tabela Users
add constraint FK_User_Addresses__Users foreign key([User_id])
references Users([User_id])
on delete no action
on update cascade
alter table User_Addresses
add constraint FK_User_Addresses__Addresses foreign key(Address_id)
references Addresses(Address_id)
on delete no action
on update cascade

--lista upowarznionych do sprzedazy
create table Worker_sellers(
[User_id] int primary key not null
)
alter table Worker_sellers
add constraint FK_Worker_seller__Users foreign key([User_id])
references Users([User_id])
on delete no action
on update cascade
create index I_Worker_sellers on Worker_sellers([User_id])

--lista upowarznionych do obslugi magazynow
create table Worker_storekeepers(
[User_id] int primary key
)
alter table Worker_storekeepers
add constraint FK_Worker_storekeepers__Users foreign key([User_id])
references Users([User_id])
on delete no action
on update cascade
create index I_Worker_storekeepers on Worker_storekeepers([User_id])

--lista upwarznionych do zamawiania towaru
create table Worker_purchaser(
[User_id] int primary key
)
alter table Worker_Purchaser
add constraint FK_Worker_purchaser__Users foreign key([User_id])
references Users([User_id])
on delete no action
on update cascade
create index I_Worker_purchaser on Worker_purchaser([User_id])

--producenci sprzetu
create table Worker(
Product_producer_id int primary key identity(1,1),
Product_producer_name nchar(25) not null
)
create index I_Worker on Worker(Product_producer_id)

--kategorie sprzetu
create table Product_categories(
Product_category_id int primary key identity(1,1),
Product_sub_category int,
Product_category_name nchar(25)
)
create index I_Product_categories on Product_categories(Product_category_id)
alter table Product_categories
add constraint FK_Product_categories__Product_categories foreign key(Product_sub_category)
references Product_categories(Product_category_id)
on delete no action
on update no action

--lista produktow
create table Products(
Product_id int primary key identity(1,1),
Product_category_id int not null,
Product_producer_id int not null,
Product_name nchar(50) not null,
Product_description ntext not null,
Product_aviable bit default(1) not null
)
alter table Products
add constraint FK_Products__Product_categories foreign key(Product_category_id)
references Product_categories(Product_category_id)
on delete no action
on update cascade
alter table Products
add constraint FK_Products__Worker foreign key(Product_producer_id)
references Worker(Product_producer_id)
on delete no action
on update cascade

--specyfikacja produktow
create table Product_specifications(
Product_specification_id int primary key identity(1,1),
Product_id int not null,
Product_specification_name nchar(30) not null,
Product_specification_value nchar(25) not null
)
alter table Product_specifications
add constraint FK_Product_specifications__Products foreign key(Product_id)
references Products(Product_id)
on delete no action
on update cascade

--historia cen produktu
create table Products_price(
Product_id int not null,
Product_price_date smalldatetime default(SYSDATETIME()) not null,
Product_price smallmoney not null,
constraint PK_Product_price primary key(Product_id, Product_price_date)
)
alter table Products_price
add constraint FK_Products_price__Products foreign key(Product_id)
references Products(Product_id)
on delete no action
on update cascade

--obrazki produktu
create table Product_images(
Product_image_id int primary key identity(1,1),
Product_id int not null,
Product_image nchar(50) not null
)
alter table Product_images
add constraint FK_Product_images__Products foreign key(Product_id)
references Products(Product_id)
on delete no action
on update cascade

--lista recenzji produktu
create table Product_opinions(
Product_id int not null,
[User_id] int not null,
Product_opinion nchar(200)
constraint PK_Product_opinions primary key(Product_id, [User_id])
)
alter table Product_opinions
add constraint FK_Product_opinions__Products foreign key (Product_id)
references Products(Product_id)
on delete no action
on update cascade
alter table Product_opinions
add constraint FK_Product_opinions__Users foreign key([User_id])
references Users([User_id])
on delete no action
on update cascade

--lista ocen produktu
create table Product_ratings(
Product_id int not null,
[User_id] int not null,
Product_rating tinyint not null,
constraint PK_Product_ratings primary key(Product_id, [User_id])
)
alter table Product_ratings
add constraint FK_Product_ratings__Products foreign key (Product_id)
references Products(Product_id)
on delete no action
on update cascade
alter table Product_ratings
add constraint FK_Product_ratings__Users foreign key([User_id])
references Users([User_id])
on delete no action
on update cascade

--lista magazynow
create table Storages(
Storage_id int primary key identity(1,1),
Address_id int not null,
Storage_name nchar(25) not null
)
create index I_Storages on Storages(Storage_id)
alter table Storages
add constraint FK_Storages__Addresses foreign key (Address_id)
references Addresses(Address_id)
on delete no action
on update cascade

--lista zamowien produktow
create table Product_orders(
Product_order_id int primary key identity(1,1),
Storage_id int not null,
Worker_purchaser_id int not null,
Product_id int not null,
Product_order_price smallmoney not null,
Product_order_quantity smallint not null,
Product_order_date smalldatetime default(SYSDATETIME()) not null,
Product_order_estimated_time date
)
alter table Product_orders
add constraint FK_Product_orders__Storages foreign key(Storage_id)
references Storages(Storage_id)
on delete no action
on update cascade
alter table Product_orders
add constraint FK_Product_orders__Products foreign key(Product_id)
references Products(Product_id)
on delete no action
on update cascade
alter table Product_orders
add constraint FK_Product_orders__Users foreign key(Worker_purchaser_id)
references Users([User_id])
on delete no action
on update cascade
create index I_Product_orders on Product_orders(Product_order_id)

--lista odbioru zamowionych towarow
create table Product_receipts(
Product_order_id int primary key not null,
Worker_storekeeper_id int not null,
Product_receipt_date smalldatetime default(SYSDATETIME()) not null
)
alter table Product_receipts
add constraint FK_Product_receipts__Users foreign key(Worker_storekeeper_id)
references Users([User_id])
on delete no action
on update cascade
alter table Product_receipts
add constraint FK_Product_receipts__Product_orders foreign key(Product_order_id)
references Product_orders(Product_order_id)
on delete no action
on update no action

--lista produktow w magazynach
create table Storage_Products(
Storage_Product_id int primary key identity(1,1),
Product_receipt_id int not null,
Product_id int not null,
Storage_Product_note nchar(100)
)
alter table Storage_Products
add constraint FK_Storage_Products__Product_receipts foreign key(Product_receipt_id)
references Product_receipts(Product_order_id)
on delete no action
on update cascade
create index I_Storage_Products on Storage_Products(Storage_Product_id)

--przechowywanie informacji o po³o¿eniu produktu
create table Storage_Product_localizations(
Storage_Product_id int not null,
Storage_id int not null,
Worker_storekeeper_id int not null,
Storage_Product_on_the_way bit default(0) not null,
Storage_Product_date smalldatetime default(SYSDATETIME()) not null,
constraint PK_Storage_Product_localizations primary key(Storage_Product_id, Storage_id, Storage_Product_date)
)
alter table Storage_Product_localizations
add constraint FK_Storage_Product_localizations__Storage_Products foreign key (Storage_Product_id)
references Storage_Products(Storage_Product_id)
on delete no action
on update cascade
alter table Storage_Product_localizations
add constraint FK_Storage_Product_localizations__Users foreign key (Worker_storekeeper_id)
references Users([User_id])
on delete no action
on update no action
alter table Storage_Product_localizations
add constraint FK_Storage_Product_localizations__Storages foreign key (Storage_id)
references Storages(Storage_id)
on delete no action
on update no action

--status zamowienia urzytkownikow
create table User_order_status(
User_order_status_id int primary key identity(1,1),
User_order_status_name nchar(25) not null
)
create index I_User_order_status on User_order_status(User_order_status_id)

--lista zamowien urzytkownikow
create table User_orders(
User_order_id int primary key identity(1,1),
User_order_status_id int not null,
User_Address_id int not null,
User_order_date smalldatetime default(SYSDATETIME()) not null,
User_note nchar(100)
)
alter table User_orders
add constraint FK_User_orders__User_order_status foreign key (User_order_status_id)
references User_order_status(User_order_status_id)
on delete no action
on update cascade
alter table User_orders
add constraint FK_User_orders__User_Addresses foreign key (User_Address_id)
references User_Addresses(User_Address_id)
on delete no action
on update cascade
create index I_User_orders on User_orders(User_order_id)

--lista produktow zamowionych przez uzytkownika
create table User_order_Products(
User_order_Product_id int primary key identity(1,1),
User_order_id int not null,
Storage_Product_id int not null,
User_order_Product_price smallmoney not null
)
alter table User_order_Products
add constraint FK_User_order_Products__User_orders foreign key(User_order_id)
references User_orders(User_order_id)
on delete no action
on update cascade
alter table User_order_Products
add constraint FK_User_order_Products__Storage_Products foreign key(Storage_Product_id)
references Storage_Products(Storage_Product_id)
on delete no action
on update no action

--lista zakonczonych transakcji
create table User_order_receipts(
User_order_id int not null,
Worker_seller_id int not null,
User_order_recipt_date smalldatetime default(SYSDATETIME()) not null
)
alter table User_order_receipts
add constraint FK_User_order_receipts__Users foreign key(Worker_seller_id)
references Users([User_id])
on delete no action
on update cascade
alter table User_order_receipts
add constraint FK_User_order_receipts__User_orders foreign key(User_order_id)
references User_orders(User_order_id)
on delete no action
on update no action
go
create trigger TR_Product_orders on Product_orders--sprawdzanie czy osoba ma uprawnienia
after insert as
IF (ROWCOUNT_BIG() = 0)
RETURN;

declare @Worker int
declare C_Product_orders cursor for
select Worker_purchaser_id from inserted
open C_Product_orders

declare @c int
declare @j int
select @c = count(*) from inserted
select @j = 1

while @j<=@c
begin
fetch next from C_Product_orders into
@Worker

if @@FETCH_STATUS <> 0
return

if not exists(SELECT [User_id] from Worker_purchaser where [User_id]=@Worker)
begin
RAISERROR ('You dont have acces to receipt Products.', 16, 1);  
ROLLBACK TRANSACTION;
end
select @j=@j+1
end
close C_Product_orders
deallocate C_Product_orders
go
create trigger TR_Product_receipts on Product_receipts--sprawdzanie czy ma sie uprawnienia i dodawanie towarow do tabel
after insert as
IF (ROWCOUNT_BIG() = 0)
RETURN;

declare @Worker int
declare @Product_id int
declare @quantity int
declare @id int
declare C_Product_receipts cursor for
select Worker_storekeeper_id, Product_id, Product_order_quantity, i.Product_order_id from inserted i join Product_orders o on o.Product_order_id=i.Product_order_id
open C_Product_receipts

declare @c int
declare @j int
select @c = count(*) from inserted
select @j = 1

while @j<=@c
begin
fetch next from C_Product_receipts into
@Worker, @Product_id, @quantity, @id

if @@FETCH_STATUS <> 0
return

if not exists(SELECT [User_id] from Worker_storekeepers where [User_id]=@Worker)
begin
RAISERROR ('You dont have acces to receipt Products.', 16, 1);  
ROLLBACK TRANSACTION;
end
declare @i int
select @i=0

while @i<@quantity
begin
SET NOCOUNT ON
insert into Storage_Products(Product_id, Product_receipt_id) values
(@Product_id, @id)
select @i=@i+1
end
select @j=@j+1
end
close C_Product_receipts
deallocate C_Product_receipts
go
create trigger TR_Storage_Product_localizations on Storage_Product_localizations
after insert as
if(ROWCOUNT_BIG() = 0)
return

declare @id int
declare C_Storage_Product_localizations cursor for
select Worker_storekeeper_id from inserted
open C_Storage_Product_localizations

declare @j int
select @j=1
declare @c int
select @c = count(*) from inserted
where(@j <= @c)
begin
fetch next from C_Storage_Product_localizations into
@id

if not exists(SELECT [User_id] from Worker_storekeepers where [User_id]=@id)
begin
RAISERROR('You dont have acces to do this',1,1)
rollback transaction
end
end

close C_Storage_Product_localizations
deallocate C_Storage_Product_localizations
go
create trigger TR_Storage_Products on Storage_Products
after insert as
if(ROWCOUNT_BIG() = 0)
return

declare @id int
declare @Worker int
declare @Storage int
declare C_Storage_Products cursor for
SELECT i.Storage_Product_id, r.Worker_storekeeper_id, o.Storage_id from inserted i
join Product_orders o on i.Product_receipt_id=o.Product_order_id
join Product_receipts r on i.Product_receipt_id=r.Product_order_id

open C_Storage_Products

declare @c int
declare @j int
select @c = count(*) from inserted
select @j = 1

while @j<=@c
begin
fetch next from C_Storage_Products into
@id, @Worker, @Storage

--SET NOCOUNT ON
insert into Storage_Product_localizations(Storage_id, Storage_Product_id, Worker_storekeeper_id ) values
(@Storage, @id, @Worker)

select @j=@j+1
end
close C_Storage_Products
deallocate C_Storage_Products
go
create trigger TR_User_order_receipts on User_order_receipts
after insert as
if(ROWCOUNT_BIG() = 0)
return

declare @id int
declare C_User_order_receipts cursor for
select Worker_seller_id from inserted
open C_User_order_receipts

declare @j int
select @j=1
declare @c int
select @c = count(*) from inserted
where(@j <= @c)
begin
fetch next from C_User_order_receipts into
@id

if not exists(SELECT [User_id] from Worker_sellers where [User_id]=@id)
begin
RAISERROR('You dont have acces to do this',1,1)
rollback transaction
end
end

close C_User_order_receipts
deallocate C_User_order_receipts
go
create view V_Workers as -- lista wszystkich pracowników
SELECT * from Users u
WHERE u.User_group_id=2
go
create view V_available_Products as --lista produktow w magazynach
select p.Product_name, count(sp.Storage_Product_id) as "ilosc"
from Products p
left join Storage_Products sp on p.Product_id=sp.Product_id
left join User_order_Products uop on uop.Storage_Product_id=sp.Storage_Product_id
left join User_orders uo on uo.User_order_id = uop.User_order_id
where uo.User_order_status_id!=3 or uo.User_order_status_id is null
group by p.Product_id, p.Product_name
go
create view V_order_list as--lista zamowien klientow
SELECT uo.User_order_id, sp.Storage_Product_id, Product_name from User_orders uo
join User_order_Products uop on uo.User_order_id=uop.User_order_id
join Storage_Products sp on sp.Storage_Product_id=uop.Storage_Product_id
join Products p on p.Product_id=sp.Product_id
where uo.User_order_status_id in (1,2)
go
create view V_planned_receipts as--lista planowanych dostaw produktow
select po.Product_order_date, po.Product_order_estimated_time, p.Product_id, p.Product_name, po.Product_order_quantity from Product_orders po
left join Products p on p.Product_id=po.Product_id
left join Product_receipts pr on po.Product_order_id=pr.Product_order_id
where pr.Product_order_id is null
go
create view V_Products_not_aviable as --produkty ktorych nie ma w magazynach
select p.Product_id, p.Product_name from Products p
left join Storage_Products sp on sp.Product_id=p.Product_id
where sp.Product_id is null
go
create view V_Products_specyfication as --specyfikacja produktow
select p.Product_name, ps.Product_specification_name, ps.Product_specification_value from Products p
join Product_specifications ps on p.Product_id=ps.Product_id
go
create view V_Products_recently_sold as--sprzedane w ostatnim czasie
select p.Product_name, count(*) as 'amount sold' from User_order_Products uop
join User_orders uo on uo.User_order_id=uop.User_order_id
join Storage_Products sp on uop.Storage_Product_id=sp.Storage_Product_id
join Products p on p.Product_id = sp.Product_id
where uo.User_order_date > DATEDIFF(DAY,  DATEADD(day, -10, SYSDATETIME()), GETDATE())
group by p.Product_id, p.Product_name
go
insert into Addresses(Address_country, Address_city, Address_street, Address_building_number, Address_zip_code) values
('Polska','Kraków','Komandora Wroñskiego Bohdana','54','30-852'),
('Polska','Warszawa','11 Listopada','136', '03-436'),
('Polska','£ódz','Al. Jana Paw³a II','112','93-570'),
('Polska','Kraków','Sp³awiñskiego Lehra Tadeusza','97','31-753'),
('Polska','Nowy s¹cz','Agaty Konstanty','60','33-304'),
('Polska','Kraków','Dietla Józefa','139','31-054'),
('Polska','Warszawa','Ewy','80','03-641'),
('Polska','Kraków','Oleandry','46','30-060'),
('Polska','Poznañ','Dolna','74','61-680'),
('Polska','Kraków','Sudecka','28','30-732')
go
insert into User_groups(User_group_name) values
('Klient'),
('Pracownik'),
('Administrator')
go
insert into Users(User_group_id, [User_name], User_password) values
(3, 'admin 1', 'admin'),
(2, 'sprzedawca 1', '123'),
(2, 'sprzedawca 2', '123'),
(2, 'sprzedawca 3', '123'),
(2, 'magazynier 1', 'zaq12wsx'),
(2, 'magazynier 2', 'zaq12wsx'),
(2, 'magazynier 3', 'zaq12wsx'),
(2, 'magazynier 4', 'zaq12wsx'),
(1, 'klient 1', 'okon'),
(1, 'klient 2', 'okon'),
(1, 'klient 3', 'okon'),
(1, 'klient 4', 'okon'),
(1, 'klient 5', 'okon'),
(1, 'klient 6', 'okon'),
(1, 'klient 7', 'okon'),
(1, 'klient 8', 'okon'),
(1, 'klient 9', 'okon'),
(1, 'klient 10', 'okon'),
(1, 'klient 11', 'okon'),
(1, 'klient 12', 'okon'),
(1, 'klient 13', 'okon'),
(1, 'klient 14', 'okon'),
(1, 'klient 15', 'okon'),
(1, 'klient 16', 'okon'),
(1, 'klient 17', 'okon'),
(1, 'klient 18', 'okon'),
(1, 'klient 19', 'okon'),
(1, 'klient 20', 'okon'),
(1, 'klient 21', 'okon'),
(1, 'klient 22', 'okon')
go
insert into Users_data([User_id], User_first_name, User_second_name, User_family_name) values
(1,'Anna',NULL,'Nowak'),
(2,'Maria',NULL,'Kowalski'),
(3,'Katarzyna',NULL,'Wiœniewski'),
(4,'Ma³gorzata',NULL,'D¹browski'),
(5,'Agnieszka',NULL,'Lewandowski'),
(6,'Krystyna',NULL,'Wójcik'),
(7,'Barbara',NULL,'Kamiñski'),
(8,'Ewa',NULL,'Kowalczyk'),
(9,'El¿bieta',NULL,'Zieliñski'),
(10,'Zofia',NULL,'Szymañski'),
(11,'Janina',NULL,'WoŸniak'),
(12,'Teresa',NULL,'Król'),
(13,'Joanna',NULL,'Jankowski'),
(14,'Magdalena',NULL,'Wojciechowski'),
(15,'Monika',NULL,'Kwiatkowski'),
(16,'Jan',NULL,'Kaczmarek'),
(17,'Andrzej',NULL,'Mazur'),
(18,'Piotr',NULL,'Krawczyk'),
(19,'Krzysztof',NULL,'Piotrowski'),
(20,'Stanis³aw',NULL,'Grabowski'),
(21,'Tomasz',NULL,'Nowakowski'),
(22,'Pawe³',NULL,'Paw³owski'),
(23,'Józef',NULL,'Michalski'),
(24,'Marcin',NULL,'Nowicki'),
(25,'Marek',NULL,'Adamczyk'),
(26,'Micha³',NULL,'Dudek'),
(27,'Grzegorz',NULL,'Zaj¹c'),
(28,'Jerzy',NULL,'Wieczorek'),
(29,'Tadeusz',NULL,'Jab³oñski'),
(30,'Adam',NULL,'Król')
go
insert into User_Addresses([User_id], Address_id) values
(30,2),
(1,7),
(22,6),
(19,1),
(27,2),
(24,2),
(12,4),
(12,7),
(24,2),
(17,3),
(6,3),
(21,1),
(17,5),
(4,7),
(6,7),
(21,4),
(26,5),
(23,6),
(3,7),
(15,3),
(30,3),
(13,2),
(11,5),
(20,1),
(12,3),
(3,7),
(19,5),
(10,6),
(7,4),
(3,2)
go
insert into Worker_sellers([User_id]) values
(1),(2),(3),(4)
go
insert into Worker_purchaser([User_id]) values
(1),(3)
go
insert into Worker_storekeepers([User_id]) values
(1),(5),(6),(7),(8)
go
insert into Worker(Product_producer_name) values
('Intel'),('Amd'),('Good ram')
go
insert into Product_categories(Product_category_name, Product_sub_category) values
('Komputery', NULL),
('Procesory', 1),
('Ram_y',1),
('DDR 3', 3),
('DDR 4', 3)
go
insert into Products(Product_category_id, Product_producer_id, Product_name, Product_description) values
(2,1,'Intel Core i5-9400F', ' '),
(2,1,'Intel Core i7-9700K', ' '),
(2,2,'AMD Ryzen 7 3700X', ' '),
(2,2,'AMD Ryzen 9 3900X', ' '),
(4,3,'GOODRAM 8GB 1333MHz CL9',' '),
(5,3,'GOODRAM 16GB 3200MHz IRIDIUM Black CL16',' ')
go
insert into Product_specifications(Product_id, Product_specification_name, Product_specification_value) values
(1,'Rodzina procesorów','Intel Core i5'),
(1,'Seria procesora','i5-9400F'),
(1,'Gniazdo procesora (socket)','Socket 1151'),
(1,'Taktowanie rdzenia','2.9 GHz'),
(1,'Taktowanie w turbo','4.1 GHz'),
(1,'Liczba rdzeni fizycznych','6'),
(1,'Liczba w¹tków','6'),
(1,'Odblokowany mno¿nik','Nie'),
(1,'Pamiêæ podrêczna','9 MB'),

(2,'Rodzina procesorów','Intel Core i7'),
(2,'Seria procesora','i7-9700K'),
(2,'Gniazdo procesora (socket)','Socket 1151'),
(2,'Taktowanie rdzenia','3.6 GHz'),
(2,'Taktowanie w turbo','4.9 GHz'),
(2,'Liczba rdzeni fizycznych','8'),
(2,'Liczba w¹tków','8'),
(2,'Odblokowany mno¿nik','TAK'),
(2,'Pamiêæ podrêczna','12 MB'),

(3,'Rodzina procesorów','AMD Ryzen'),
(3,'Seria procesora','Ryzen 7 3700X'),
(3,'Gniazdo procesora (socket)','Socket AM4'),
(3,'Taktowanie rdzenia','3.6 GHz'),
(3,'Taktowanie w turbo','4.4 GHz'),
(3,'Liczba rdzeni fizycznych','8'),
(3,'Liczba w¹tków','16'),
(3,'Odblokowany mno¿nik','TAK'),
(3,'Pamiêæ podrêczna','36 MB'),

(4,'Rodzina procesorów','AMD Ryzen'),
(4,'Seria procesora','Ryzen 9 3900X'),
(4,'Gniazdo procesora (socket)','Socket AM4'),
(4,'Taktowanie rdzenia','3.8 GHz'),
(4,'Taktowanie w turbo','4.6 GHz'),
(4,'Liczba rdzeni fizycznych','12'),
(4,'Liczba w¹tków','24'),
(4,'Odblokowany mno¿nik','TAK'),
(4,'Pamiêæ podrêczna','70 MB'),

(5,'Rodzaj pamiêci','DDR3'),
(5,'Pojemnoœæ ca³kowita','8 GB (1x8 GB)'),
(5,'Taktowanie','1333 MHz (PC3-10600)'),
(5,'OpóŸnienia (cycle latency)','CL 9'),
(5,'Napiêcie','1,5 V'),

(6,'Rodzaj pamiêci','DDR4'),
(6,'Pojemnoœæ ca³kowita','16 GB (2x8 GB)'),
(6,'Taktowanie','3200 MHz (PC4-25600)'),
(6,'OpóŸnienia (cycle latency)','CL 16'),
(6,'Napiêcie','1,2 V')
go
insert into Products_price(Product_id, Product_price_date, Product_price) values
(1,'2019-08-21',700),
(1,'2019-10-21',669),
(2,'2019-08-21',1900),
(2,'2019-10-21',1869),
(3,'2019-10-21',1529),
(4,'2019-10-21',2899),
(5,'2019-10-21',139),
(6,'2019-10-21',349);
go
INSERT INTO Product_images(Product_id, Product_image)
Values (1, '1.png');
go
insert into Storages(Address_id, Storage_name) values
(8,'G³ówny magazyn'),
(9,'Sklep 1'),
(10,'Slep 2');
go
insert into Product_orders(Storage_id, Worker_purchaser_id, Product_id, Product_order_price, Product_order_quantity, Product_order_estimated_time) values
(1,1,1,2000, 4, '2019-10-25'),
(2,3,1,1000, 2, '2019-10-27'),
(3,3,1,1000, 2, NULL),
(3,3,1,1000, 2, '2022-02-13'),
(2,3,2,1000, 2, '2020-11-11'),
(1,3,3,1000, 2, '2020-03-03')
go
insert into Product_receipts(Product_order_id, Worker_storekeeper_id) values
(1,6),
(2,8),
(3,1);
go
insert into User_order_status(User_order_status_name) values
('Z³o¿one'),
('Wykonane'),
('Zrealizowane'),
('Zwrucone')

insert into User_orders(User_order_status_id, User_Address_id, User_note) values
(1, 1, NULL),
(1, 2, NULL),
(3, 3, NULL),
(1, 4, NULL)

insert into User_order_Products(User_order_id, Storage_Product_id, User_order_Product_price) values
(1, 1, 200),
(2, 2, 200),
(3, 3, 400),
(4, 4, 200)

insert into User_order_receipts(User_order_id, Worker_seller_id) values
(3, 1)
