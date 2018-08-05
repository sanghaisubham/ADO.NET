USE master;
GO
CREATE DATABASE mydb
ON
(NAME=mydb_dat,
FILENAME='d:\UHG2018\mydb.mdf',
SIZE= 4MB,
MAXSIZE= 15MB,
FILEGROWTH	= 10% )
LOG ON
(NAME=mydb_log,
FILENAME='d:\UHG2018\mydblog.ldf',
SIZE	= 5MB,
MAXSIZE= 25MB,
FILEGROWTH= 5MB);
GO

create database SubhamDB

create database HelloDB
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
use SubhamDB 

create table product(productid int,name varchar(20),price money)

insert into product values(100,'Pen',100)
insert into product(productid,name,price) values(99,'Notebook',50)
insert into product(productid,name,price) values(102,'Pencil',50)
insert into product(productid,name) values(103,'Eraser')
insert into product(productid,name,price) values(104,'Glue',NULL)

select * from product
select name,price from product
select name,price from product where price > 50
select * from product where price is NULL
select * from product where price is  NOT NULL
select name,price from product order by price
select name,price from product order by price desc
select sum(price) as 'SUM' from product
select avg(price) as 'Avg' from product
select max(price) as 'Max' from product
select min(price) as 'Min' from product
select count(price) as 'Count' from product

update product set price=20 where productid=103
update product set price=30 where productid=104

delete from product where productid=99

select * from product where price between 50 and 100
select * from product where price not between 50 and 100

select * from product where
price in(select price from product where price between 50 and 100)

select * from product where
price not in(select price from product where price between 50 and 100)


/* check this */

create table person(name varchar(20)) 
select * from person 

alter table person add age tinyint
alter table person add height varchar(20)
alter table person alter column height smallint
alter table person drop column height  
alter table person add personId int identity(100,1) /*Autoicrement is identity with startring value and seed*/

insert into person values('Jack',20) 
insert into person values('Jill',21)

select * from person

drop table person

select * from Book
insert into Book (title,price,isbn) values('C++',50,123.55)
delete from Book where title='C++'
delete from Book where title='C#'
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
use SubhamDB
create table deptt(deptid int constraint pk primary key,
					name varchar(20),
					strength int)

insert into deptt values(100,'Sales',200)

select * from deptt

exec sp_helpconstraint deptt /* sp=>Stored Procedure*/

alter table deptt drop constraint pk

drop table deptt
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
create table deptt(deptid int primary key,
					name varchar(20),
					strength int)
exec sp_helpconstraint deptt

alter table deptt drop constraint PK__deptt__BE2C1AEEFBAC0E8E

alter table deptt add constraint pk primary key(deptid)

insert into deptt values(100,'Sales',20)
insert into deptt values(101,'marketing',30)
insert into deptt values(102,'finance',32)

select * from deptt

create table emp(
				 code int constraint epk primary key,
				 name varchar(20),
				 salary money,
				 deptid int constraint edfk foreign key references deptt(deptid))

drop table emp

create table emp(
				 code int constraint epk primary key,
				 name varchar(20),
				 salary money,
				 deptid int constraint edfk foreign key references deptt(deptid) 
				 on delete cascade on update cascade)

/* Updates the foreign key value in case the primary key is updated and removes the entries  
of foreign keys when corresponding primary key is deleted*/

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~check constraint~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

drop table product
create table product(pid int,title varchar(15),
price money constraint ck check(price>0 and price<=1000))/*check constraint checks domain integrity*/

insert into product values(10,'Pencil',2000)
insert into product values(10,'Pencil',200)
insert into product values(10,'Pen',400)

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

create table Details(
	pid int constraint dpk primary key,
	email varchar(20) constraint uk unique,
	city varchar(20) constraint df default('DELHI'))

	insert into Details values (10,'abc@abc.com','AGRA')
	insert into Details(pid,email) values (11,'xyz@abc.com')
	
	select * from Details
	/*unique allows one NULL whereas Pid does not allow NULL*/
	drop table Details
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 

use SubhamDB
select * from deptt
select * from emp

insert into emp 
values(99,'Ramesh',2100,2000)

alter table emp nocheck constraint edfk 
alter table emp check constraint edfk

update emp set deptid=101 where deptid=2000

alter table emp nocheck constraint epk 
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
--~~~~~~~~~~~~~~~~~~~~~~Inner Join ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
select e.name ,e.salary,d.name from 
emp e join deptt d 
on e.deptid=d.deptid

select emp.name ,salary,deptt.name from 
emp  inner join deptt  
on emp.deptid=deptt.deptid

select emp.name ,salary,deptt.name from 
emp  inner join deptt  
on emp.deptid=deptt.deptid where deptt.name='sales'
order by emp.salary desc

--~~~~~~~~~~~~~~~~~~~~~~Outter Join ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
select e.name ,e.salary,d.name from 
emp e left outer join deptt d 
on e.deptid=d.deptid

select e.name ,e.salary,d.name from 
emp e right outer join deptt d 
on e.deptid=d.deptid

--~~~~~~~~~~~~~~~~~~~~~~Cross Join ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
select e.deptid,e.name ,e.salary,d.name from 
emp e cross join deptt d 
--~~~~~~~~~~~~~~~~~~~~~~equi Join ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
select e.*,d.* from 
emp e join deptt d 
on e.deptid=d.deptid


--~~~~~~~~~~~~~~~~~~~~~~Self Join ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

select a.name,a.salary,b.name,b.salary from 
emp a join emp b 
on a.salary=b.salary 
where a.name>b.name
--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
select * from emp 

select deptid,sum(salary) as SUM from emp 
group by deptid

select d.name,sum(e.salary) as 'total' from 
emp e join deptt d
on e.deptid=d.deptid
group by d.name
having sum(e.salary)<10000

select d.name,sum(e.salary) as 'total' into tempoemp from 
emp e join deptt d
on e.deptid=d.deptid
group by d.name
having sum(e.salary)<10000

select * from tempoemp

select e.name,e.salary 
from emp e join deptt d 
on  e.deptid=d.deptid 
where d.strength=min(d.strength)

select e.name,e.salary,d.strength 
from emp e join deptt d 
on  e.deptid=d.deptid 
where d.strength =(select min(strength) from deptt)

select name,strength
from deptt 
group by strength

--~~~~~~~~~~~~View~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

--View is not a copy it is a window

select * from emp

--~~~~~~~~~~~~Simple View~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

create view View1 as 

select name,salary from emp

select * from View1

insert into View1 values('Tom',999) --Error

update View1 set name='####' where salary=2100

drop view View1

--~~~~~~~~~~~~Complex View(uses 2 Tables)~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

create view View2 as
select d.name,sum(salary) as 'sumSalary'
from emp e join deptt d
on e.deptid=d.deptid
group by d.name

select * from View2

create view View3 as
select e.name as 'empname',d.name as 'depttname',e.salary,d.strength
from emp e join deptt d
on e.deptid=d.deptid

select * from View3

update View3 set empname='####' , depttname='Production' --Error
where strength=10

--View updation affects the parent (base) table until the view updation does not affect updation of
--more than one base table

--View is not a copy it is a window


--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
use SubhamDB

exec sp_helpindex deptt

--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

alter table product add constraint ppk primary key(pid)

alter table product add constraint uk1 unique(title)

alter table product drop constraint uk1

--clustered index holds the property of storing the data in the leaf
--non clustered index holds the property of storing the reference to the data 

--Maximum Non Clustered index can be 999
create nonclustered index ncindex on product(title,price)

create clustered index index1 on product(price) 
-- we can have only one clustered index in a table

--~~~~~~~~~~~~~~~~~~~~~stored procedures~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

create procedure AddEmp @code int,@name varchar(20), --Name of procedure followed by parameter list
						@salary money,@DptId int

as 
insert into emp values(@code,@name,@salary,@DptId)

--Sql server compiles it and stores it as an Object

select * from emp

exec AddEmp 88,'Hupp',9999,101

--~~~~~~~~~~~~~~~~~~~~~~~~~~~~sproc with output parameters~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


create proc GetEmpDetails 
	@name varchar(20) output,--ouput paraneter
	@salary money output

as

select @name=e.name,@salary=e.salary 
from emp e join deptt d
on e.deptid=d.deptid
where d.strength=(select min(strength) from deptt)

declare @n varchar(20)
declare @s money

exec GetEmpDetails @n output,@s output
print @n
print @s