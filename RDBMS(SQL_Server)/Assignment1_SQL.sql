
-- Display the lowest course fee.  (STUDY TABLE)--
select min(ccost) from study;

--Find the name of female programmer who is earning more than the highest paid male programmer 
select name1 from prog where sex='F' and sal > (select max(sal) from prog where sex='M');

--Who is the author of costliest package (SOFT TABLE)
select name1 from soft where scost = (select max(scost) from soft);

--Find the name and total sales value of packages (SALES VALUE=SCOST * SOLD)

select title,name1, scost*sold as 'sales value' from soft;

--Find the selling cost average for software developed in Pascal (SOFT Table)
select sum(scost)/(count(*)) as 'Average' from soft where devin='PASCAL';
				OR
select avg(scost) as 'Average' from soft where devin='PASCAL'; 

--How many packages were developed in DBASE (SOFT TABLE)
select count(*) from soft where devin='DBASE';

--List programmer name from Prog table and number of packages each developed (PROG and SOF TABLE)

select s.name1, count(s.title) as 'Number of packages' from prog p
join soft s
on p.name1 = s.name1
group by s.name1;

--Which institute conducts the costliest course. (STUDY TABLE)
select name1 from study where ccost = (select max(ccost) from study);

--Who is the highest paid C programmer (PROG TABLE)
select name1, sal from prog where sal = (select max(sal) from prog where prof1='C' or prof2='C');

--Which female programmer earns more than 3000 but does not know ORACLE & DBASE(PROG TABLE)
select name1 from prog where sex='F' and sal > 3000 and prof1 NOT IN ('ORACLE','DBASE') and prof2 NOT IN ('ORACLE','DBASE');

--Which package has the highest development cost. (SOFT TABLE)
select title from soft where ccost = (select max(ccost) from soft);

--Create a Stored Procedure that takes name of programmer as input and displays software developed by the programmer(SOFT TABLE).

use SQLDB;
GO

CREATE PROCEDURE ReturnSoftware @Programmer varchar(30)
AS
SELECT title FROM soft where name1 = @Programmer;


EXEC ReturnSoftware @Programmer = 'ANAND';